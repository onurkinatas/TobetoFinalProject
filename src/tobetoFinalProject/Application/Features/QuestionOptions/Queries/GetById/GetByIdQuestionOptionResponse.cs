using Core.Application.Responses;

namespace Application.Features.QuestionOptions.Queries.GetById;

public class GetByIdQuestionOptionResponse : IResponse
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
}