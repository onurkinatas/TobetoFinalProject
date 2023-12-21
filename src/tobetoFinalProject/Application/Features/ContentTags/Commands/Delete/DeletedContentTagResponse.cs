using Core.Application.Responses;

namespace Application.Features.ContentTags.Commands.Delete;

public class DeletedContentTagResponse : IResponse
{
    public Guid Id { get; set; }
}