using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatGPTClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    SecurityStamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedByIp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc6b020c-69a7-4335-a2a1-80b24748d266", "AQAAAAIAAYagAAAAECq9An1YvgQjpZNjswgXBBpdo+Uvr2EdUpogxk7F5HFkvpPiW8F2qoo0PO1C1TN2rw==", "f8735293-ebb3-44b4-a613-c35ca2cb72ce" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId_Token",
                table: "RefreshTokens",
                columns: new[] { "AppUserId", "Token" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0415856b-375f-40bb-99c9-c8ffc5fc67d5", "AQAAAAIAAYagAAAAEOOi/RN6gW6dtMHytYt+g/hTDsoc3WTb64DMb/Y9adCU3vLg7EimFS0poOWcYFIEJQ==", "6c079e71-127f-4d80-8f47-d1ff75e43042" });
        }
    }
}
