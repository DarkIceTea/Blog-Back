namespace UserDomain;

public record Profile
{
    public Guid Id { get; init; } 
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string PathToAvatar { get; init; } = string.Empty;
};