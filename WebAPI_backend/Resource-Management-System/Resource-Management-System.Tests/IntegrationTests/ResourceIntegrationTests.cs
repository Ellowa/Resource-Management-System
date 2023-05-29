using BysinessServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json;
using Resource_Management_System.Tests.IntegrationTests.Helpers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Resource_Management_System.Tests.IntegrationTests
{
    [TestFixture]
    public class ResourceIntegrationTests
    {
        private HttpClient _httpClient;
        private CustomWebApplicationFactory _factory;
        private string _requestUri = "api/resource/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _httpClient = _factory.CreateClient();
        }

        [Test]
        public async Task ResourceController_Get_ShouldReturn200()
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri);
            var resources = await response.Content.ReadAsAsync<ResourceModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resources, Is.Not.Null);
            Assert.That(resources, Is.Not.Empty);
        }

        [TestCase(1)]
        public async Task ResourceController_GetWithDetails_ShouldReturn200(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + "details");
            var resources = await response.Content.ReadAsAsync<ResourceModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resources, Is.Not.Null);
            Assert.That(resources, Is.Not.Empty);
            Assert.That(resources.FirstOrDefault(r => r.Id == id).Requests, Is.Not.Empty);
        }

        [Test]
        public async Task ResourceController_GetScheduleByUserId_ShouldReturn200()
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + "user/3");
            var schedules = await response.Content.ReadAsAsync<ScheduleModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(schedules, Is.Not.Null);
            Assert.That(schedules, Is.Not.Empty);
        }

        [Test]
        public async Task ResourceController_GetScheduleByResourceId_ShouldReturn200()
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + "schedule/2");
            var schedules = await response.Content.ReadAsAsync<ScheduleModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(schedules, Is.Not.Null);
            Assert.That(schedules, Is.Not.Empty);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task ResourceController_GetById_ShouldReturn200(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + id);
            var resource = await response.Content.ReadAsAsync<ResourceModel>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resource, Is.Not.Null);
        }

        [TestCase(67676)]
        [TestCase(867864)]
        public async Task ResourceController_GetById_ShouldReturn404(int id)
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
        public async Task ResourceController_Create_ShouldReturn201AndGeneratedId()
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);
            var resource = new ResourceModel { Name = "TestNew", SerialNumber = "TestNew", ResourceTypeId = 1};
            var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri, content);
            var actyalResource = await response.Content.ReadAsAsync<ResourceModel>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actyalResource, Is.Not.Null);
            Assert.That(actyalResource.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task ResourceController_Create_ShouldReturn400ValidationProblem()
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);
            var resource = new ResourceModel { Name = "T", ResourceTypeId = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBody, Does.Contain("One or more validation errors occurred"));
        }

        [Test]
        public async Task ResourceController_Create_ShouldReturn401Unauthorized()
        {
            // Arrange
            var resource = new ResourceModel { Name = "TestNew", SerialNumber = "TestNew", ResourceTypeId = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri, content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task ResourceController_Create_ShouldReturn403Forbidden()
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);
            var resource = new ResourceModel { Name = "T", ResourceTypeId = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri, content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [TestCase(1)]
        public async Task ResourceController_Update_ShouldReturn204(int id)
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);
            var resourceToUpdate = new ResourceModel {Id = id, Name = "TestUpdate", SerialNumber = "TestUpdate", ResourceTypeId = 2 };
            var content = new StringContent(JsonConvert.SerializeObject(resourceToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(_requestUri + id, content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [TestCase(1)]
        public async Task ResourceController_Delete_ShouldReturn204(int id)
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);

            // Act
            var response = await _httpClient.DeleteAsync(_requestUri + id);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task ResourceController_GetResourceType_ShouldReturn200()
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + "type");
            var resourceTypes = await response.Content.ReadAsAsync<ResourceTypeModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resourceTypes, Is.Not.Null);
            Assert.That(resourceTypes, Is.Not.Empty);
        }

        [TestCase(1)]
        public async Task ResourceController_GetResourceTypeWithDetails_ShouldReturn200(int id)
        {
            // Arrange
            var userAccessToken = await LoginHelper.getAccessTokenAsync("User_test", "User_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccessToken);

            // Act
            var response = await _httpClient.GetAsync(_requestUri + "type/details");
            var resourceTypes = await response.Content.ReadAsAsync<ResourceTypeModel[]>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resourceTypes, Is.Not.Null);
            Assert.That(resourceTypes, Is.Not.Empty);
            Assert.That(resourceTypes.FirstOrDefault(r => r.Id == id).Resources, Is.Not.Empty);
        }

        [Test]
        public async Task ResourceController_CreateResourceType_ShouldReturn201AndGeneratedId()
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);
            var resourceType = new ResourceTypeModel { TypeName = "NewType" };
            var content = new StringContent(JsonConvert.SerializeObject(resourceType), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_requestUri + "type", content);
            var actyalResourceType = await response.Content.ReadAsAsync<ResourceTypeModel>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actyalResourceType, Is.Not.Null);
            Assert.That(actyalResourceType.Id, Is.Not.EqualTo(0));
        }

        [TestCase(1)]
        public async Task ResourceController_UpdateResourceType_ShouldReturn204(int id)
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);
            var resourceToUpdate = new ResourceTypeModel { Id = id, TypeName = "TestUpdate" };
            var content = new StringContent(JsonConvert.SerializeObject(resourceToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(_requestUri + "type/" + id, content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [TestCase(1)]
        public async Task ResourceController_DeleteResourceType_ShouldReturn204(int id)
        {
            // Arrange
            var managerAccessToken = await LoginHelper.getAccessTokenAsync("Manager_test", "Manager_test", _httpClient);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managerAccessToken);

            // Act
            var response = await _httpClient.DeleteAsync(_requestUri + "type/" + id);

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
