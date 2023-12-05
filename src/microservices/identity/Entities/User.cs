using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity.Entities;

namespace identity.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; } = "User";
        public bool Status { get; set; } = true;

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password, 10);
        }

        public bool ValidatePassword(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}