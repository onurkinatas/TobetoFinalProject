using Core.Application.Responses;

namespace Application.Features.Contents.Commands.Delete;

public class DeletedContentResponse : IResponse
{
    public Guid Id { get; set; }
}