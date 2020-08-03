using Domain;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Auth.DTO;
using TestCase.Application.Auth.Exceptions;
using TestCase.Application.Auth.JWT;
using TestCase.Application.Users.Queries;
using TestCase.Domain;

namespace TestCase.Application.Auth
{
    public sealed class AuthService
    {
        private readonly JwtFactory _jwtFactory;
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public AuthService(IUnitOfWork work, JwtFactory jwtFactory) 
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
            _repository = _work.EntityRepository;
        }

        public async Task<AuthUserDto> Authorize(UserLoginDto userDto)
        {
            var userEntity = await _repository.FirstOrDefault<User>(u => u.Email == userDto.Email);

            if (userEntity is null)
                throw new NotFoundEntityException(nameof(User));

            if (!SecurityHelper.ValidatePassword(userDto.Password, userEntity.Password, userEntity.Salt))
                throw new InvalidUsernameOrPasswordException();

            var token = await GenerateAccessToken(userEntity.Id, userEntity.UserName, userEntity.Email);

            var user = new UserDto
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email
            }; 

            return new AuthUserDto
            {
                User = user,
                Token = token
            };
        }

        public async Task<AccessTokenDto> GenerateAccessToken(int userId, string userName, string email)
        {
            var refreshToken = _jwtFactory.GenerateRefreshToken();

            await _repository.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = userId
            });

            await _work.Commit();

            var accessToken = await _jwtFactory.GenerateAccessToken(userId, userName, email);

            return new AccessTokenDto(accessToken, refreshToken);
        }

        public async Task<AccessTokenDto> RefreshToken(RefreshTokenDto dto)
        {
            var userId = _jwtFactory.GetUserIdFromToken(dto.AccessToken, dto.SigningKey);

            var userEntity = await _repository.SingleOrDefault<User>(user => user.Id == userId);
                
            if (userEntity is null)
                throw new NotFoundEntityException(nameof(User), userId);

            var rToken = await _repository.FirstOrDefault<RefreshToken>(t => t.Token == dto.RefreshToken && t.UserId == userId);

            if (rToken is null)
                throw new InvalidTokenException("refresh");
            

            if (!rToken.IsActive)
                throw new ExpiredRefreshTokenException();

            var jwtToken = await _jwtFactory.GenerateAccessToken(userEntity.Id, userEntity.UserName, userEntity.Email);
            var refreshToken = _jwtFactory.GenerateRefreshToken();

            await _repository.Remove(rToken); // delete the token we've exchanged
            await _repository.Add(new RefreshToken // add the new one
            {
                Token = refreshToken,
                UserId = userEntity.Id
            });

            await _work.Commit();

            return new AccessTokenDto(jwtToken, refreshToken);
        }

        public async Task RevokeRefreshToken(string refreshToken, int userId)
        {
            var rToken = await _repository.FirstOrDefault<RefreshToken>(t => t.Token == refreshToken && t.UserId == userId);

            if (rToken is null)
                throw new InvalidTokenException("refresh");

            await _repository.Remove(rToken);
            await _work.Commit();
        }
    }
}
