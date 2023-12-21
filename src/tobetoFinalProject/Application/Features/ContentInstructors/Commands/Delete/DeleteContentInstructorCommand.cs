using Application.Features.ContentInstructors.Constants;
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

namespace Application.Features.ContentInstructors.Commands.Delete;

public class DeleteContentInstructorCommand : IRequest<DeletedContentInstructorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentInstructorsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentInstructors";

    public class DeleteContentInstructorCommandHandler : IRequestHandler<DeleteContentInstructorCommand, DeletedContentInstructorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentInstructorRepository _contentInstructorRepository;
        private readonly ContentInstructorBusinessRules _contentInstructorBusinessRules;

        public DeleteContentInstructorCommandHandler(IMapper mapper, IContentInstructorRepository contentInstructorRepository,
                                         ContentInstructorBusinessRules contentInstructorBusinessRules)
        {
            _mapper = mapper;
            _contentInstructorRepository = contentInstructorRepository;
            _contentInstructorBusinessRules = contentInstructorBusinessRules;
        }

        public async Task<DeletedContentInstructorResponse> Handle(DeleteContentInstructorCommand request, CancellationToken cancellationToken)
        {
            ContentInstructor? contentInstructor = await _contentInstructorRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _contentInstructorBusinessRules.ContentInstructorShouldExistWhenSelected(contentInstructor);

            await _contentInstructorRepository.DeleteAsync(contentInstructor!);

            DeletedContentInstructorResponse response = _mapper.Map<DeletedContentInstructorResponse>(contentInstructor);
            return response;
        }
    }
}