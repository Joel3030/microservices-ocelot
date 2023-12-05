using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.DTOs
{
    public class LoginDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}