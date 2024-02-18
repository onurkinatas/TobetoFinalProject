using Application.Features.Quizs.Queries.GetById;
using Core.Application.Dtos;

namespace Application.Features.ClassQuizs.Queries.GetList;

public class GetListClassQuizListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public GetByIdQuizResponse Quiz { get; set; }
}