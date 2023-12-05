using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace identity.Services
{
    public interface IAuthService
    {
        Task<Results<Ok<string>, UnauthorizedHttpResult>> Login(LoginDTO loginDTO);
    }
}