using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.CreateModule;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.DeleteModule;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Commands.UpdateModule;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetAllModules;
using OnlineCoursePlatform.Application.Features.ModuleFeature.Queries.GetModuleById;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;
        public ModulesController(IMediator mediator, ICookieService cookieService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
        }

        [HttpPost("add-Module")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> AddModuleAsync(ModuleDto moduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(new CreateModuleCommand(moduleDto));
            if (result.Message is not null)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllModulesAsync()
        {
            var result =await _mediator.Send(new GetAllModuleQuery());
            if (result is null || !result.Any())
                return NoContent();
            return Ok(result);
        }

        [HttpGet("({id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send( new GetModuleByIdQuery(id));
            if (result.Message != null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> UpdateModuleAsync(int id, UpdateModuleDto moduleDto)
        {
            var CurresntUserId = _cookieService.GetFromCookies("userID");
            var result = await _mediator.Send(new UpdateModuleCommand(id, moduleDto, CurresntUserId));
            if (result.Message is not null) 
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteModuleAsync(int id)
        {
            var CurresntUserId = _cookieService.GetFromCookies("userID");
            var result = await _mediator.Send(new DeleteModuleCommand(id, CurresntUserId));
            if (!result.IsDeleted)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
