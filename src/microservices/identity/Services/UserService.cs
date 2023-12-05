using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using identityapi.DTOs;
using identityapi.Entities;
using identityapi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace identityapi.services
{

    public class UserService(ApplicationDbContext context, IMapper mapper) : IUserService
    {
        public async Task<Results<Ok, NotFound>> Create(CreateUserDTO createUserDTO)
        {
            var user = mapper.Map<User>(createUserDTO);
            user.SetPassword(user.Password);
            context.Add(user);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }
        public async Task<Results<Ok<List<UserDTO>>, NotFound>> FindAll()
        {
            var usersDTO = new List<UserDTO>();

            var users = await context.Users.ToListAsync();
            if (users?.Count > 0)
            {
                usersDTO = mapper.Map<List<UserDTO>>(users);
            }
            return TypedResults.Ok(usersDTO);
        }

        public async Task<Results<Ok<UserDTO>, NotFound>> FindOne(Guid id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var userDTO = mapper.Map<UserDTO>(user);

            return TypedResults.Ok(userDTO);
        }

        public async Task<User> FindOneByUsername(string username)
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            return user!;

        }

        public async Task<Results<Ok, NotFound>> Update(Guid id, UpdateUserDTO updateUserDTO)
        {
            var user = await context.Users.AnyAsync(x => x.Id == id);

            if (!user)
            {
                return TypedResults.NotFound();
            }

            context.Update(updateUserDTO);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public async Task<Results<Ok, NotFound>> Remove(Guid id)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

    }
}