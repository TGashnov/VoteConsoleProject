using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;
using VoteWebApi.BL.Exceptions;
using VoteWebApi.BL.Model;

namespace VoteWebApi.BL.Services
{
    public partial class UsersService
    {
        public async Task<IEnumerable<UserApiDto>> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            return users.Select(u => new UserApiDto(u, userManager.GetRolesAsync(u).Result));
        }

        public async Task<UserApiDto> GetUser(string userName) 
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            return new UserApiDto(user, await userManager.GetRolesAsync(user));
        }

        public async Task<Exception> UpdateUser(UserDbDTO userDb, UserApiDto userApi)
        {
            if (userDb == null)
            {
                return new KeyNotFoundException("Пользователь не найден");
            }
            userApi.Update(userDb);
            var result = await userManager.UpdateAsync(userDb);
            return result.Succeeded ? null : new SaveChangesException();
        }

        public async Task<Exception> UpdateUser(UserApiDto user)
        {
            return await ApplyToUser(user.UserName, u => UpdateUser(u, user));
        }

        public async Task<Exception> ResetPassword(UserDbDTO user, string newPassword)
        {
            if(user == null)
            {
                return new KeyNotFoundException("Пользователь не найден");
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded ? null : new SaveChangesException();
        }

        public async Task<Exception> ResetPassword(string userName, string newPassword)
        {
            return await ApplyToUser(userName, user => ResetPassword(user, newPassword));
        }

        public async Task<Exception> Create(UserCreateApiDto user)
        {
            var userDb = user.Create();
            var result = await userManager.CreateAsync(userDb, user.Password);
            if (!result.Succeeded)
            {
                if (await UserExists(user.UserName))
                {
                    return new AlreadyExistsException($"Пользователь с именем {user.UserName} уже существует.");
                }
            }
            result = await userManager.AddToRolesAsync(userDb, user.Roles);
            if (!result.Succeeded)
            {
                return new Exception("Не удалось назначить пользователю одну или несколько из указанных ролей.");
            }

            return result.Succeeded ? null : new SaveChangesException();
        }

        public async Task<Exception> Delete(UserDbDTO user)
        {
            if (user == null)
            {
                return new KeyNotFoundException("Пользователь не найден.");
            }
            var result = await userManager.DeleteAsync(user);
            return result.Succeeded ? null : new SaveChangesException();
        }

        public async Task<Exception> Delete(string userName)
        {
            return await ApplyToUser(userName, Delete);
        }
    }
}
