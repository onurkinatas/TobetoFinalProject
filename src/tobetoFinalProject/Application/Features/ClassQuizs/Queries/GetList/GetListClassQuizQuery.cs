using Application.Features.ClassQuizs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassQuizs.Constants.ClassQuizsOperationClaims;

namespace Application.Features.ClassQuizs.Queries.GetList;

public class GetListClassQuizQuery : IRequest<GetListResponse<GetListClassQuizListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListClassQuizQueryHandler : IRequestHandler<GetListClassQuizQuery, GetListResponse<GetListClassQuizListItemDto>>
    {
        private readonly IClassQuizRepository _classQuizRepository;
        private readonly IMapper _mapper;

        public GetListClassQuizQueryHandler(IClassQuizRepository classQuizRepository, IMapper mapper)
        {
            _classQuizRepository = classQuizRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListClassQuizListItemDto>> Handle(GetListClassQuizQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ClassQuiz> classQuizs = await _classQuizRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassQuizListItemDto> response = _mapper.Map<GetListResponse<GetListClassQuizListItemDto>>(classQuizs);
            return response;
        }
    }
}