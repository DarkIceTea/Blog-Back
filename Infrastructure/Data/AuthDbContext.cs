using System;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<CustomUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomUser>()
                .HasOne(x => x.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<CustomUser>(u => u.RefreshTokenId);

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>() { Id = Guid.Parse("9ab095e1-e01f-4bd1-aa0f-07aaab5fa60f"), Name = "User", NormalizedName = "USER" },
                                                            new IdentityRole<Guid>() { Id = Guid.Parse("bf7aeb36-e186-49ba-9384-433745675d71"), Name = "Editor", NormalizedName = "EDITOR" },
                                                            new IdentityRole<Guid>() { Id = Guid.Parse("48d7ea2e-247c-4eed-a62f-8ffbebf251a3"), Name = "Admin", NormalizedName = "ADMIN" });
        }
    }
}
