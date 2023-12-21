using Core.Application.Responses;

namespace Application.Features.Lectures.Commands.Delete;

public class DeletedLectureResponse : IResponse
{
    public Guid Id { get; set; }
}