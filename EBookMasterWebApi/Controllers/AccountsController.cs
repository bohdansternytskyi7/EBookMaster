using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using AutoMapper;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Models;

namespace MedicalFacility.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly EBookMasterDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountsController(EBookMasterDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginRequestDTO loginRequest)
        {
            var user = await _context.Users.Where(x => x.Email == loginRequest.Email).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest();

            if (HashPassword(loginRequest.Password, user.Salt) != user.Password)
                return Unauthorized();

            user.RefreshToken = GenerateSalt();
            user.RefreshTokenExpiration = DateTime.Now.AddHours(1);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(GenerateAccessToken(user)),
				user.RefreshToken
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

        private JwtSecurityToken GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            return new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtSettings:ValidIssuer").Value,
                audience: _configuration.GetSection("JwtSettings:ValidAudience").Value,
                claims: [ new Claim(ClaimTypes.NameIdentifier, user.Email) ],
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
