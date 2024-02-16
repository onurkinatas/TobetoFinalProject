using Core.Application.Dtos;

namespace Application.Features.Quizs.Queries.GetList;

public class GetListQuizListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

}