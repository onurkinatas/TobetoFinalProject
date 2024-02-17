using Application.Features.Quizs.Queries.GetById;
using Application.Features.Quizs.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.GeneralQuizs.Queries.GetList;

public class GetListGeneralQuizListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public GetByIdQuizResponse Quiz { get; set; }
}