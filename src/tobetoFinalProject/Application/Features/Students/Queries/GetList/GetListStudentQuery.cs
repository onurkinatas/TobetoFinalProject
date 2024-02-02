using Application.Features.Students.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Students.Constants.StudentsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Students.Queries.GetList;

public class GetListStudentQuery : IRequest<GetListResponse<GetListStudentListItemDto>>, /*ISecuredRequest*/ ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudents({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudents";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentQueryHandler : IRequestHandler<GetListStudentQuery, GetListResponse<GetListStudentListItemDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetListStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentListItemDto>> Handle(GetListStudentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Student> students = await _studentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
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
                    .ThenInclude(s => s.StudentClass)
                     .Include(s => s.User),
                    size: request.PageRequest.PageSize,
                    orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentListItemDto> response = _mapper.Map<GetListResponse<GetListStudentListItemDto>>(students);
            return response;
        }
    }
}