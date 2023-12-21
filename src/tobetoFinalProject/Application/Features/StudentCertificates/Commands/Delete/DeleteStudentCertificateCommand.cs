using Application.Features.StudentCertificates.Constants;
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

namespace Application.Features.StudentCertificates.Commands.Delete;

public class DeleteStudentCertificateCommand : IRequest<DeletedStudentCertificateResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentCertificatesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentCertificates";

    public class DeleteStudentCertificateCommandHandler : IRequestHandler<DeleteStudentCertificateCommand, DeletedStudentCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentCertificateRepository _studentCertificateRepository;
        private readonly StudentCertificateBusinessRules _studentCertificateBusinessRules;

        public DeleteStudentCertificateCommandHandler(IMapper mapper, IStudentCertificateRepository studentCertificateRepository,
                                         StudentCertificateBusinessRules studentCertificateBusinessRules)
        {
            _mapper = mapper;
            _studentCertificateRepository = studentCertificateRepository;
            _studentCertificateBusinessRules = studentCertificateBusinessRules;
        }

        public async Task<DeletedStudentCertificateResponse> Handle(DeleteStudentCertificateCommand request, CancellationToken cancellationToken)
        {
            StudentCertificate? studentCertificate = await _studentCertificateRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentCertificateBusinessRules.StudentCertificateShouldExistWhenSelected(studentCertificate);

            await _studentCertificateRepository.DeleteAsync(studentCertificate!);

            DeletedStudentCertificateResponse response = _mapper.Map<DeletedStudentCertificateResponse>(studentCertificate);
            return response;
        }
    }
}