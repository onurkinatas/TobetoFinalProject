using Core.Application.Responses;

namespace Application.Features.Tags.Queries.GetById;

public class GetByIdTagResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}