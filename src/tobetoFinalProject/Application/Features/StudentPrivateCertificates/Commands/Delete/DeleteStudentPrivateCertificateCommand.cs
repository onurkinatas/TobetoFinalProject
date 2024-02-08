using Application.Features.StudentPrivateCertificates.Constants;
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

namespace Application.Features.StudentPrivateCertificates.Commands.Delete;

public class DeleteStudentPrivateCertificateCommand : IRequest<DeletedStudentPrivateCertificateResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentPrivateCertificatesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentPrivateCertificates";

    public class DeleteStudentPrivateCertificateCommandHandler : IRequestHandler<DeleteStudentPrivateCertificateCommand, DeletedStudentPrivateCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
        private readonly StudentPrivateCertificateBusinessRules _studentPrivateCertificateBusinessRules;

        public DeleteStudentPrivateCertificateCommandHandler(IMapper mapper, IStudentPrivateCertificateRepository studentPrivateCertificateRepository,
                                         StudentPrivateCertificateBusinessRules studentPrivateCertificateBusinessRules)
        {
            _mapper = mapper;
            _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
            _studentPrivateCertificateBusinessRules = studentPrivateCertificateBusinessRules;
        }

        public async Task<DeletedStudentPrivateCertificateResponse> Handle(DeleteStudentPrivateCertificateCommand request, CancellationToken cancellationToken)
        {
            StudentPrivateCertificate? studentPrivateCertificate = await _studentPrivateCertificateRepository.GetAsync(predicate: spc => spc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentPrivateCertificateBusinessRules.StudentPrivateCertificateShouldExistWhenSelected(studentPrivateCertificate);

            await _studentPrivateCertificateRepository.DeleteAsync(studentPrivateCertificate!);

            DeletedStudentPrivateCertificateResponse response = _mapper.Map<DeletedStudentPrivateCertificateResponse>(studentPrivateCertificate);
            return response;
        }
    }
}