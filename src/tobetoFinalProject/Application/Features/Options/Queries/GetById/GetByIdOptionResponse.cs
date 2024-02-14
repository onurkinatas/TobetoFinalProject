using Core.Application.Responses;

namespace Application.Features.Options.Queries.GetById;

public class GetByIdOptionResponse : IResponse
{
    public int Id { get; set; }
    public string Text { get; set; }
}