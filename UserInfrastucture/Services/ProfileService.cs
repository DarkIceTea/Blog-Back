using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions;
using UserDomain;
using UserInfrastucture.Data;

namespace UserInfrastucture.Services;

public class ProfileService : IProfileService
{
    UserDbContext _context;
    public ProfileService(UserDbContext context)
    {
        _context = context;
    }
    public async Task<Profile> GetUserByIdAsync(Guid id)
    {
        return await _context.Profiles.FindAsync(id);
    }
    public async Task<List<Profile>> GetUsersAsync()
    {
        return await _context.Profiles.ToListAsync();
    }
    public async Task<Profile> CreateUserAsync(Profile profile)
    {
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
    public async Task<Profile> UpdateUserAsync(Profile profile)
    {
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
    public async Task DeleteUserAsync(Guid id)
    {
        var profile = await _context.Profiles.FindAsync(id);
        if (profile != null)
        {
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}