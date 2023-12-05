using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity.DTOs;
using identity.Entities;
using identity.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Middleware;

namespace identity.services
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

            var token = jwtBuilder.GetToken(user.Id.ToString());

            return TypedResults.Ok(token);
        }
    }
}