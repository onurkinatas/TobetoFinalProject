using Application.Features.ContentCategories.Constants;
using Application.Features.ContentCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentCategories.Constants.ContentCategoriesOperationClaims;

namespace Application.Features.ContentCategories.Commands.Create;

public class CreateContentCategoryCommand : IRequest<CreatedContentCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentCategoriesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentCategories";

    public class CreateContentCategoryCommandHandler : IRequestHandler<CreateContentCategoryCommand, CreatedContentCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentCategoryRepository _contentCategoryRepository;
        private readonly ContentCategoryBusinessRules _contentCategoryBusinessRules;

        public CreateContentCategoryCommandHandler(IMapper mapper, IContentCategoryRepository contentCategoryRepository,
                                         ContentCategoryBusinessRules contentCategoryBusinessRules)
        {
            _mapper = mapper;
            _contentCategoryRepository = contentCategoryRepository;
            _contentCategoryBusinessRules = contentCategoryBusinessRules;
        }

        public async Task<CreatedContentCategoryResponse> Handle(CreateContentCategoryCommand request, CancellationToken cancellationToken)
        {
            ContentCategory contentCategory = _mapper.Map<ContentCategory>(request);

            await _contentCategoryRepository.AddAsync(contentCategory);

            CreatedContentCategoryResponse response = _mapper.Map<CreatedContentCategoryResponse>(contentCategory);
            return response;
        }
    }
}