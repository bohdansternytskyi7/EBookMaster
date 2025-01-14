using System.Text;
using EBookMasterWebApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using EBookMaster.Middlewares;
using EBookMasterWebApi.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EBookMasterWebApi.Services.Interfaces;
using EBookMasterWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EBookMasterDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
	opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var secretKey = builder.Configuration.GetSection("SECRET_KEY").Value;
if (string.IsNullOrEmpty(secretKey))
{
	throw new InvalidOperationException("SECRET_KEY is not set.");
}

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration.GetSection("JwtSettings:ValidIssuer").Value,
		ValidAudience = builder.Configuration.GetSection("JwtSettings:ValidAudience").Value,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
	};
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();
app.Run();
