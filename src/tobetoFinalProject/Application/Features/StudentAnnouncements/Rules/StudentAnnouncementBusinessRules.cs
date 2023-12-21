using Application.Features.StudentAnnouncements.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentAnnouncements.Rules;

public class StudentAnnouncementBusinessRules : BaseBusinessRules
{
    private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;

    public StudentAnnouncementBusinessRules(IStudentAnnouncementRepository studentAnnouncementRepository)
    {
        _studentAnnouncementRepository = studentAnnouncementRepository;
    }

    public Task StudentAnnouncementShouldExistWhenSelected(StudentAnnouncement? studentAnnouncement)
    {
        if (studentAnnouncement == null)
            throw new BusinessException(StudentAnnouncementsBusinessMessages.StudentAnnouncementNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentAnnouncementIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentAnnouncement? studentAnnouncement = await _studentAnnouncementRepository.GetAsync(
            predicate: sa => sa.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);
    }
}