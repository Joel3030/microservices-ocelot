using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identityapi.DTOs;
using identityapi.Entities;
using identityapi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Middleware;

namespace identityapi.services
{
    public class AuthService(IUserService userService, IJwtBuilder jwtBuilder) : IAuthService
    {
        public async Task<Results<Ok<string>, UnauthorizedHttpResult>> Login(LoginDTO loginDTO)
        {
            var user = await userService.FindOneByUsername(loginDTO.Username);

            if (user is null || !user.ValidatePassword(loginDTO.Password, user.Password))
            {
                return TypedResults.Unauthorized();
            }

            var token = jwtBuilder.GetToken(user);

            return TypedResults.Ok(token);
        }
    }
}