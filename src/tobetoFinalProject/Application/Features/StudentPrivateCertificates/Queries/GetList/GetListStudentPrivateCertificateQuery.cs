using Application.Features.StudentPrivateCertificates.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentPrivateCertificates.Constants.StudentPrivateCertificatesOperationClaims;

namespace Application.Features.StudentPrivateCertificates.Queries.GetList;

public class GetListStudentPrivateCertificateQuery : IRequest<GetListResponse<GetListStudentPrivateCertificateListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentPrivateCertificates({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentPrivateCertificates";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentPrivateCertificateQueryHandler : IRequestHandler<GetListStudentPrivateCertificateQuery, GetListResponse<GetListStudentPrivateCertificateListItemDto>>
    {
        private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
        private readonly IMapper _mapper;

        public GetListStudentPrivateCertificateQueryHandler(IStudentPrivateCertificateRepository studentPrivateCertificateRepository, IMapper mapper)
        {
            _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentPrivateCertificateListItemDto>> Handle(GetListStudentPrivateCertificateQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentPrivateCertificate> studentPrivateCertificates = await _studentPrivateCertificateRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentPrivateCertificateListItemDto> response = _mapper.Map<GetListResponse<GetListStudentPrivateCertificateListItemDto>>(studentPrivateCertificates);
            return response;
        }
    }
}