using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identityapi.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace identityapi.Services
{
    public interface IAuthService
    {
        Task<Results<Ok<string>, UnauthorizedHttpResult>> Login(LoginDTO loginDTO);
    }
}