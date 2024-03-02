using Amazon.Runtime.Internal.Util;
using Application.Features.Students.Rules;
using Application.Services.Repositories;
using Application.Services.StudentClassStudents;
using Application.Services.Students;
using Core.Security.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ContextOperations;
public class ContextOperationManager : IContextOperationService
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudentsService _studentsService;
    private readonly StudentBusinessRules _studentBusinessRules;
    private readonly IStudentClassStudentsService _studentClassStudentsService;
    public ContextOperationManager(IHttpContextAccessor contextAccessor, StudentBusinessRules studentBusinessRules, IStudentsService studentsService, IStudentClassStudentsService studentClassStudentsServicey)
    {
        _httpContextAccessor = contextAccessor;
        _studentBusinessRules = studentBusinessRules;
        _studentsService = studentsService;
        _studentClassStudentsService = studentClassStudentsServicey;
    }


    public async Task<Student> GetStudentFromContext()
    {
        int userId = _httpContextAccessor.HttpContext.User.GetUserId();
        await _studentBusinessRules.StudentShouldBeExist(userId);
        Student student = await _studentsService.GetAsync(predicate: s => s.UserId == userId);
        return student;
    }
    public async Task<ICollection<Guid>> GetStudentClassesFromContext()
    {
        Student student = await GetStudentFromContext();
        ICollection<Guid> classIds = _studentClassStudentsService
            .GetAllWithoutPaginate(sc => sc.StudentId == student.Id)
            .Select(sc => sc.StudentClassId)
            .ToList();
        return classIds;

    }
}