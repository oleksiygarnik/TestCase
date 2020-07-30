using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain.Abstract;

namespace TestCase.Domain
{
    public sealed class RefreshToken : Entity
    {
        private const int DAYS_TO_EXPIRE = 5;

        public string Token { get; set; }
        public DateTime Expires { get; private set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsActive => DateTime.UtcNow <= Expires;

        public RefreshToken()
        {
            Expires = DateTime.UtcNow.AddDays(DAYS_TO_EXPIRE);
        }
    }
}
