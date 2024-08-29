using ChatGPTClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatGPTClone.Infrastructure.Persistence.Seeders;

public class UserSeeder : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var initialUserId = Guid.Parse("2798212b-3e5d-4556-8629-a64eb70da4a8");

        var initialUser = new AppUser
        {
            Id = initialUserId,
            UserName = "cengizilkerli",
            NormalizedUserName = "CENGIZILKERLI",
            Email = "cengizilkerli@gmail.com",
            NormalizedEmail = "CENGIZILKERLI@GMAIL.COM",
            EmailConfirmed = true,
            CreatedByUserId = initialUserId.ToString(),
            CreatedOn = new DateTimeOffset(new DateTime(2024, 08, 28)),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = "Cengiz",
            LastName = "İlkerli",
            LockoutEnabled = false,
            AccessFailedCount = 0
        };

        initialUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(initialUser, "123cengizilkerli123");

        builder.HasData(initialUser);
    }
}