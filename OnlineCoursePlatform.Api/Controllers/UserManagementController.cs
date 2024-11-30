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

        [HttpGet("get-users")]
        [Authorize]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            if (users.Count == 0)
                return NotFound("No users found!");
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
