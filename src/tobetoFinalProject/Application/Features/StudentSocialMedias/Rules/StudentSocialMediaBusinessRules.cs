using Application.Features.StudentSocialMedias.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentSocialMedias.Rules;

public class StudentSocialMediaBusinessRules : BaseBusinessRules
{
    private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;

    public StudentSocialMediaBusinessRules(IStudentSocialMediaRepository studentSocialMediaRepository)
    {
        _studentSocialMediaRepository = studentSocialMediaRepository;
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
}