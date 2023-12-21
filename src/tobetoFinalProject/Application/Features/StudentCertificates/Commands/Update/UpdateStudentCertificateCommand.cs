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

namespace Application.Features.StudentCertificates.Commands.Update;

public class UpdateStudentCertificateCommand : IRequest<UpdatedStudentCertificateResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CertificateId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentCertificatesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentCertificates";

    public class UpdateStudentCertificateCommandHandler : IRequestHandler<UpdateStudentCertificateCommand, UpdatedStudentCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentCertificateRepository _studentCertificateRepository;
        private readonly StudentCertificateBusinessRules _studentCertificateBusinessRules;

        public UpdateStudentCertificateCommandHandler(IMapper mapper, IStudentCertificateRepository studentCertificateRepository,
                                         StudentCertificateBusinessRules studentCertificateBusinessRules)
        {
            _mapper = mapper;
            _studentCertificateRepository = studentCertificateRepository;
            _studentCertificateBusinessRules = studentCertificateBusinessRules;
        }

        public async Task<UpdatedStudentCertificateResponse> Handle(UpdateStudentCertificateCommand request, CancellationToken cancellationToken)
        {
            StudentCertificate? studentCertificate = await _studentCertificateRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentCertificateBusinessRules.StudentCertificateShouldExistWhenSelected(studentCertificate);
            studentCertificate = _mapper.Map(request, studentCertificate);

            await _studentCertificateRepository.UpdateAsync(studentCertificate!);

            UpdatedStudentCertificateResponse response = _mapper.Map<UpdatedStudentCertificateResponse>(studentCertificate);
            return response;
        }
    }
}