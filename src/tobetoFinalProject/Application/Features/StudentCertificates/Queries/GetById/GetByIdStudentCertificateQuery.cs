using Application.Features.StudentCertificates.Constants;
using Application.Features.StudentCertificates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentCertificates.Constants.StudentCertificatesOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentCertificates.Queries.GetById;

public class GetByIdStudentCertificateQuery : IRequest<GetByIdStudentCertificateResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentCertificateQueryHandler : IRequestHandler<GetByIdStudentCertificateQuery, GetByIdStudentCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentCertificateRepository _studentCertificateRepository;
        private readonly StudentCertificateBusinessRules _studentCertificateBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentCertificateQueryHandler(IMapper mapper, IStudentCertificateRepository studentCertificateRepository, StudentCertificateBusinessRules studentCertificateBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentCertificateRepository = studentCertificateRepository;
            _studentCertificateBusinessRules = studentCertificateBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentCertificateResponse> Handle(GetByIdStudentCertificateQuery request, CancellationToken cancellationToken)
        {

            StudentCertificate? studentCertificate = await _studentCertificateRepository.GetAsync(
                predicate: sc => sc.Id == request.Id,
                include: sc => sc.Include(sc => sc.Certificate),
                cancellationToken: cancellationToken);
            await _studentCertificateBusinessRules.StudentCertificateShouldExistWhenSelected(studentCertificate);

            GetByIdStudentCertificateResponse response = _mapper.Map<GetByIdStudentCertificateResponse>(studentCertificate);
            return response;
        }
    }
}