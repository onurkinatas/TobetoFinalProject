using Core.Application.Responses;

namespace Application.Features.ClassAnnouncements.Commands.Delete;

public class DeletedClassAnnouncementResponse : IResponse
{
    public Guid Id { get; set; }
}