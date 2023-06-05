using BysinessServices.Models;
using DataAccess.Entities;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Resource_Management_System.Tests.IntegrationTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Resource_Management_System.Tests.IntegrationTests
{
    [TestFixture]
    public class RequestIntegrationTests
    {
        private HttpClient _httpClient;
        private CustomWebApplicationFactory _factory;
        private string _requestUri = "api/request/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _httpClient = _factory.CreateClient();
        }

        [Test]
        public async Task RequestController_Get_ShouldReturn200()
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri);
            var requests = await response.Content.ReadAsAsync<RequestModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests, Is.Not.Empty);
        }

        [TestCase(2)]
        [TestCase(3)]
        public async Task RequestController_GetRequestByUserId_ShouldReturn200(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + "user/" + id);
            var requests = await response.Content.ReadAsAsync<RequestModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests, Is.Not.Empty);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task RequestController_GetById_ShouldReturn200(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + id);
            var request = await response.Content.ReadAsAsync<RequestModel>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(request, Is.Not.Null);
        }

        [TestCase(67676)]
        [TestCase(867864)]
        public async Task RequestController_GetById_ShouldReturn404(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + id);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task RequestController_Create_ShouldReturn201AndGeneratedId()
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var request = new RequestModel { ResourceId = 1, UserId = 3, Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(5), Purpose = "TestPurpose" };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri, content);
            var actyalRequest = await response.Content.ReadAsAsync<RequestModel>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actyalRequest, Is.Not.Null);
            Assert.That(actyalRequest.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task RequestController_Create_ShouldReturn403()
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);
            var request = new RequestModel { ResourceId = 1, UserId = 1, Start = DateTime.Now, End = DateTime.Now.AddDays(5) };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri, content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [TestCase(3)]
        public async Task RequestController_Delete_ShouldReturn204(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.DeleteAsync(_requestUri + id);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task RequestController_Delete_ShouldReturn403(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.DeleteAsync(_requestUri + id);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [TestCase(67676)]
        [TestCase(867864)]
        public async Task RequestController_Delete_ShouldReturn404(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.DeleteAsync(_requestUri + id);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task RequestController_Deny_ShouldReturn204(int id)
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);

            // Act
            var response = await _httpClient.DeleteAsync(_requestUri + "deny/" + id);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task RequestController_Confirm_ShouldReturn204(int id)
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);

            // Act
            var response = await _httpClient.PutAsync(_requestUri + "confirm/" + id, null);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _httpClient.Dispose();
        }
    }
}
