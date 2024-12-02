using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.UserManagement.Commands.ChangeRole;
using OnlineCoursePlatform.Application.Features.UserManagement.Commands.DeleteUser;
using OnlineCoursePlatform.Application.Features.UserManagement.Commands.UpdateUser;
using OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsers;
using OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsersByRole;
using OnlineCoursePlatform.Application.Features.UserManagement.Queries.SearchUsersByName;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        public UserManagementController(UserManager<User> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet("get-all-users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            if (users.Count == 0)
                return NotFound("No users found!");
            return Ok(users);
        }

        [HttpGet("get-all-students")]
        [Authorize]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var users = await _mediator.Send(new GetUsersByRoleQuery("Student"));
            if (users.Count == 0)
                return NotFound("No users found with role: Student");
            return Ok(users);
        }

        [HttpGet("get-all-instructors")]
        [Authorize]
        public async Task<IActionResult> GetAllInstructorsAsync()
        {
            var users = await _mediator.Send(new GetUsersByRoleQuery("Instructor"));
            if (users.Count == 0)
                return NotFound("No users found with role: Instructor");
            return Ok(users);
        }

        [HttpGet("search-users-by-name")]
        [Authorize]
        public async Task<IActionResult> SearchUsersByNameAsync([FromQuery] string keyword)
        {
            var users = await _mediator.Send(new SearchUsersByNameQuery(keyword));
            if (users.Count == 0)
                return NotFound($"No users found with keyword: {keyword}");
            return Ok(users);
        }

        [HttpPut("change-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleAsync(ChangeUserRoleDto changeUserRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new ChangeRoleCommand(changeUserRoleDto));
            return Ok(result);
        }

        [HttpDelete("delete-user")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(string UserName)
        {
            var CurrentUserName = Request.Cookies["UserName"];
            var refreshToken = Request.Cookies["refreshToken"];
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new DeleteUserCommand(UserName, CurrentUserName, refreshToken));
            return Ok(result);
        }

        [HttpPut("update-user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new UpdateUserCommand(updateUserDto));
            return Ok(result);
        }
    }
}
