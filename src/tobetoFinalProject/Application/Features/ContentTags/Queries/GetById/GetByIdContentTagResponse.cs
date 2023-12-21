using Core.Application.Responses;

namespace Application.Features.ContentTags.Queries.GetById;

public class GetByIdContentTagResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }
}