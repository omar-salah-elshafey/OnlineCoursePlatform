using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
