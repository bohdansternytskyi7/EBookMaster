using Microsoft.EntityFrameworkCore;

namespace EBookMaster.Models
{
	public class MainDbContext : DbContext
	{
		private readonly IConfiguration _configuration;
		public MainDbContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public MainDbContext(DbContextOptions<MainDbContext> options, IConfiguration configuration)
			: base(options)
		{
			_configuration = configuration;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
