using Application.Features.ContentInstructors.Constants;
using Application.Features.ContentInstructors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentInstructors.Constants.ContentInstructorsOperationClaims;

namespace Application.Features.ContentInstructors.Commands.Create;

public class CreateContentInstructorCommand : IRequest<CreatedContentInstructorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentInstructorsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentInstructors";

    public class CreateContentInstructorCommandHandler : IRequestHandler<CreateContentInstructorCommand, CreatedContentInstructorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentInstructorRepository _contentInstructorRepository;
        private readonly ContentInstructorBusinessRules _contentInstructorBusinessRules;

        public CreateContentInstructorCommandHandler(IMapper mapper, IContentInstructorRepository contentInstructorRepository,
                                         ContentInstructorBusinessRules contentInstructorBusinessRules)
        {
            _mapper = mapper;
            _contentInstructorRepository = contentInstructorRepository;
            _contentInstructorBusinessRules = contentInstructorBusinessRules;
        }

        public async Task<CreatedContentInstructorResponse> Handle(CreateContentInstructorCommand request, CancellationToken cancellationToken)
        {
            ContentInstructor contentInstructor = _mapper.Map<ContentInstructor>(request);

            await _contentInstructorRepository.AddAsync(contentInstructor);

            CreatedContentInstructorResponse response = _mapper.Map<CreatedContentInstructorResponse>(contentInstructor);
            return response;
        }
    }
}