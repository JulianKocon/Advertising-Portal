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

        public async Task<ResultMessageDTO> LoginUserAsync(UserToLoginDTO userToLoginDTO)
        {
            User user = await _context.Users.Where(x => x.Email.Equals(userToLoginDTO.Email)).SingleOrDefaultAsync();
            if(user == null)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = "Wrong email"
                };
            }

            bool isPasswordValid = VerifyPasswordHash(userToLoginDTO.Password, user);

            if (!isPasswordValid)
            {
                return new ResultMessageDTO
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = "Wrong password"
                };
            }

            return new ResultMessageDTO
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "Logged in"
            };
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

        private bool VerifyPasswordHash(string password, User user)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}

