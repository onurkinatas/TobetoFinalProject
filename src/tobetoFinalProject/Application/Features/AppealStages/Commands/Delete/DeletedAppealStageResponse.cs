using Core.Application.Responses;

namespace Application.Features.AppealStages.Commands.Delete;

public class DeletedAppealStageResponse : IResponse
{
    public Guid Id { get; set; }
}