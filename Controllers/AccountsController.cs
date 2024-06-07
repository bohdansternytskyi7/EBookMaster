using AutoMapper;
using EBookMaster.DTOs;
using EBookMaster.Enums;
using EBookMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace MedicalFacility.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountsController(MainDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] RegisterRequestDTO registerRequest)
        {
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(registerRequest.Password, salt);
            await _context.Users.AddAsync(new User {
                Name = registerRequest.Name,
                Surname = registerRequest.Surname,
                Email = registerRequest.Email,
                Password = hashedPassword,
                Salt = salt,
                Role = Role.Reader,
                SubscriptionId = 1,
                LibraryCardNumber = await GetNexLibraryCardNumber()
			});
            await _context.SaveChangesAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequestDTO loginRequest)
        {
            var user = await _context.Users.Where(x => x.Email == loginRequest.Email).FirstOrDefaultAsync();
            var hashedPassword = HashPassword(loginRequest.Password, user.Salt);

            if (hashedPassword != user.Password)
                return Unauthorized();

            var refreshToken = GenerateSalt();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddHours(1);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(GenerateAccessToken()),
                refreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshTokenRequest)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshTokenRequest);

            if (user == null)
                return Unauthorized("Invalid refresh token.");

            if (user.RefreshTokenExpiration.Value < DateTime.Now)
                return Unauthorized("Refresh token expired.");  

            var newAccessToken = GenerateAccessToken();
            var newRefreshToken = GenerateSalt();
            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        private static string GenerateSalt()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        private static string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32));
        }

        private JwtSecurityToken GenerateAccessToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            return new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtSettings:ValidIssuer").Value,
                audience: _configuration.GetSection("JwtSettings:ValidAudience").Value,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        }

        private async Task<int> GetNexLibraryCardNumber()
        {
			var libraryCardNumbers = await _context.Users.Select(x => x.LibraryCardNumber).ToListAsync();
			return (libraryCardNumbers.Any() ? libraryCardNumbers.Max() : 0) + 1;
        }
    }
}
