using Core.Application.Responses;

namespace Application.Features.SubTypes.Queries.GetById;

public class GetByIdSubTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}