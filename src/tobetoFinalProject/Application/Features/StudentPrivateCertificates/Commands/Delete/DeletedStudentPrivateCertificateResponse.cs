using Core.Application.Responses;

namespace Application.Features.StudentPrivateCertificates.Commands.Delete;

public class DeletedStudentPrivateCertificateResponse : IResponse
{
    public Guid Id { get; set; }
}