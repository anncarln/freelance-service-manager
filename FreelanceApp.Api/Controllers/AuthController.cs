using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FreelanceApp.Api.Data;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Helpers;
using FreelanceApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ApplicationDbContext context, JwtService jwtService) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly JwtService _jwtService = jwtService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> Login(LoginUserDto dto)
        {
            var passwordHash = HashPassword(dto.Password);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == passwordHash);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new LoginResultDto { Token = token });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);

            if (userExists)
            {
                return BadRequest("A user with this email already exists.");
            }

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        private static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hashedBytes);
        }
    }
}