using Core.Application.Responses;

namespace Application.Features.Appeals.Queries.GetById;

public class GetByIdAppealResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}