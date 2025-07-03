namespace GatewayAPI.Responses;

public record Tokens
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}