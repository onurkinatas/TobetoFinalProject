using Application.Features.StudentQuizOptions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentQuizOptions.Constants.StudentQuizOptionsOperationClaims;

namespace Application.Features.StudentQuizOptions.Queries.GetList;

public class GetListStudentQuizOptionQuery : IRequest<GetListResponse<GetListStudentQuizOptionListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListStudentQuizOptionQueryHandler : IRequestHandler<GetListStudentQuizOptionQuery, GetListResponse<GetListStudentQuizOptionListItemDto>>
    {
        private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;
        private readonly IMapper _mapper;

        public GetListStudentQuizOptionQueryHandler(IStudentQuizOptionRepository studentQuizOptionRepository, IMapper mapper)
        {
            _studentQuizOptionRepository = studentQuizOptionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentQuizOptionListItemDto>> Handle(GetListStudentQuizOptionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentQuizOption> studentQuizOptions = await _studentQuizOptionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentQuizOptionListItemDto> response = _mapper.Map<GetListResponse<GetListStudentQuizOptionListItemDto>>(studentQuizOptions);
            return response;
        }
    }
}