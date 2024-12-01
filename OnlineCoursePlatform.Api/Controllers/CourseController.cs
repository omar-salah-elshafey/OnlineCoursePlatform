using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.CourseFeature.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.CourseFeature.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.Features.CourseFeature.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetAllCourses;
using OnlineCoursePlatform.Application.Features.CourseFeature.Queries.GetCourseById;
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

        [HttpPost]
        [Authorize(Roles ="Admin, Instructor")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
        {
            // Validate the courseDto first if needed
            if (courseDto == null)
            {
                return BadRequest("Course data is required.");
            }
            var userId = _cookieService.GetFromCookies("userID");
            // Send the command to create the course
            var result = await _mediator.Send(new CreateCourseCommand(courseDto, userId));

            if (result.Message is not null)
            {
                return NotFound(result.Message);  // Course not found
            }

            return Ok(result); // Return 201 Created with course details
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _mediator.Send(new GetAllCoursesQuery());

            if (courses == null || courses.Count == 0)
            {
                return NoContent(); // Return 204 if no courses found
            }

            return Ok(courses); // Return 200 with the list of courses
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _mediator.Send(new GetCourseByIdQuery(id));

            if (course == null)
            {
                return NotFound($"Course with ID: {id} is not found."); // Return 404 if course not found
            }

            return Ok(course); // Return 200 with the course data
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id,[FromBody] UpdateCourseDto updateCourseDto)
        {
            
            var result = await _mediator.Send(new UpdateCourseCommand(id, updateCourseDto));
            if (result.Message is not null)
            {
                return NotFound(result.Message);  // Course not found
            }

            return Ok(result);  // Return updated course
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand(id));

            if (!result)
                return NotFound($"Course with ID: {id} is not found."); // Successfully deleted
            return Ok(result);
        }
    }
}
