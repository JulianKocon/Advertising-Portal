using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdvertisingPortal.DataAccess;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingPortal.Services.Implementations
{
    public class UserDbService : IUserDbService
    {
        public readonly PortalDbContext _context;

        public UserDbService(PortalDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(UserDTO userDTO)
        {
            User user = null;
            bool emailOccupied = _context.Users.AnyAsync(x => x.Email.Equals(userDTO.Email)).Result;
            
            if (emailOccupied) {
                return user;
            }

            CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
             user = new User()
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }
    }
}

