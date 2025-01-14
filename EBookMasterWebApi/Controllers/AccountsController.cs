using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.Controllers;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Enums;
using EBookMasterWebApi.Services.Interfaces;

namespace MedicalFacility.Controllers
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
            var user = await _context.Users.Where(x => x.Email == loginRequest.Email).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest();

            if (_accountsService.HashPassword(loginRequest.Password, user.Salt) != user.Password)
                return Unauthorized();

            user.RefreshToken = _accountsService.GenerateSalt();
            user.RefreshTokenExpiration = DateTime.Now.AddHours(1);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = _accountsService.GenerateAccessToken(user),
				user.RefreshToken
			});
        }
    }
}
