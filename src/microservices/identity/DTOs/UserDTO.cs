using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public string Role { get; set; } = "User";
        public bool Status { get; set; } = true;
    }
}