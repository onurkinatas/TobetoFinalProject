using Application.Features.StudentPrivateCertificates.Constants;
using Application.Features.StudentPrivateCertificates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentPrivateCertificates.Constants.StudentPrivateCertificatesOperationClaims;

namespace Application.Features.StudentPrivateCertificates.Queries.GetById;

public class GetByIdStudentPrivateCertificateQuery : IRequest<GetByIdStudentPrivateCertificateResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentPrivateCertificateQueryHandler : IRequestHandler<GetByIdStudentPrivateCertificateQuery, GetByIdStudentPrivateCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
        private readonly StudentPrivateCertificateBusinessRules _studentPrivateCertificateBusinessRules;

        public GetByIdStudentPrivateCertificateQueryHandler(IMapper mapper, IStudentPrivateCertificateRepository studentPrivateCertificateRepository, StudentPrivateCertificateBusinessRules studentPrivateCertificateBusinessRules)
        {
            _mapper = mapper;
            _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
            _studentPrivateCertificateBusinessRules = studentPrivateCertificateBusinessRules;
        }

        public async Task<GetByIdStudentPrivateCertificateResponse> Handle(GetByIdStudentPrivateCertificateQuery request, CancellationToken cancellationToken)
        {
            StudentPrivateCertificate? studentPrivateCertificate = await _studentPrivateCertificateRepository.GetAsync(predicate: spc => spc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentPrivateCertificateBusinessRules.StudentPrivateCertificateShouldExistWhenSelected(studentPrivateCertificate);

            GetByIdStudentPrivateCertificateResponse response = _mapper.Map<GetByIdStudentPrivateCertificateResponse>(studentPrivateCertificate);
            return response;
        }
    }
}