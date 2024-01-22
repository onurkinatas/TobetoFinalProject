using Amazon.Runtime.Internal.Util;
using Application.Features.Students.Rules;
using Application.Services.Repositories;
using Core.Security.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ContextOperations;
public class ContextOperationManager:IContextOperationService
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudentRepository _studentRepository;
    private readonly StudentBusinessRules _studentBusinessRules;
    private readonly IStudentClassStudentRepository _studentClassStudentRepository;
    public ContextOperationManager(IHttpContextAccessor contextAccessor, IStudentRepository studentRepository, StudentBusinessRules studentBusinessRules, IStudentClassStudentRepository studentClassStudentRepository)
    {
        _httpContextAccessor = contextAccessor;
        _studentRepository = studentRepository;
        _studentBusinessRules = studentBusinessRules;
        _studentClassStudentRepository = studentClassStudentRepository;
    }
    public async Task<Student> GetStudentFromContext()
    {
        int userId = _httpContextAccessor.HttpContext.User.GetUserId();
        await _studentBusinessRules.StudentShouldBeExist(userId);
        Student student= await _studentRepository.GetAsync(predicate: s => s.UserId == userId);
        return student;
    }
    public async Task<ICollection<Guid>> GetStudentClassesFromContext()
    {
        Student student = await GetStudentFromContext();
        ICollection<Guid> classIds = _studentClassStudentRepository
            .GetAllWithoutPaginate(sc => sc.StudentId == student.Id)
            .Select(sc => sc.StudentClassId)
            .ToList();
        return classIds;
       
    }
}

