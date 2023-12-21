using Core.Application.Responses;

namespace Application.Features.Stages.Queries.GetById;

public class GetByIdStageResponse : IResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
}