using Application.Features.StudentCertificates.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentCertificates.Constants.StudentCertificatesOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;
using Application.Services.ContextOperations;

namespace Application.Features.StudentCertificates.Queries.GetList;

public class GetListStudentCertificateQuery : IRequest<GetListResponse<GetListStudentCertificateListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentCertificates({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentCertificates";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentCertificateQueryHandler : IRequestHandler<GetListStudentCertificateQuery, GetListResponse<GetListStudentCertificateListItemDto>>
    {
        private readonly IStudentCertificateRepository _studentCertificateRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentCertificateQueryHandler(IStudentCertificateRepository studentCertificateRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentCertificateRepository = studentCertificateRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentCertificateListItemDto>> Handle(GetListStudentCertificateQuery request, CancellationToken cancellationToken)
        {
            Student getStudentFromContext = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentCertificate> studentCertificates = await _studentCertificateRepository.GetListAsync(
                predicate: s => s.StudentId == getStudentFromContext.Id,
                include: sc => sc.Include(sc => sc.Certificate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentCertificateListItemDto> response = _mapper.Map<GetListResponse<GetListStudentCertificateListItemDto>>(studentCertificates);
            return response;
        }
    }
}