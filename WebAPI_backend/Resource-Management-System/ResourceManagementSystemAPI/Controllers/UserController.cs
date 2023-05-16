using BysinessServices.Interfaces;
using BysinessServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ResourceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;

        }

        /// <summary>
        /// GET: api/user
        /// Getting a list of all users (without login and password)
        /// </summary>
        /// <returns>list of users without login and password</returns>
        [HttpGet]
        public async Task<IEnumerable<UserProtectedModel>> Get()
        {
            return await _userService.GetAllUserWithoutProtectedInfo();
        }


        /// <summary>
        /// Get: api/user/4
        /// Getting information about user by their id
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>information about user</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserProtectedModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserWithoutProtectedInfoById(id);
            return user is not null ? Ok(user) : NotFound();
        }

        /// <summary>
        /// POST: api/user
        /// Create new user with login and password
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns>result of creating</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(UserUnsafeModel user)
        {
            byte[] hash, salt;
            _authService.CreatePasswordHash(user.Password, out hash, out salt);
            var userWithAuth = _userService.ConvertToUserWithAuth(user, hash, salt);
            var createdUser = await _userService.AddProtectedAsync(userWithAuth);

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// PUT: api/user/12
        /// Change user information
        /// </summary>
        /// <param name="id">id of user to change</param>
        /// <param name="user">new user unformation</param>
        /// <returns>status codes</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UserUnsafeModel user)
        {
            if (id != user.Id) { return BadRequest(); }

            byte[] hash, salt;
            _authService.CreatePasswordHash(user.Password, out hash, out salt);
            var changedUser = _userService.ConvertToUserWithAuth(user, hash, salt);
            await _userService.UpdateAsync(changedUser);

            return NoContent();
        }

        /// <summary>
        /// DELETE: apt/user/delete/15
        /// Delete user by their id
        /// </summary>
        /// <param name="id">id of user to delete</param>
        /// <returns>status code</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await _userService.GetByIdAsync(id)) is null) { return NotFound(); }

            await _userService.DeleteAsync(id);

            return NoContent();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////User Roles
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// GET: api/user/role
        /// Get list of all roles
        /// </summary>
        /// <returns>list of roles</returns>
        [HttpGet("role")]
        public async Task<IEnumerable<RoleModel>> GetRole()
        {
            return await _userService.GetAllRolesAsync();
        }

        /// <summary>
        /// PUT: api/user/role/2
        /// Update information about role in the system
        /// </summary>
        /// <param name="id">id of role</param>
        /// <param name="role">updated information</param>
        /// <returns>the object saved in the system</returns>
        [HttpPut("role/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRole(int id, RoleModel role)
        {
            if (id != role.Id) { return BadRequest(); }

            _userService.UpdateRoleAsync(role);

            return NoContent();
        }

        /// <summary>
        /// POST: api/user/role
        /// Creating new user role
        /// </summary>
        /// <param name="role">new role object</param>
        /// <returns>the object opf role created in the system</returns>
        [HttpPost("role")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRole(RoleModel role)
        {
            await _userService.AddRoleAsync(role);
            return CreatedAtAction(null, null, role);
        }

        /// <summary>
        /// DELETE: api/role/4
        /// Delete the role from system
        /// </summary>
        /// <param name="id">id of role to delete</param>
        /// <returns></returns>
        [HttpDelete("role/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(int id) 
        {
            var rolesToDelete = await _userService.GetAllRolesAsync();

            if (rolesToDelete.FirstOrDefault
                (role => role.Id == id) is null) { return NotFound(); }

            await _userService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
