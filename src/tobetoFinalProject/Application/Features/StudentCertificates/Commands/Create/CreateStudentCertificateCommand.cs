using Application.Features.StudentCertificates.Constants;
using Application.Features.StudentCertificates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentCertificates.Constants.StudentCertificatesOperationClaims;

namespace Application.Features.StudentCertificates.Commands.Create;

public class CreateStudentCertificateCommand : IRequest<CreatedStudentCertificateResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentId { get; set; }
    public Guid CertificateId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentCertificatesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentCertificates";

    public class CreateStudentCertificateCommandHandler : IRequestHandler<CreateStudentCertificateCommand, CreatedStudentCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentCertificateRepository _studentCertificateRepository;
        private readonly StudentCertificateBusinessRules _studentCertificateBusinessRules;

        public CreateStudentCertificateCommandHandler(IMapper mapper, IStudentCertificateRepository studentCertificateRepository,
                                         StudentCertificateBusinessRules studentCertificateBusinessRules)
        {
            _mapper = mapper;
            _studentCertificateRepository = studentCertificateRepository;
            _studentCertificateBusinessRules = studentCertificateBusinessRules;
        }

        public async Task<CreatedStudentCertificateResponse> Handle(CreateStudentCertificateCommand request, CancellationToken cancellationToken)
        {
            StudentCertificate studentCertificate = _mapper.Map<StudentCertificate>(request);

            await _studentCertificateRepository.AddAsync(studentCertificate);

            CreatedStudentCertificateResponse response = _mapper.Map<CreatedStudentCertificateResponse>(studentCertificate);
            return response;
        }
    }
}