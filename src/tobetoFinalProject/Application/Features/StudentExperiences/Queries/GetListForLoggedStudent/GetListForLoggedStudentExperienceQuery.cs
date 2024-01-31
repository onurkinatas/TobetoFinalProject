using Application.Features.StudentExperiences.Queries.GetList;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentExperiences.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentExperienceQuery : IRequest<GetListResponse<GetListStudentExperienceListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] {"Student" };

    public TimeSpan? SlidingExpiration { get; }

    public class GetListForLoggedStudentExperienceQueryHandler : IRequestHandler<GetListForLoggedStudentExperienceQuery, GetListResponse<GetListStudentExperienceListItemDto>>
    {
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentRepository _studentRepository;
        private readonly IContextOperationService _contextOperationService;


        public GetListForLoggedStudentExperienceQueryHandler(IStudentExperienceRepository studentExperienceRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IStudentRepository studentRepository, IContextOperationService contextOperationService)
        {
            _studentExperienceRepository = studentExperienceRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentExperienceListItemDto>> Handle(GetListForLoggedStudentExperienceQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentExperience> studentExperiences = await _studentExperienceRepository.GetListAsync(
                predicate: se => se.StudentId == student.Id,
                include: se => se.Include(se => se.City)
                               .Include(se=>se.Student)
                               .ThenInclude(se=>se.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentExperienceListItemDto> response = _mapper.Map<GetListResponse<GetListStudentExperienceListItemDto>>(studentExperiences);
            return response;
        }
    }
}
