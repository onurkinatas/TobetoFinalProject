using Core.Application.Responses;

namespace Application.Features.Appeals.Commands.Delete;

public class DeletedAppealResponse : IResponse
{
    public Guid Id { get; set; }
}