using Core.Application.Responses;

namespace Application.Features.StudentAnnouncements.Commands.Delete;

public class DeletedStudentAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
}