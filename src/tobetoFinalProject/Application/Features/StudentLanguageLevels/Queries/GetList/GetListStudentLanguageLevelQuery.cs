using Application.Features.StudentLanguageLevels.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentLanguageLevels.Constants.StudentLanguageLevelsOperationClaims;

namespace Application.Features.StudentLanguageLevels.Queries.GetList;

public class GetListStudentLanguageLevelQuery : IRequest<GetListResponse<GetListStudentLanguageLevelListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentLanguageLevels({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentLanguageLevels";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentLanguageLevelQueryHandler : IRequestHandler<GetListStudentLanguageLevelQuery, GetListResponse<GetListStudentLanguageLevelListItemDto>>
    {
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly IMapper _mapper;

        public GetListStudentLanguageLevelQueryHandler(IStudentLanguageLevelRepository studentLanguageLevelRepository, IMapper mapper)
        {
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentLanguageLevelListItemDto>> Handle(GetListStudentLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentLanguageLevel> studentLanguageLevels = await _studentLanguageLevelRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentLanguageLevelListItemDto> response = _mapper.Map<GetListResponse<GetListStudentLanguageLevelListItemDto>>(studentLanguageLevels);
            return response;
        }
    }
}