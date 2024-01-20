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

namespace Application.Features.ContentCategories.Commands.Update;

public class UpdateContentCategoryCommand : IRequest<UpdatedContentCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentCategoriesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentCategories";

    public class UpdateContentCategoryCommandHandler : IRequestHandler<UpdateContentCategoryCommand, UpdatedContentCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentCategoryRepository _contentCategoryRepository;
        private readonly ContentCategoryBusinessRules _contentCategoryBusinessRules;

        public UpdateContentCategoryCommandHandler(IMapper mapper, IContentCategoryRepository contentCategoryRepository,
                                         ContentCategoryBusinessRules contentCategoryBusinessRules)
        {
            _mapper = mapper;
            _contentCategoryRepository = contentCategoryRepository;
            _contentCategoryBusinessRules = contentCategoryBusinessRules;
        }

        public async Task<UpdatedContentCategoryResponse> Handle(UpdateContentCategoryCommand request, CancellationToken cancellationToken)
        {
            ContentCategory? contentCategory = await _contentCategoryRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _contentCategoryBusinessRules.ContentCategoryShouldExistWhenSelected(contentCategory);
            contentCategory = _mapper.Map(request, contentCategory);
            await _contentCategoryBusinessRules.ContentCategoryNameShouldNotExist(contentCategory, cancellationToken);
            await _contentCategoryRepository.UpdateAsync(contentCategory!);

            UpdatedContentCategoryResponse response = _mapper.Map<UpdatedContentCategoryResponse>(contentCategory);
            return response;
        }
    }
}