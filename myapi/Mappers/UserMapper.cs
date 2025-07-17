using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.User;
using myapi.Models;

namespace myapi.Mappers
{
    public static class UserMapper
    {
        public static void ApplyTo(this UpdateUserDto dto, AppUser user)
        {
            if (dto.Email != null)
            user.Email = dto.Email;

            if (dto.PhoneNumber != null)
                user.PhoneNumber = dto.PhoneNumber;

            if (dto.FullName != null)
                user.FullName = dto.FullName;
        }
    }
}