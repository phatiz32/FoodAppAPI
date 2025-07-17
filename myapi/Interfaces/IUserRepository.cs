using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.User;

namespace myapi.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto?> GetByIdAsync(String userId);
        Task<bool> UpdateAsync(String userId, UpdateUserDto dto);
    }
}