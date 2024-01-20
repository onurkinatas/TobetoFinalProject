using Application.Features.ContentInstructors.Constants;
using Application.Features.ContentInstructors.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentInstructors.Rules;

public class ContentInstructorBusinessRules : BaseBusinessRules
{
    private readonly IContentInstructorRepository _contentInstructorRepository;

    public ContentInstructorBusinessRules(IContentInstructorRepository contentInstructorRepository)
    {
        _contentInstructorRepository = contentInstructorRepository;
    }
    public async Task ContentInstructorShouldNotExistsWhenInsert(Guid contentId, Guid instructorId)
    {
        bool doesExists = await _contentInstructorRepository
            .AnyAsync(predicate: ca => ca.InstructorId == instructorId && ca.ContentId == contentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentInstructorsBusinessMessages.ContentInstructorAlreadyExists);
    }
    public async Task ContentInstructorShouldNotExistsWhenUpdate(Guid contentId, Guid instructorId)
    {
        bool doesExists = await _contentInstructorRepository
            .AnyAsync(predicate: ca => ca.InstructorId == instructorId && ca.ContentId == contentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(ContentInstructorsBusinessMessages.ContentInstructorAlreadyExists);
    }

    public Task ContentInstructorShouldExistWhenSelected(ContentInstructor? contentInstructor)
    {
        if (contentInstructor == null)
            throw new BusinessException(ContentInstructorsBusinessMessages.ContentInstructorNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentInstructorIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ContentInstructor? contentInstructor = await _contentInstructorRepository.GetAsync(
            predicate: ci => ci.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentInstructorShouldExistWhenSelected(contentInstructor);
    }
}