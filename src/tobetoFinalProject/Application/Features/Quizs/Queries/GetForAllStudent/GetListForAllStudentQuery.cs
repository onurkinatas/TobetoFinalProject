using Application.Features.Quizs.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.GetForAllStudent;
public class GetListForAllStudentQuery : IRequest<GetListResponse<GetListQuizListItemDto>>, ISecuredRequest
{

    public string[] Roles => new[] {"Student" };

    public class GetListForAllStudentQueryHandler : IRequestHandler<GetListForAllStudentQuery, GetListResponse<GetListQuizListItemDto>>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public GetListForAllStudentQueryHandler(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuizListItemDto>> Handle(GetListForAllStudentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Quiz> quizs = await _quizRepository.GetListAsync(
                index: 0,
                size: 5,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQuizListItemDto> response = _mapper.Map<GetListResponse<GetListQuizListItemDto>>(quizs);
            return response;
        }
    }
}
