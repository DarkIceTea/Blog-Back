using UserDomain;

namespace UserApplication.Abstractions;

public interface IUserService
{
    public Task<Profile> GetUserByIdAsync(Guid id);
    public Task<List<Profile>> GetUsersAsync();
    public Task<Profile> CreateUserAsync(Profile profile);
    public Task<Profile> UpdateUserAsync(Profile profile);
    public Task DeleteUserAsync(Guid id);
}