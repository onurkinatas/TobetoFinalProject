using Core.Application.Responses;

namespace Application.Features.StudentCertificates.Commands.Delete;

public class DeletedStudentCertificateResponse : IResponse
{
    public Guid Id { get; set; }
}