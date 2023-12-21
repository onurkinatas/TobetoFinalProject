using Core.Application.Responses;

namespace Application.Features.LectureLikes.Commands.Delete;

public class DeletedLectureLikeResponse : IResponse
{
    public Guid Id { get; set; }
}