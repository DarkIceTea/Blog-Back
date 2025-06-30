using Microsoft.EntityFrameworkCore;
using UserDomain;

namespace UserInfrastucture.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; }
}