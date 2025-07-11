namespace BlogInfrastructure.Dto;

public record TagDto()
{
    public Guid Id { get; init; }
    public string Name { get; init; }
};