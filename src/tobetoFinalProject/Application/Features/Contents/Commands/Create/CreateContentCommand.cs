using Application.Features.Contents.Constants;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Contents.Constants.ContentsOperationClaims;

namespace Application.Features.Contents.Commands.Create;

public class CreateContentCommand : IRequest<CreatedContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public Guid LanguageId { get; set; }
    public Guid SubTypeId { get; set; }
    public string VideoUrl { get; set; }
    public string Description { get; set; }
    public Guid ManufacturerId { get; set; }
    public int Duration { get; set; }
    public Guid? ContentCategoryId { get; set; }
    public ICollection<ContentInstructor>? ContentInstructors { get; set; }
     
    public string[] Roles => new[] { Admin, Write, ContentsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContents";

    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, CreatedContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;
        private readonly ContentBusinessRules _contentBusinessRules;

        public CreateContentCommandHandler(IMapper mapper, IContentRepository contentRepository,
                                         ContentBusinessRules contentBusinessRules)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
            _contentBusinessRules = contentBusinessRules;
        }

        public async Task<CreatedContentResponse> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            Content content = _mapper.Map<Content>(request);

            await _contentBusinessRules.ContentNameShouldNotExist(content, cancellationToken);

            await _contentRepository.AddAsync(content);

            CreatedContentResponse response = _mapper.Map<CreatedContentResponse>(content);
            return response;
        }
    }
}