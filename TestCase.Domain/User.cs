using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain.Abstract;

namespace TestCase.Domain
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public User(string email, string username, string password, string salt)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("message", nameof(email));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("message", nameof(username));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("message", nameof(password));

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("message", nameof(salt));

            Email = email;
            Username = username;
            Password = password;
            Salt = salt;
        }

        public void ChangeUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("message", nameof(username));

            Username = username;
        }

        public void ChangeEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("message", nameof(email));

            Email = email;
        }
    }
}
