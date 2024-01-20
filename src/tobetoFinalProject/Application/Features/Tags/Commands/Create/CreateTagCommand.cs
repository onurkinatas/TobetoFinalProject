using Application.Features.Tags.Constants;
using Application.Features.Tags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Tags.Constants.TagsOperationClaims;
using Application.Features.Languages.Rules;
namespace Application.Features.Tags.Commands.Create;

public class CreateTagCommand : IRequest<CreatedTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, TagsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTags";

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, CreatedTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly TagBusinessRules _tagBusinessRules;

        public CreateTagCommandHandler(IMapper mapper, ITagRepository tagRepository,
                                         TagBusinessRules tagBusinessRules)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _tagBusinessRules = tagBusinessRules;
        }

        public async Task<CreatedTagResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            Tag tag = _mapper.Map<Tag>(request);

            await _tagBusinessRules.TagShouldNotExistsWhenInsert(tag.Name);

            await _tagRepository.AddAsync(tag);

            CreatedTagResponse response = _mapper.Map<CreatedTagResponse>(tag);
            return response;
        }
    }
}