using BysinessServices.Interfaces;
using BysinessServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ResourceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        // GET: api/resource
        [HttpGet]
        public async Task<IEnumerable<ResourceModel>> Get()
        {
            return await _resourceService.GetAllAsync();
        }

        //GET: api/resource/user/5
        [HttpGet("user/{id}")]
        [ProducesResponseType(typeof(ScheduleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetScheduleByUserId(int id)
        {
            var schedule = await _resourceService.GetScheduleByUserId();
            return schedule == null ? NotFound() : Ok(schedule);
        }

        //GET: api/resource/schedule/5
        [HttpGet("schedule/{id}")]
        [ProducesResponseType(typeof(ScheduleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetScheduleByResourceId(int id)
        {
            var schedule = await _resourceService.GetScheduleByResourceId();
            return schedule == null ? NotFound() : Ok(schedule);
        }

        // GET: api/resource/5
        [HttpGet("{id}")]
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
            await _resourceService.AddAsync(resource);

            return CreatedAtAction(nameof(GetById), new { id = resource.Id }, resource);
        }

        // PUT: api/resource/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ResourceModel resource)
        {
            if (id != resource.Id) return BadRequest();

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

        // POST: api/resource/type
        [HttpPost("type")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateResourceType(ResourceTypeModel resourceType)
        {
            await _resourceService.AddResourceTypeAsync(resourceType);

            return CreatedAtAction(null, null, resourceType);
        }

        // PUT: api/resource/type/5
        [HttpPut("type/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateResourceType(int id, ResourceTypeModel resourceType)
        {
            if (id != resourceType.Id) return BadRequest();

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
