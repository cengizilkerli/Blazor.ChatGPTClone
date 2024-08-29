using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatGPTClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSeederUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedByUserId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedByUserId", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"), 0, "a92a70a0-bdf6-4cf6-96e4-3a75087c144a", "2798212b-3e5d-4556-8629-a64eb70da4a8", new DateTimeOffset(new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "cengizilkerli@gmail.com", true, "Cengiz", "İlkerli", false, null, null, null, "CENGIZILKERLI@GMAIL.COM", "CENGIZILKERLI", "AQAAAAIAAYagAAAAENSekyAUuyOupnj1/9b5TWzfQtYspXHkSiXFpjAHNyHC6FjlJaHXVYhk/mXkHa3v5g==", null, false, "00670702-b7c7-465e-a2bb-4b7892421a6d", false, "cengizilkerli" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"));
        }
    }
}
