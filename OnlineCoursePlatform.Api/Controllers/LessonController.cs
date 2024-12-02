using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.Features.LessonFeature.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Features.LessonFeature.Commands.DeleteLesson;
using OnlineCoursePlatform.Application.Features.LessonFeature.Commands.UpdateLesson;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;
using OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetAllLessons;
using OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonById;
using OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonsByModuleId;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;
        public LessonController(IMediator mediator, ICookieService cookieService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
        }

        [HttpPost("add-lesson")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> CreateLessonAsync(LessonDto lessonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new CreateLessonCommand(lessonDto));
            if (result.Message is not null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllLessonsAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetAllLessonsQuery());
            if (result == null || result.Count == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("get-by-lessonId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetLessonByIdAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetLessonByIdQuery(id));
            if (result.Message is not null)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpGet("get-by-moduleId/{moduleId}")]
        [Authorize]
        public async Task<IActionResult> GetLessonsByModuleIdAsync(int moduleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetLessonsByModuleIdQuery(moduleId));
            if (result == null || result.Count == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin, Instructor")]
        public async Task<IActionResult> UpdateLessonAsync(int id, LessonDto lessonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new UpdateLessonCommand(id, lessonDto));
            if (result.Message is not null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteLessonAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new DeleteLessonCommand(id));
            if (!result.IsDeleted)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
