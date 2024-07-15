using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movementProj.Server.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private HttpClient _httpClient;
        public UsersController(ILogger<UsersController> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClient = httpClient.CreateClient();
        }

        [HttpGet("getUsers/{page}")]
        public async Task<IActionResult> GetUsers(int page)
        {
            _logger.LogInformation("GetUsers called with page: {Page}", page);
            try
            {
                var users = await _httpClient.GetFromJsonAsync<UserResponse>($"https://reqres.in/api/users?page={page}");
                _logger.LogInformation("GetUsers successful for page: {Page}", page);
                return Ok(users?.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting users for page: {Page}", page);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogInformation("GetUser called with id: {Id}", id);
            try
            {
                var user = await _httpClient.GetFromJsonAsync<UserDetailResponse>($"https://reqres.in/api/users/{id}");
                if (user != null)
                {
                    _logger.LogInformation("GetUser successful for id: {Id}", id);
                    return Ok(user.Data);
                }
                _logger.LogWarning("User not found for id: {Id}", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user for id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest newUser)
        {
            _logger.LogInformation("CreateUser called");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://reqres.in/api/users", newUser);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("CreateUser successful");
                    return Ok(await response.Content.ReadFromJsonAsync<CreateUserResponse>());
                }
                _logger.LogWarning("CreateUser failed");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest updatedUser)
        {
            _logger.LogInformation("UpdateUser called with id: {Id}", id);
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://reqres.in/api/users/{id}", updatedUser);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("UpdateUser successful for id: {Id}", id);
                    return NoContent();
                }
                _logger.LogWarning("UpdateUser failed for id: {Id}", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user for id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("DeleteUser called with id: {Id}", id);
            try
            {
                var response = await _httpClient.DeleteAsync($"https://reqres.in/api/users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("DeleteUser successful for id: {Id}", id);
                    return NoContent();
                }
                _logger.LogWarning("DeleteUser failed for id: {Id}", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user for id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
