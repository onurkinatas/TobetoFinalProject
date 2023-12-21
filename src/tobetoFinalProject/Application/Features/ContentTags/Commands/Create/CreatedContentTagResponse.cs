using Core.Application.Responses;

namespace Application.Features.ContentTags.Commands.Create;

public class CreatedContentTagResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid TagId { get; set; }
}