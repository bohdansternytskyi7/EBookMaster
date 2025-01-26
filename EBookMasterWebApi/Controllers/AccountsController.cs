using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Enums;
using EBookMasterWebApi.Services.Interfaces;

namespace EBookMasterWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        public AccountsController(EBookMasterDbContext context, IConfiguration configuration, IMapper mapper, IAccountsService accountsService)
	        : base(context, configuration, mapper, accountsService)
		{ }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> ReaderRegisterAsync([FromBody] RegisterRequestDTO registerRequest)
        {
	        var reader = await RegisterUserAsync(registerRequest, Role.Reader);
	        return Ok(new
	        {
		        AccessToken = _accountsService.GenerateAccessToken(reader),
		        RefreshToken = reader.RefreshToken!
	        });
        }

		[AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO loginRequest)
        {
            var user = await _context.Users
	            .Include(x => x.UserSubscription)
					.ThenInclude(x => x.Subscription)
	            .Where(x => x.Email == loginRequest.Email)
	            .FirstOrDefaultAsync();
            if (user == null)
                return BadRequest();

            if (_accountsService.HashPassword(loginRequest.Password, user.Salt) != user.Password)
                return Unauthorized();

            user.RefreshToken = _accountsService.GenerateSalt();
            user.RefreshTokenExpiration = DateTime.Now.AddHours(1);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = _accountsService.GenerateAccessToken(user),
				RefreshToken = user.RefreshToken,
                IsPremium = user.UserSubscription.Subscription.Type == SubscriptionType.Premium
			});
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
	        var user = await GetCurrentUserAsync();
	        user.RefreshToken = null;
	        user.RefreshTokenExpiration = null;
	        await _context.SaveChangesAsync();

	        var accessToken = Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
	        if (!string.IsNullOrEmpty(accessToken))
	        {
		        var tokenHandler = new JwtSecurityTokenHandler();
		        var jwtToken = tokenHandler.ReadJwtToken(accessToken);
		        var jti = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

		        if (!string.IsNullOrEmpty(jti))
			        _accountsService.BlacklistToken(jti);
	        }
	        return Ok();
        }
	}
}
