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
using Microsoft.AspNetCore.Http;
using Application.Services.ContextOperations;
using Application.Services.ImageService;

namespace Application.Features.StudentPrivateCertificates.Commands.Create;

public class CreateStudentPrivateCertificateCommand : IRequest<CreatedStudentPrivateCertificateResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? StudentId { get; set; }
    public string? CertificateUrl { get; set; }
    public IFormFile CertificateUrlTemp { get; set; }

    public string[] Roles => new[] {"Student"};

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentPrivateCertificates";

    public class CreateStudentPrivateCertificateCommandHandler : IRequestHandler<CreateStudentPrivateCertificateCommand, CreatedStudentPrivateCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
        private readonly StudentPrivateCertificateBusinessRules _studentPrivateCertificateBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly ImageServiceBase _imageServiceBase;
        public CreateStudentPrivateCertificateCommandHandler(IMapper mapper, IStudentPrivateCertificateRepository studentPrivateCertificateRepository,
                                         StudentPrivateCertificateBusinessRules studentPrivateCertificateBusinessRules, IContextOperationService contextOperationService, ImageServiceBase imageServiceBase)
        {
            _mapper = mapper;
            _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
            _studentPrivateCertificateBusinessRules = studentPrivateCertificateBusinessRules;
            _contextOperationService = contextOperationService;
            _imageServiceBase = imageServiceBase;
        }

        public async Task<CreatedStudentPrivateCertificateResponse> Handle(CreateStudentPrivateCertificateCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;

            request.CertificateUrl = await _imageServiceBase.UploadAsync(request.CertificateUrlTemp);

            StudentPrivateCertificate studentPrivateCertificate = _mapper.Map<StudentPrivateCertificate>(request);

            await _studentPrivateCertificateRepository.AddAsync(studentPrivateCertificate);

            CreatedStudentPrivateCertificateResponse response = _mapper.Map<CreatedStudentPrivateCertificateResponse>(studentPrivateCertificate);
            return response;
        }
    }
}