using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers;
// DTO для запроса регистрации (тот же, что мы обсуждали ранее)
public class RegistrationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public DateTime DateOfBirth { get; set; }
}

// DTO для ответа от UserService
public class UserCreationResponse
{
    public Guid UserId { get; set; }
}
[Controller]
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
                    JsonSerializer.Serialize(new { request.Email, request.Password, request.UserRole, request.UserName }),
                    Encoding.UTF8, "application/json");

                var userResponse = await httpClient.PostAsync($"{userServiceUrl}/api/users", userContent);

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
                    JsonSerializer.Serialize(new { UserId = newUserId, FirstName = request.UserName, Email = request.Email }),
                    Encoding.UTF8, "application/json");
                
                var profileResponse = await httpClient.PostAsync($"{profileServiceUrl}/api/profiles", profileContent);

                if (!profileResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to create profile."); 
                }
                return Ok(new { UserId = newUserId, Message = "User and profile created successfully." });
            }
            catch (Exception)
            {
                // Rollback: Delete the user if profile creation fails
                if (newUserId.HasValue)
                {
                    var userServiceUrl = _configuration["UserService:BaseUrl"];
                    await httpClient.DeleteAsync($"{userServiceUrl}/api/users/{newUserId.Value}");
                }
                return StatusCode(500, "An error occurred during registration. The operation has been rolled back.");
            }
        }
}