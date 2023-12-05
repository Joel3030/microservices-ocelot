using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity.DTOs;
using identity.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace identity.Services
{
    public interface IUserService
    {
        Task<Results<Ok, NotFound>> Create(CreateUserDTO createUserDTO);
        Task<Results<Ok<List<UserDTO>>, NotFound>> FindAll();
        Task<Results<Ok<UserDTO>, NotFound>> FindOne(Guid id);
        Task<User> FindOneByUsername(string username);
        Task<Results<Ok, NotFound>> Update(Guid id, UpdateUserDTO updateUserDTO);
        Task<Results<Ok, NotFound>> Remove(Guid id);
    }
}