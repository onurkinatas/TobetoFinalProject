using Core.Application.Responses;

namespace Application.Features.Options.Commands.Delete;

public class DeletedOptionResponse : IResponse
{
    public int Id { get; set; }
}