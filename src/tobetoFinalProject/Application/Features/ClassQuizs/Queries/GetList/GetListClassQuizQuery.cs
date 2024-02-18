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
using Application.Services.ContextOperations;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClassQuizs.Queries.GetList;

public class GetListClassQuizQuery : IRequest<GetListResponse<GetListClassQuizListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read,"Student" };

    public class GetListClassQuizQueryHandler : IRequestHandler<GetListClassQuizQuery, GetListResponse<GetListClassQuizListItemDto>>
    {
        private readonly IClassQuizRepository _classQuizRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListClassQuizQueryHandler(IClassQuizRepository classQuizRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _classQuizRepository = classQuizRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListClassQuizListItemDto>> Handle(GetListClassQuizQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getClassIds = await _contextOperationService.GetStudentClassesFromContext();
            IPaginate<ClassQuiz> classQuizs = await _classQuizRepository.GetListAsync(
                predicate: cq => getClassIds.Contains(cq.StudentClassId),
                include: gq => gq.Include(gq => gq.Quiz),
                
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassQuizListItemDto> response = _mapper.Map<GetListResponse<GetListClassQuizListItemDto>>(classQuizs);
            return response;
        }
    }
}