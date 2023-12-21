using Application.Features.StudentSocialMedias.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentSocialMedias;

public class StudentSocialMediasManager : IStudentSocialMediasService
{
    private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
    private readonly StudentSocialMediaBusinessRules _studentSocialMediaBusinessRules;

    public StudentSocialMediasManager(IStudentSocialMediaRepository studentSocialMediaRepository, StudentSocialMediaBusinessRules studentSocialMediaBusinessRules)
    {
        _studentSocialMediaRepository = studentSocialMediaRepository;
        _studentSocialMediaBusinessRules = studentSocialMediaBusinessRules;
    }

    public async Task<StudentSocialMedia?> GetAsync(
        Expression<Func<StudentSocialMedia, bool>> predicate,
        Func<IQueryable<StudentSocialMedia>, IIncludableQueryable<StudentSocialMedia, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentSocialMedia? studentSocialMedia = await _studentSocialMediaRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentSocialMedia;
    }

    public async Task<IPaginate<StudentSocialMedia>?> GetListAsync(
        Expression<Func<StudentSocialMedia, bool>>? predicate = null,
        Func<IQueryable<StudentSocialMedia>, IOrderedQueryable<StudentSocialMedia>>? orderBy = null,
        Func<IQueryable<StudentSocialMedia>, IIncludableQueryable<StudentSocialMedia, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentSocialMedia> studentSocialMediaList = await _studentSocialMediaRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentSocialMediaList;
    }

    public async Task<StudentSocialMedia> AddAsync(StudentSocialMedia studentSocialMedia)
    {
        StudentSocialMedia addedStudentSocialMedia = await _studentSocialMediaRepository.AddAsync(studentSocialMedia);

        return addedStudentSocialMedia;
    }

    public async Task<StudentSocialMedia> UpdateAsync(StudentSocialMedia studentSocialMedia)
    {
        StudentSocialMedia updatedStudentSocialMedia = await _studentSocialMediaRepository.UpdateAsync(studentSocialMedia);

        return updatedStudentSocialMedia;
    }

    public async Task<StudentSocialMedia> DeleteAsync(StudentSocialMedia studentSocialMedia, bool permanent = false)
    {
        StudentSocialMedia deletedStudentSocialMedia = await _studentSocialMediaRepository.DeleteAsync(studentSocialMedia);

        return deletedStudentSocialMedia;
    }
}
