using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EaseTrail.WebApp.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // 2. Simule um login com credenciais inválidas
            var loginRequest = new
            {
                Email_UserName = "user@example.com",
                Password = "wrong_password"
            };

            var response = await _client.PostAsJsonAsync("/api/user/login", loginRequest);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode); // Espera-se uma resposta 401 Unauthorized
        }

        [Fact]
        public async Task Login_InactiveUser_ReturnsForbidden()
        {
            // 3. Simule um login com um usuário inativo
            var loginRequest = new
            {
                Email_UserName = "inactive_user@example.com",
                Password = "password"
            };

            var response = await _client.PostAsJsonAsync("/api/user/login", loginRequest);

            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode); // Espera-se uma resposta 403 Forbidden
        }
    }
}