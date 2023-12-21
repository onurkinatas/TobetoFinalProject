using Application.Features.ContentCategories.Constants;
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

namespace Application.Features.ContentCategories.Commands.Delete;

public class DeleteContentCategoryCommand : IRequest<DeletedContentCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentCategoriesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentCategories";

    public class DeleteContentCategoryCommandHandler : IRequestHandler<DeleteContentCategoryCommand, DeletedContentCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentCategoryRepository _contentCategoryRepository;
        private readonly ContentCategoryBusinessRules _contentCategoryBusinessRules;

        public DeleteContentCategoryCommandHandler(IMapper mapper, IContentCategoryRepository contentCategoryRepository,
                                         ContentCategoryBusinessRules contentCategoryBusinessRules)
        {
            _mapper = mapper;
            _contentCategoryRepository = contentCategoryRepository;
            _contentCategoryBusinessRules = contentCategoryBusinessRules;
        }

        public async Task<DeletedContentCategoryResponse> Handle(DeleteContentCategoryCommand request, CancellationToken cancellationToken)
        {
            ContentCategory? contentCategory = await _contentCategoryRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _contentCategoryBusinessRules.ContentCategoryShouldExistWhenSelected(contentCategory);

            await _contentCategoryRepository.DeleteAsync(contentCategory!);

            DeletedContentCategoryResponse response = _mapper.Map<DeletedContentCategoryResponse>(contentCategory);
            return response;
        }
    }
}