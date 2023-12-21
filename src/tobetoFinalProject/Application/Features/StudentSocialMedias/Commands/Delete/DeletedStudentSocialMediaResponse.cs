using Core.Application.Responses;

namespace Application.Features.StudentSocialMedias.Commands.Delete;

public class DeletedStudentSocialMediaResponse : IResponse
{
    public Guid Id { get; set; }
}