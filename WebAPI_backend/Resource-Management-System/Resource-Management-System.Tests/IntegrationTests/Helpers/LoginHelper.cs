using BysinessServices.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Resource_Management_System.Tests.IntegrationTests.Helpers
{
    public class LoginHelper
    {
        public static async Task<string> getAccessTokenAsync(string login, string password, HttpClient httpClient)
        {
            var authInfo = new AuthInfoModel { Login = login, Password = password };
            var authContent = new StringContent(JsonConvert.SerializeObject(authInfo), Encoding.UTF8, "application/json");
            var responseAccessToken = await httpClient.PostAsync("api/user/login", authContent);
            Assert.That(responseAccessToken.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Incorrect login or password");

            var accessToken = await responseAccessToken.Content.ReadAsAsync<JwtPairModel>();
            Assert.That(accessToken, Is.Not.Null);
            Assert.That(accessToken.AccessToken, Is.Not.Empty);

            return accessToken.AccessToken;
        }
    }
}
