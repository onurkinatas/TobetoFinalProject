using Core.Application.Responses;

namespace Application.Features.ContentTags.Commands.Update;

public class UpdatedContentTagResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }
}