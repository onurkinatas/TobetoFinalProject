using Application.Features.Students.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using static Application.Features.Students.Constants.StudentsOperationClaims;

namespace Application.Features.Students.Queries.GetById;

public class GetByTokenStudentQuery : IRequest<GetByTokenStudentResponse>, ISecuredRequest,ICachableRequest
{
    public string[] Roles => new[] { Admin, Read, "Student" };
    public int? UserId { get; set; }
  
    public string CacheGroupKey => $"GetStudent{UserId}";
    public string CacheKey => $"GetStudent({UserId})";
    public bool BypassCache { get; }
    
    public TimeSpan? SlidingExpiration { get; }

    public class GetByTokenStudentQueryHandler : IRequestHandler<GetByTokenStudentQuery, GetByTokenStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly StudentBusinessRules _studentBusinessRules;
        private readonly IContextOperationService _contextOperationService;

        public GetByTokenStudentQueryHandler(IMapper mapper, IStudentRepository studentRepository, StudentBusinessRules studentBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _studentBusinessRules = studentBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByTokenStudentResponse> Handle(GetByTokenStudentQuery request, CancellationToken cancellationToken)
        {

            Student getStudent = await _contextOperationService.GetStudentFromContext();

            Student? student = await _studentRepository.GetAsync(
                predicate: s => s.Id == getStudent.Id,
                include: IncludeStudentDetails(),
                cancellationToken: cancellationToken);
            await _studentBusinessRules.StudentShouldExistWhenSelected(student);

            GetByTokenStudentResponse response = _mapper.Map<GetByTokenStudentResponse>(student);
            return response;
        }
        private Func<IQueryable<Student>, IIncludableQueryable<Student, object>> IncludeStudentDetails()
        {
            return query => query
            .Include(s => s.StudentSocialMedias)
                .ThenInclude(s => s.SocialMedia)
            .Include(s => s.StudentCertificates)
                .ThenInclude(s => s.Certificate)
            .Include(s => s.StudentLanguageLevels)
                .ThenInclude(s => s.LanguageLevel)
                    .ThenInclude(ll => ll.Language)
            .Include(s => s.StudentSkills)
                .ThenInclude(s => s.Skill)
            .Include(s => s.StudentAppeal)
                .ThenInclude(s => s.Appeal)
            .Include(s => s.StudentEducations)
            .Include(s => s.StudentExperiences)
                .ThenInclude(se => se.City)
            .Include(s => s.StudentPrivateCertificates)
            .Include(s => s.StudentClassStudentes)
                .ThenInclude(s => s.StudentClass)
            .Include(s => s.User)
            .Include(s => s.StudentQuizResults);


        }
    }
}