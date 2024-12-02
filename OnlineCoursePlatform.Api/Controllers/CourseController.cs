using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.CourseFeature.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.CourseFeature.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.Features.CourseFeature.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetAllCourses;
using OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCourseById;
using OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCoursesByInstructorId;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;

        public CourseController(IMediator mediator, ICookieService cookieService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
        }

        [HttpPost("add-course")]
        [Authorize(Roles ="Admin, Instructor")]
        public async Task<IActionResult> CreateCourseAsync([FromBody] CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data is required.");
            }
            var userId = _cookieService.GetFromCookies("userID");
            var result = await _mediator.Send(new CreateCourseCommand(courseDto, userId));

            if (result.Message is not null)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var courses = await _mediator.Send(new GetAllCoursesQuery());

            if (courses == null || courses.Count == 0)
            {
                return NoContent();
            }

            return Ok(courses);
        }

        [HttpGet("get-by-courseId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCourseByIdAsync(int id)
        {
            var course = await _mediator.Send(new GetCourseByIdQuery(id));

            if (course.Message is not null)
            {
                return NotFound(course.Message);
            }

            return Ok(course);
        }

        [HttpGet("get-by-instructorId/{instructorId}")]
        [Authorize]
        public async Task<IActionResult> GetCoursesByInstructorIdAsync(string instructorId)
        {
            var courses = await _mediator.Send(new GetCoursesByInstructorIdQuery(instructorId));

            if (courses == null || courses.Count == 0)
            {
                return NoContent();
            }

            return Ok(courses);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> UpdateCourse(int id,[FromBody] UpdateCourseDto updateCourseDto)
        {
            var CurresntUserId = _cookieService.GetFromCookies("userID");
            var result = await _mediator.Send(new UpdateCourseCommand(id, updateCourseDto, CurresntUserId));
            if (result.Message is not null)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var CurresntUserId = _cookieService.GetFromCookies("userID");
            var result = await _mediator.Send(new DeleteCourseCommand(id, CurresntUserId));

            if (!result.IsDeleted)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
