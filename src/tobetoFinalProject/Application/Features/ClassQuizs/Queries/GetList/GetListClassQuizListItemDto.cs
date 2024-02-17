using Core.Application.Dtos;

namespace Application.Features.ClassQuizs.Queries.GetList;

public class GetListClassQuizListItemDto : IDto
{
    public int Id { get; set; }
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }
}