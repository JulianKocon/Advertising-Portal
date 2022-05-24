using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdvertisingPortal.DataAccess;
using AdvertisingPortal.DTO;
using AdvertisingPortal.Entities;
using AdvertisingPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisingPortal.Services.Implementations
{
    public class UserDbService : IUserDbService
    {
        public readonly PortalDbContext _context;
        private readonly IConfiguration _configuration;

        public UserDbService(PortalDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResultMessageDTO> LoginUserAsync(UserToLoginDTO userToLoginDTO)
        {
            User user = await _context.Users.Where(x => x.Email.Equals(userToLoginDTO.Email)).SingleOrDefaultAsync();
            if (user == null)
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
            string token = CreateToken(user);

            return new ResultMessageDTO
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "Logged in",
                Token = token
            };
        }

        public async Task<User> RegisterUserAsync(UserDTO userDTO)
        {
            User user = null;
            bool emailOccupied = _context.Users.AnyAsync(x => x.Email.Equals(userDTO.Email)).Result;

            if (emailOccupied)
            {
                return user;
            }

            CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user = new User()
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User"
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role)
            };






            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                "http://localhost:14838",
                "http://localhost:14838",
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
                ) ;

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

