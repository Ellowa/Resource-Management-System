using BysinessServices.Interfaces;
using BysinessServices.Models;
using DataAccess.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Security.AccessControl;

namespace ResourceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly IValidator<ResourceTypeModel> _resourceTypeValidator;
        private readonly IValidator<ResourceModel> _resourceValidator;

        public ResourceController(IResourceService resourceService, IValidator<ResourceTypeModel> resourceTypeValidator, IValidator<ResourceModel> resourceValidator)
        {
            _resourceService = resourceService;
            _resourceTypeValidator = resourceTypeValidator;
            _resourceValidator = resourceValidator;
        }

        // GET: api/resource
        [HttpGet]
        public async Task<IEnumerable<ResourceModel>> Get()
        {
            return await _resourceService.GetAllAsync(r => r.ResourceType);
        }

        // GET: api/resource/details
        [HttpGet("details")]
        public async Task<IEnumerable<ResourceModel>> GetWithDetails()
        {
            return await _resourceService.GetAllAsync(r => r.ResourceType, r => r.Schedules, r => r.Requests);
        }

        //GET: api/resource/user/5
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<ScheduleModel>> GetScheduleByUserId(int id)
        {
            return await _resourceService.GetScheduleByUserId(id);
        }

        //GET: api/resource/schedule/5
        [HttpGet("schedule/{id}")]
        public async Task<IEnumerable<ScheduleModel>> GetScheduleByResourceId(int id)
        {
            return await _resourceService.GetScheduleByResourceId(id);
        }

        // GET: api/resource/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResourceModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var resource = await _resourceService.GetByIdAsync(id);
            return resource == null ? NotFound() : Ok(resource);
        }

        // POST: api/resource
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ResourceModel resource)
        {
            var validationResult = await _resourceValidator.ValidateAsync(resource);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return ValidationProblem(validationResult.Errors.ToString());
            }

            var createdResource = await _resourceService.AddAsync(resource);

            return CreatedAtAction(nameof(GetById), new { id = createdResource.Id }, createdResource);
        }

        // PUT: api/resource/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ResourceModel resource)
        {
            var validationResult = await _resourceValidator.ValidateAsync(resource);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return ValidationProblem(validationResult.Errors.ToString());
            }

            await _resourceService.UpdateAsync(resource);

            return NoContent();
        }

        // DELETE: api/resource/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var resourceToDelete = await _resourceService.GetByIdAsync(id);
            if (resourceToDelete == null) return NotFound();

            await _resourceService.DeleteAsync(id);

            return NoContent();
        }


        // GET: api/resource/type
        [HttpGet("type")]
        public async Task<IEnumerable<ResourceTypeModel>> GetResourceType()
        {
            return await _resourceService.GetAllResourceTypesAsync();
        }

        // GET: api/resource/type/details
        [HttpGet("type/details")]
        public async Task<IEnumerable<ResourceTypeModel>> GetResourceTypeWithDetails()
        {
            return await _resourceService.GetAllResourceTypesAsync(rt => rt.Resources);
        }

        // POST: api/resource/type
        [HttpPost("type")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateResourceType(ResourceTypeModel resourceType)
        {
            var validationResult = await _resourceTypeValidator.ValidateAsync(resourceType);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return ValidationProblem(validationResult.Errors.ToString());
            }

            var createdResourceType = await _resourceService.AddResourceTypeAsync(resourceType);

            return CreatedAtAction(null, new { id = createdResourceType.Id }, createdResourceType);
        }

        // PUT: api/resource/type/5
        [HttpPut("type/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateResourceType(int id, ResourceTypeModel resourceType)
        {
            var validationResult = await _resourceTypeValidator.ValidateAsync(resourceType);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return ValidationProblem(validationResult.Errors.ToString());
            }

            await _resourceService.UpdateResourceTypeAsync(resourceType);

            return NoContent();
        }

        // DELETE: api/resource/type/5
        [HttpDelete("type/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteResourceType(int id)
        {
            var resourceTypesToDelete = await _resourceService.GetAllResourceTypesAsync();
            var resourceTypeToDelete = resourceTypesToDelete.FirstOrDefault(rt => rt.Id == id);
            if (resourceTypeToDelete == null) return NotFound();

            await _resourceService.RemoveResourceTypeAsync(id);

            return NoContent();
        }
    }
}
