﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EBookMaster.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishingHouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PublishingHouseId = table.Column<int>(type: "int", nullable: false),
                    PublicationYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeriesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_PublishingHouses_PublishingHouseId",
                        column: x => x.PublishingHouseId,
                        principalTable: "PublishingHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LibraryCardNumber = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => new { x.BooksId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BookCategory_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookBorrowings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBorrowings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BookBorrowingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_BookBorrowings_BookBorrowingId",
                        column: x => x.BookBorrowingId,
                        principalTable: "BookBorrowings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Name", "Nationality", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "American", "Doe" },
                    { 2, new DateTime(1975, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "British", "Smith" },
                    { 3, new DateTime(1990, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Canadian", "Johnson" },
                    { 4, new DateTime(1985, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily", "Australian", "Brown" },
                    { 5, new DateTime(1970, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pierre", "French", "Davis" },
                    { 6, new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hans", "German", "Müller" },
                    { 7, new DateTime(1988, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Francesca", "Italian", "Bianchi" },
                    { 8, new DateTime(1965, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos", "Spanish", "García" },
                    { 9, new DateTime(1978, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sakura", "Japanese", "Yamamoto" },
                    { 10, new DateTime(1983, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arjun", "Indian", "Patel" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Narrative literature created from the imagination.", "Fiction" },
                    { 2, "Literature based on facts, real events, and real people.", "Non-Fiction" },
                    { 3, "Literature dealing with futuristic settings and advanced technologies.", "Science Fiction" },
                    { 4, "Literature featuring magical and supernatural elements.", "Fantasy" },
                    { 5, "Literature detailing the life of a real person.", "Biography" }
                });

            migrationBuilder.InsertData(
                table: "PublishingHouses",
                columns: new[] { "Id", "Country", "FoundationDate", "Name" },
                values: new object[,]
                {
                    { 1, "United States", new DateTime(2013, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Penguin Random House" },
                    { 2, "United States", new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HarperCollins" },
                    { 3, "United States", new DateTime(1924, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simon & Schuster" },
                    { 4, "United Kingdom", new DateTime(1843, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macmillan Publishers" },
                    { 5, "France", new DateTime(1826, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hachette Livre" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "The Lord of the Rings" },
                    { 2, "Harry Potter" },
                    { 3, "A Song of Ice and Fire" },
                    { 4, "The Chronicles of Narnia" },
                    { 5, "Sherlock Holmes" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "Period", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 1, 9.99m, 1 },
                    { 2, 1, 14.99m, 2 },
                    { 3, 2, 100m, 1 },
                    { 4, 2, 150m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "PublicationYear", "PublishingHouseId", "SeriesId", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "The Fellowship of the Ring" },
                    { 2, new DateTime(1954, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "The Two Towers" },
                    { 3, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Harry Potter and the Philosopher's Stone" },
                    { 4, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Harry Potter and the Chamber of Secrets" },
                    { 5, new DateTime(1988, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "A Brief History of Time" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LibraryCardNumber", "Name", "Password", "RefreshToken", "RefreshTokenExpiration", "Role", "Salt", "SubscriptionId", "Surname" },
                values: new object[] { 1, "edward.norton@example.com", 1, "Edward", "j45qsKROWKfK0uVI9jMPE715FxLsHJlFjy763a1J87I=", null, null, 1, "CTQnUwx4k4fhjcapvGoAlh96jMgz6zUHCfF6W7GH3oc=", 4, "Norton" });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "BookBorrowings",
                columns: new[] { "Id", "BookId", "BorrowingDate", "ReturnDate", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 3, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 4, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 2, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, 1, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "BookCategory",
                columns: new[] { "BooksId", "CategoriesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookBorrowingId", "Description", "Rate" },
                values: new object[,]
                {
                    { 1, 1, "Great book, highly recommended!", 4 },
                    { 2, 2, "Interesting read, but could be improved.", 3 },
                    { 3, 3, "Absolutely loved it, couldn't put it down!", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_BookId",
                table: "BookBorrowings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowings_UserId",
                table: "BookBorrowings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_CategoriesId",
                table: "BookCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublishingHouseId",
                table: "Books",
                column: "PublishingHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SeriesId",
                table: "Books",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookBorrowingId",
                table: "Reviews",
                column: "BookBorrowingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionId",
                table: "Users",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "BookBorrowings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PublishingHouses");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}