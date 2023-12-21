using Application.Features.ClassAnnouncements.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ClassAnnouncements.Rules;

public class ClassAnnouncementBusinessRules : BaseBusinessRules
{
    private readonly IClassAnnouncementRepository _classAnnouncementRepository;

    public ClassAnnouncementBusinessRules(IClassAnnouncementRepository classAnnouncementRepository)
    {
        _classAnnouncementRepository = classAnnouncementRepository;
    }

    public Task ClassAnnouncementShouldExistWhenSelected(ClassAnnouncement? classAnnouncement)
    {
        if (classAnnouncement == null)
            throw new BusinessException(ClassAnnouncementsBusinessMessages.ClassAnnouncementNotExists);
        return Task.CompletedTask;
    }

    public async Task ClassAnnouncementIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ClassAnnouncement? classAnnouncement = await _classAnnouncementRepository.GetAsync(
            predicate: ca => ca.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ClassAnnouncementShouldExistWhenSelected(classAnnouncement);
    }
}