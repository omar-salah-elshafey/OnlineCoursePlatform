using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursePlatform.Application.DTOs;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.DeleteEnrollment;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Commands.UpdateEnrollment;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetAllEnrollments;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentById;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByCourseId;
using OnlineCoursePlatform.Application.Features.EnrollmentFeature.Queries.GetEnrollmentsByStudentId;
using OnlineCoursePlatform.Application.Interfaces;

namespace OnlineCoursePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;
        private readonly ILogger<EnrollmentController> _logger;
        public EnrollmentController(IMediator mediator, ICookieService cookieService, ILogger<EnrollmentController> logger)
        {
            _mediator = mediator;
            _cookieService = cookieService;
            _logger = logger;
        }
        [HttpPost("create-enrollment")]
        [Authorize]
        public async Task<IActionResult> CreateEnrollmentAsync(EnrollmentDto enrollmentDto)
        {
            var CurrentUserId = _cookieService.GetFromCookies("userID");
            if (CurrentUserId == null)
                _logger.LogWarning("UserID is null");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new CreateEnrollmentCommand(enrollmentDto, CurrentUserId));
            if(result.Message is not null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllEnrollmentsAsync()
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetAllEnrollmentsQuery());
            if (result is null || result.Count == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("get-by-enrollmentId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllEnrollmentByIdAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetEnrollmentByIdQuery(id));
            if (result.Message is not null)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpGet("get-by-courseId/{courseId}")]
        [Authorize]
        public async Task<IActionResult> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetEnrollmentsByCourseIdQuery(courseId));
            if (result is null || result.Count == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("get-by-studentId/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetEnrollmentsByStudentIdAsync(string studentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new GetEnrollmentsByStudentIdQuery(studentId));
            if (result is null || result.Count == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEnrollmentAsync(int id,  UpdateEnrollmentDto enrollmentDto)
        {
            var CurrentUserId = _cookieService.GetFromCookies("userID");
            if (CurrentUserId == null)
                _logger.LogWarning("UserID is null");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new UpdateEnrollmentCommand(id, enrollmentDto, CurrentUserId!));
            if (result.Message is not null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEnrollmentAsync(int id)
        {
            var CurrentUserId = _cookieService.GetFromCookies("userID");
            if (CurrentUserId == null)
                _logger.LogWarning("UserID is null");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new DeleteEnrollmentCommand(id, CurrentUserId!));
            if (result.Message is not null)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
