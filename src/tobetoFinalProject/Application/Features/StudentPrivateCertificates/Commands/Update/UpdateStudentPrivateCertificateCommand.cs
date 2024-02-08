using Application.Features.StudentPrivateCertificates.Constants;
using Application.Features.StudentPrivateCertificates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentPrivateCertificates.Constants.StudentPrivateCertificatesOperationClaims;

namespace Application.Features.StudentPrivateCertificates.Commands.Update;

public class UpdateStudentPrivateCertificateCommand : IRequest<UpdatedStudentPrivateCertificateResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string? CertificateUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentPrivateCertificatesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentPrivateCertificates";

    public class UpdateStudentPrivateCertificateCommandHandler : IRequestHandler<UpdateStudentPrivateCertificateCommand, UpdatedStudentPrivateCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
        private readonly StudentPrivateCertificateBusinessRules _studentPrivateCertificateBusinessRules;

        public UpdateStudentPrivateCertificateCommandHandler(IMapper mapper, IStudentPrivateCertificateRepository studentPrivateCertificateRepository,
                                         StudentPrivateCertificateBusinessRules studentPrivateCertificateBusinessRules)
        {
            _mapper = mapper;
            _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
            _studentPrivateCertificateBusinessRules = studentPrivateCertificateBusinessRules;
        }

        public async Task<UpdatedStudentPrivateCertificateResponse> Handle(UpdateStudentPrivateCertificateCommand request, CancellationToken cancellationToken)
        {
            StudentPrivateCertificate? studentPrivateCertificate = await _studentPrivateCertificateRepository.GetAsync(predicate: spc => spc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentPrivateCertificateBusinessRules.StudentPrivateCertificateShouldExistWhenSelected(studentPrivateCertificate);
            studentPrivateCertificate = _mapper.Map(request, studentPrivateCertificate);

            await _studentPrivateCertificateRepository.UpdateAsync(studentPrivateCertificate!);

            UpdatedStudentPrivateCertificateResponse response = _mapper.Map<UpdatedStudentPrivateCertificateResponse>(studentPrivateCertificate);
            return response;
        }
    }
}