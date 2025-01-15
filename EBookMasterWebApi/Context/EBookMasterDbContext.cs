using EBookMasterWebApi.Enums;
using EBookMasterWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EBookMasterWebApi.Context
{
	public class EBookMasterDbContext : DbContext
	{
		private readonly IConfiguration _configuration;

		public EBookMasterDbContext(DbContextOptions<EBookMasterDbContext> options, IConfiguration configuration)
			: base(options)
		{
			_configuration = configuration;
		}

		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<PublishingHouse> PublishingHouses { get; set; }
		public DbSet<Series> Series { get; set; }
		public DbSet<BookBorrowing> BookBorrowings { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<Report> Reports { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Recommendation> Recommendations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Author>(e =>
			{
				var index = 1;
				e.HasData(
					new Author { Id = index++, DateOfBirth = new DateTime(1980, 1, 1), Nationality = "American", Name = "John", Surname = "Doe" },
					new Author { Id = index++, DateOfBirth = new DateTime(1975, 6, 15), Nationality = "British", Name = "Jane", Surname = "Smith" },
					new Author { Id = index++, DateOfBirth = new DateTime(1990, 11, 30), Nationality = "Canadian", Name = "Michael", Surname = "Johnson" },
					new Author { Id = index++, DateOfBirth = new DateTime(1985, 5, 25), Nationality = "Australian", Name = "Emily", Surname = "Brown" },
					new Author { Id = index++, DateOfBirth = new DateTime(1970, 8, 19), Nationality = "French", Name = "Pierre", Surname = "Davis" },
					new Author { Id = index++, DateOfBirth = new DateTime(1995, 3, 10), Nationality = "German", Name = "Hans", Surname = "Müller" },
					new Author { Id = index++, DateOfBirth = new DateTime(1988, 2, 28), Nationality = "Italian", Name = "Francesca", Surname = "Bianchi" },
					new Author { Id = index++, DateOfBirth = new DateTime(1965, 9, 22), Nationality = "Spanish", Name = "Carlos", Surname = "García" },
					new Author { Id = index++, DateOfBirth = new DateTime(1978, 7, 12), Nationality = "Japanese", Name = "Sakura", Surname = "Yamamoto" },
					new Author { Id = index++, DateOfBirth = new DateTime(1983, 4, 18), Nationality = "Indian", Name = "Arjun", Surname = "Patel" }
				);
			});

			modelBuilder.Entity<Category>(e =>
			{
				var index = 1;
				e.HasData(
					new Category { Id = index++, Name = "Fiction", Description = "Narrative literature created from the imagination." },
					new Category { Id = index++, Name = "Non-Fiction", Description = "Literature based on facts, real events, and real people." },
					new Category { Id = index++, Name = "Science Fiction", Description = "Literature dealing with futuristic settings and advanced technologies." },
					new Category { Id = index++, Name = "Fantasy", Description = "Literature featuring magical and supernatural elements." },
					new Category { Id = index++, Name = "Biography", Description = "Literature detailing the life of a real person." },
					new Category { Id = index++, Name = "History", Description = "Literature based on historical events and figures." },
					new Category { Id = index++, Name = "Mystery", Description = "Literature dealing with the solution of a crime or unraveling of secrets." },
					new Category { Id = index++, Name = "Thriller", Description = "Literature designed to hold the interest by the use of a high degree of intrigue, adventure, or suspense." }
				);
			});

			modelBuilder.Entity<PublishingHouse>(e =>
			{
				var index = 1;
				e.HasData(
					new PublishingHouse { Id = index++, Name = "Penguin Random House", Country = "United States", FoundationDate = new DateTime(2013, 7, 1) },
					new PublishingHouse { Id = index++, Name = "HarperCollins", Country = "United States", FoundationDate = new DateTime(1989, 8, 1) },
					new PublishingHouse { Id = index++, Name = "Simon & Schuster", Country = "United States", FoundationDate = new DateTime(1924, 1, 2) },
					new PublishingHouse { Id = index++, Name = "Macmillan Publishers", Country = "United Kingdom", FoundationDate = new DateTime(1843, 8, 10) },
					new PublishingHouse { Id = index++, Name = "Hachette Livre", Country = "France", FoundationDate = new DateTime(1826, 6, 1) }
				);
			});

			modelBuilder.Entity<Series>(e =>
			{
				var index = 1;
				e.HasData(
					new Series { Id = index++, Name = "The Lord of the Rings", IsOver = true, FirstBookPublicationDate = new DateTime(1954, 7, 29) },
					new Series { Id = index++, Name = "Harry Potter", IsOver = true, FirstBookPublicationDate = new DateTime(1997, 6, 26) },
					new Series { Id = index++, Name = "A Song of Ice and Fire", IsOver = false, FirstBookPublicationDate = new DateTime(1996, 8, 6) },
					new Series { Id = index++, Name = "The Chronicles of Narnia", IsOver = true, FirstBookPublicationDate = new DateTime(1950, 10, 16) },
					new Series { Id = index++, Name = "Sherlock Holmes", IsOver = true, FirstBookPublicationDate = new DateTime(1887, 11, 14) },
					new Series { Id = index++, Name = "The Hunger Games", IsOver = true, FirstBookPublicationDate = new DateTime(2008, 9, 14) },
					new Series { Id = index++, Name = "Percy Jackson & the Olympians", IsOver = true, FirstBookPublicationDate = new DateTime(2005, 6, 28) },
					new Series { Id = index++, Name = "Divergent", IsOver = true, FirstBookPublicationDate = new DateTime(2011, 4, 25) }
				);
			});

			modelBuilder.Entity<Book>(e =>
			{
				e.HasMany(x => x.Authors)
					.WithMany(y => y.Books)
					.UsingEntity(z => z.HasData(
						new { AuthorsId = 1, BooksId = 1 },
						new { AuthorsId = 1, BooksId = 2 },
						new { AuthorsId = 2, BooksId = 3 },
						new { AuthorsId = 2, BooksId = 4 },
						new { AuthorsId = 3, BooksId = 5 },
						new { AuthorsId = 8, BooksId = 8 },
						new { AuthorsId = 7, BooksId = 8 },
						new { AuthorsId = 5, BooksId = 7 },
						new { AuthorsId = 6, BooksId = 6 },
						new { AuthorsId = 1, BooksId = 3 },
						new { AuthorsId = 6, BooksId = 7 }
					));

				e.HasMany(b => b.Categories)
					.WithMany(c => c.Books)
					.UsingEntity(j => j.HasData(
						new { BooksId = 1, CategoriesId = 1 },
						new { BooksId = 2, CategoriesId = 1 },
						new { BooksId = 3, CategoriesId = 1 },
						new { BooksId = 4, CategoriesId = 1 },
						new { BooksId = 8, CategoriesId = 8 },
						new { BooksId = 7, CategoriesId = 8 },
						new { BooksId = 6, CategoriesId = 7 },
						new { BooksId = 6, CategoriesId = 6 },
						new { BooksId = 5, CategoriesId = 2 }
					));

				var index = 1;
				e.HasData(
					new { Id = index++, Title = "The Fellowship of the Ring", PublishingHouseId = 1, PublicationYear = new DateTime(1954, 7, 29), SeriesId = 1, Status = BookStatus.Available, IsPremium = true },
					new { Id = index++, Title = "The Two Towers", PublishingHouseId = 1, PublicationYear = new DateTime(1954, 11, 11), SeriesId = 1, Status = BookStatus.Available, IsPremium = false },
					new { Id = index++, Title = "Harry Potter and the Philosopher's Stone", PublishingHouseId = 2, PublicationYear = new DateTime(1997, 6, 26), SeriesId = 2, Status = BookStatus.Available, IsPremium = false },
					new { Id = index++, Title = "Harry Potter and the Chamber of Secrets", PublishingHouseId = 2, PublicationYear = new DateTime(1998, 7, 2), SeriesId = 2, Status = BookStatus.Available, IsPremium = true },
					new { Id = index++, Title = "A Brief History of Time", PublishingHouseId = 1, PublicationYear = new DateTime(1988, 4, 1), SeriesId = (int?)null, Status = BookStatus.Available, IsPremium = true },
					new { Id = index++, Title = "The Hunger Games", PublishingHouseId = 2, PublicationYear = new DateTime(2008, 9, 14), SeriesId = 6, Status = BookStatus.Available, IsPremium = true },
					new { Id = index++, Title = "Catching Fire", PublishingHouseId = 2, PublicationYear = new DateTime(2009, 9, 1), SeriesId = 6, Status = BookStatus.Available, IsPremium = false },
					new { Id = index++, Title = "Mockingjay", PublishingHouseId = 2, PublicationYear = new DateTime(2010, 8, 24), SeriesId = 6, Status = BookStatus.Available, IsPremium = false }
				);
			});

			modelBuilder.Entity<Report>(e =>
			{
				e.Property(s => s.AverageRate)
					.HasColumnType("decimal(18, 2)");
			});

			modelBuilder.Entity<Subscription>(e =>
			{
				e.Property(s => s.Price)
					.HasColumnType("decimal(18, 2)");

				var index = 1;
				e.HasData(
					new Subscription { Id = index++, Type = SubscriptionType.Standard, Period = SubscriptionPeriod.Monthly, Price = 9.99m },
					new Subscription { Id = index++, Type = SubscriptionType.Premium, Period = SubscriptionPeriod.Monthly, Price = 14.99m },
					new Subscription { Id = index++, Type = SubscriptionType.Standard, Period = SubscriptionPeriod.Annual, Price = 100 },
					new Subscription { Id = index++, Type = SubscriptionType.Premium, Period = SubscriptionPeriod.Annual, Price = 150 }
				);
			});

			modelBuilder.Entity<User>(e =>
			{
				var index = 1;
				e.HasData(
					new User
					{
						Id = index,
						Name = "Bohdan",
						Surname = "Sternytskyi",
						Email = "s26028@pjwstk.edu.pl",
						Password = "jZs/vfkieZcdBngxPAHzXuEDi5XZg0tOXXdtUooa1ag=",
						Salt = "mZ5bf60ttVt+4Xx6FHpvFHx+Vx/pPUoYql9QO+G9t3Y=",
						LibraryCardNumber = index++,
						Role = Role.Librarian,
						SubscriptionId = 4
					}
				);
			});

			modelBuilder.Entity<BookBorrowing>(e =>
			{
				var index = 1;
				e.HasData(
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 5, 20, 10, 0, 0), ReturnDate = new DateTime(2024, 6, 20, 10, 0, 0), BookId = 2, UserId = 1 },
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 6, 1, 11, 0, 0), ReturnDate = new DateTime(2024, 7, 1, 11, 0, 0), BookId = 3, UserId = 1 },
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 6, 10, 12, 0, 0), ReturnDate = new DateTime(2024, 7, 10, 12, 0, 0), BookId = 4, UserId = 1 },
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 7, 5, 13, 0, 0), ReturnDate = null, BookId = 2, UserId = 1 },
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 5, 15, 14, 0, 0), ReturnDate = new DateTime(2024, 6, 15, 14, 0, 0), BookId = 1, UserId = 1 },
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 7, 20, 15, 0, 0), ReturnDate = new DateTime(2024, 8, 20, 15, 0, 0), BookId = 5, UserId = 1 },
					new BookBorrowing { Id = index++, BorrowingDate = new DateTime(2024, 8, 1, 16, 0, 0), ReturnDate = null, BookId = 6, UserId = 1 }
				);
			});

			modelBuilder.Entity<Review>(e =>
			{
				var index = 1;
				e.HasData(
					new Review { Id = index++, Rate = 4, Description = "Great book, highly recommended!", BookBorrowingId = 1 },
					new Review { Id = index++, Rate = 3, Description = "Interesting read, but could be improved.", BookBorrowingId = 2 },
					new Review { Id = index++, Rate = 5, Description = "Absolutely loved it, couldn't put it down!", BookBorrowingId = 3 },
					new Review { Id = index++, Rate = 2, Description = "Not as good as I expected.", BookBorrowingId = 4 },
					new Review { Id = index++, Rate = 5, Description = "An excellent read!", BookBorrowingId = 5 }
				);
			});
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
