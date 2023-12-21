using Core.Application.Responses;

namespace Application.Features.ClassExams.Commands.Delete;

public class DeletedClassExamResponse : IResponse
{
    public Guid Id { get; set; }
}