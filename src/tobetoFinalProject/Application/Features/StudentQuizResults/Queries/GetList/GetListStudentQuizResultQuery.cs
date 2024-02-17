using Application.Features.StudentQuizResults.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentQuizResults.Constants.StudentQuizResultsOperationClaims;

namespace Application.Features.StudentQuizResults.Queries.GetList;

public class GetListStudentQuizResultQuery : IRequest<GetListResponse<GetListStudentQuizResultListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListStudentQuizResultQueryHandler : IRequestHandler<GetListStudentQuizResultQuery, GetListResponse<GetListStudentQuizResultListItemDto>>
    {
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly IMapper _mapper;

        public GetListStudentQuizResultQueryHandler(IStudentQuizResultRepository studentQuizResultRepository, IMapper mapper)
        {
            _studentQuizResultRepository = studentQuizResultRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentQuizResultListItemDto>> Handle(GetListStudentQuizResultQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentQuizResult> studentQuizResults = await _studentQuizResultRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentQuizResultListItemDto> response = _mapper.Map<GetListResponse<GetListStudentQuizResultListItemDto>>(studentQuizResults);
            return response;
        }
    }
}