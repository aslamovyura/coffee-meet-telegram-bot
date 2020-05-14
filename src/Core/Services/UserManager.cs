using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    /// <summary>
    /// Class to manage users.
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IApplicationContext _context;

        /// <summary>
        /// Define class to manage application users. 
        /// </summary>
        /// <param name="context">Application context.</param>
        public UserManager(IApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Username == username);

            return user;
        }

        /// <inheritdoc/>
        public async Task<AppUser> GetUserByIdAsync(long userId)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        /// <inheritdoc/>
        public async Task<ICollection<AppUser>> GetUsersAsync()
        {
            var users = await _context.AppUsers.ToListAsync();
            return users;
        }

        /// <inheritdoc/>
        public async Task<Result> CreateUserAsync(long userId, string username, string firstName = default, string lastName = default)
        {
            userId = userId > 0 ? userId: throw new ArgumentOutOfRangeException();
            username = username ?? userId.ToString();

            var isExist = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);

            if (isExist != null)
            {
                Console.WriteLine($"User @{username} is already exist!");
                return null;
            }

            var user = new AppUser
            {
                Id = userId,
                Username = username
            };

            Console.WriteLine($"Creating new user @{username}!");
            try
            {
                await _context.AppUsers.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR while creating user @{username}!");
                string[] errors = { ex.Message };
                return Result.Failure(errors);
            }

            Console.WriteLine($"User @{username} successfully created!");
            return Result.Success();
        }

        /// <inheritdoc/>
        public async Task<Result> DeleteUserAsync(long userId)
        {
            var user = _context.AppUsers.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return Result.Success();
            }

            _context.AppUsers.Remove(user);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}