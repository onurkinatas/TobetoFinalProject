using Application.Features.Students.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Students.Constants.StudentsOperationClaims;

namespace Application.Features.Students.Queries.GetById;

public class GetByTokenStudentQuery : IRequest<GetByTokenStudentResponse>, ISecuredRequest
{
    public string[] Roles => new[] { Admin, Read, "Student" };

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
                include: s => s.Include(s => s.StudentSocialMedias)
                .ThenInclude(s => s.SocialMedia)
                .Include(s => s.StudentCertificates)
                .ThenInclude(s => s.Certificate)
                .Include(s => s.StudentLanguageLevels)
                .ThenInclude(s => s.LanguageLevel)
                .Include(s => s.StudentSkills)
                .ThenInclude(s => s.Skill)
                .Include(s => s.StudentAppeal)
                .ThenInclude(s => s.Appeal)
                .Include(s => s.StudentEducations)
                .Include(s => s.StudentExperiences)
                .ThenInclude(se=>se.City)
                .Include(s => s.StudentPrivateCertificates)
                .Include(s => s.StudentClassStudentes)
                .ThenInclude(s => s.StudentClass)
                 .Include(s => s.User),
                cancellationToken: cancellationToken);
            await _studentBusinessRules.StudentShouldExistWhenSelected(student);

            GetByTokenStudentResponse response = _mapper.Map<GetByTokenStudentResponse>(student);
            return response;
        }
    }
}