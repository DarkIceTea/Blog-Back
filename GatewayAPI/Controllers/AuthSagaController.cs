using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using GatewayAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers;
public record LoginRequest(string Email, string Password);
public class RegistrationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
public class UserCreationResponse
{
    public Guid UserId { get; set; }
    public Tokens Tokens { get; set; }
}

[ApiController]
[Route("api")]
public class AuthSagaController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AuthSagaController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient();
        Guid? newUserId = null;

        try
        {
            // Step 1: Create user in UserService
            var userServiceUrl = _configuration["UserService:BaseUrl"];
            var userContent = new StringContent(
                JsonSerializer.Serialize(new { request.Email, request.Password, UserRole = "User", request.UserName }),
                Encoding.UTF8, "application/json");

            var userResponse = await httpClient.PostAsync($"{userServiceUrl}/register", userContent);

            if (!userResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)userResponse.StatusCode, await userResponse.Content.ReadAsStringAsync());
            }

            var userCreationResponse = JsonSerializer.Deserialize<UserCreationResponse>(
                await userResponse.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            newUserId = userCreationResponse.UserId;

            //step 2: Create profile in ProfileService
            var profileServiceUrl = _configuration["ProfileService:BaseUrl"];
            var profileContent = new StringContent(
                JsonSerializer.Serialize(new
                {
                    Id = newUserId, FirstName = request.UserName, Email = request.Email,
                    DateOfBirth = request.DateOfBirth
                }),
                Encoding.UTF8, "application/json");

            var profileResponse = await httpClient.PostAsync($"{profileServiceUrl}/api/user", profileContent);

            if (!profileResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create profile.");
            }

            return Ok(new
            {
                Message = "User and profile created successfully.",
                Tokens = userCreationResponse.Tokens,
                Profile = JsonSerializer.Deserialize<Profile>(profileResponse.Content.ReadAsStringAsync().Result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            });
        }
        catch (Exception ex)
        {
            // Rollback: Delete the user if profile creation fails
            if (newUserId.HasValue)
            {
                var userServiceUrl = _configuration["UserService:BaseUrl"];
                //await httpClient.DeleteAsync($"{userServiceUrl}/api/users/{newUserId.Value}"); //TODO: make delete method in AuthService
            }

            return StatusCode(500,
                "An error occurred during registration. The operation has been rolled back." + ex.Message);
        }
    }
     [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient();
        Guid? newUserId = null;

        try
        {
            // Step 1: Get Tokens form AuthService
            var userServiceUrl = _configuration["UserService:BaseUrl"];
            var userContent = new StringContent(
                JsonSerializer.Serialize(new { request.Email, request.Password}),
                Encoding.UTF8, "application/json");

            var authResponse = await httpClient.PostAsync($"{userServiceUrl}/login", userContent);

            if (!authResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)authResponse.StatusCode, await authResponse.Content.ReadAsStringAsync());
            }

            var tokensToSend = JsonSerializer.Deserialize<Tokens>(
                await authResponse.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //step 2: Get profile from UserService
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(tokensToSend.AccessToken))
                throw new Exception("Invalid access token.");
            var jwtToken = tokenHandler.ReadJwtToken(tokensToSend.AccessToken);
            Guid id = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
            
            var profileServiceUrl = _configuration["ProfileService:BaseUrl"];
            
            var profileResponse = await httpClient.GetAsync($"{profileServiceUrl}/api/user/{id}");

            if (!profileResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create profile.");
            }

            return Ok(new
            {
                Message = "Login successful.",
                Tokens = tokensToSend,
                Profile = JsonSerializer.Deserialize<Profile>(profileResponse.Content.ReadAsStringAsync().Result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                "An Error Occured during logging" + ex.Message);
        }
    }
}