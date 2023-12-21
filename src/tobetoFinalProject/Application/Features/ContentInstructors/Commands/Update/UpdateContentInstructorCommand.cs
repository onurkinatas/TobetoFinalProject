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

namespace Application.Features.ContentInstructors.Commands.Update;

public class UpdateContentInstructorCommand : IRequest<UpdatedContentInstructorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentInstructorsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentInstructors";

    public class UpdateContentInstructorCommandHandler : IRequestHandler<UpdateContentInstructorCommand, UpdatedContentInstructorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentInstructorRepository _contentInstructorRepository;
        private readonly ContentInstructorBusinessRules _contentInstructorBusinessRules;

        public UpdateContentInstructorCommandHandler(IMapper mapper, IContentInstructorRepository contentInstructorRepository,
                                         ContentInstructorBusinessRules contentInstructorBusinessRules)
        {
            _mapper = mapper;
            _contentInstructorRepository = contentInstructorRepository;
            _contentInstructorBusinessRules = contentInstructorBusinessRules;
        }

        public async Task<UpdatedContentInstructorResponse> Handle(UpdateContentInstructorCommand request, CancellationToken cancellationToken)
        {
            ContentInstructor? contentInstructor = await _contentInstructorRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _contentInstructorBusinessRules.ContentInstructorShouldExistWhenSelected(contentInstructor);
            contentInstructor = _mapper.Map(request, contentInstructor);

            await _contentInstructorRepository.UpdateAsync(contentInstructor!);

            UpdatedContentInstructorResponse response = _mapper.Map<UpdatedContentInstructorResponse>(contentInstructor);
            return response;
        }
    }
}