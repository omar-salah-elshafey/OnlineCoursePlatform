using MediatR;
using OnlineCoursePlatform.Application.Models;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonById
{
    public record GetLessonByIdQuery(int Id) : IRequest<LessonResponseModel>;
}
