using MediatR;
using OnlineCoursePlatform.Application.Features.LessonFeature.Dtos;

namespace OnlineCoursePlatform.Application.Features.LessonFeature.Queries.GetLessonById
{
    public record GetLessonByIdQuery(int Id) : IRequest<LessonResponseModel>;
}
