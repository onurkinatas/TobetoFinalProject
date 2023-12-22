using Application.Features.StudentSocialMedias.Constants;
using Application.Services.CacheForMemory;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Application.Features.StudentSocialMedias.Rules;

public class StudentSocialMediaBusinessRules : BaseBusinessRules
{
    private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
    private readonly ICacheMemoryService _cacheMemoryService;

    public StudentSocialMediaBusinessRules(IStudentSocialMediaRepository studentSocialMediaRepository, ICacheMemoryService cacheMemoryService)
    {
        _studentSocialMediaRepository = studentSocialMediaRepository;
        _cacheMemoryService = cacheMemoryService;
    }

    public Task StudentSocialMediaShouldExistWhenSelected(StudentSocialMedia? studentSocialMedia)
    {
        if (studentSocialMedia == null)
            throw new BusinessException(StudentSocialMediasBusinessMessages.StudentSocialMediaNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentSocialMediaIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentSocialMedia? studentSocialMedia = await _studentSocialMediaRepository.GetAsync(
            predicate: ssm => ssm.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentSocialMediaShouldExistWhenSelected(studentSocialMedia);
    }

    public async Task StudentSocialMediaSelectionControl(StudentSocialMedia? studentSocialMedia, CancellationToken cancellationToken) 
    {
        var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

        IPaginate<StudentSocialMedia> studentSocialMedias = await _studentSocialMediaRepository.GetListAsync(
                predicate: s => s.StudentId == cacheMemoryStudentId,
                include: sc => sc.Include(sc => sc.SocialMedia),
                cancellationToken: cancellationToken
        );

        if (studentSocialMedias.Count == 3)
            throw new BusinessException(StudentSocialMediasBusinessMessages.MaxStudenSocialMediaCapacity);
    }
}