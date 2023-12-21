using Application.Features.Students.Constants;
using Application.Features.Students.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Students.Constants.StudentsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Students.Queries.GetById;

public class GetByIdStudentQuery : IRequest<GetByIdStudentResponse> /*ISecuredRequest*/
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentQueryHandler : IRequestHandler<GetByIdStudentQuery, GetByIdStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly StudentBusinessRules _studentBusinessRules;

        public GetByIdStudentQueryHandler(IMapper mapper, IStudentRepository studentRepository, StudentBusinessRules studentBusinessRules)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _studentBusinessRules = studentBusinessRules;
        }

        public async Task<GetByIdStudentResponse> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
        {
            Student? student = await _studentRepository.GetAsync(
                predicate: s => s.Id == request.Id,
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
                .Include(s => s.StudentClassStudentes)
                .ThenInclude(s => s.StudentClass),
                cancellationToken: cancellationToken);
            await _studentBusinessRules.StudentShouldExistWhenSelected(student);

            GetByIdStudentResponse response = _mapper.Map<GetByIdStudentResponse>(student);
            return response;
        }
    }
}