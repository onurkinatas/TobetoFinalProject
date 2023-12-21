using Application.Features.ContentCategories.Constants;
using Application.Features.ContentCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentCategories.Constants.ContentCategoriesOperationClaims;

namespace Application.Features.ContentCategories.Queries.GetById;

public class GetByIdContentCategoryQuery : IRequest<GetByIdContentCategoryResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentCategoryQueryHandler : IRequestHandler<GetByIdContentCategoryQuery, GetByIdContentCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentCategoryRepository _contentCategoryRepository;
        private readonly ContentCategoryBusinessRules _contentCategoryBusinessRules;

        public GetByIdContentCategoryQueryHandler(IMapper mapper, IContentCategoryRepository contentCategoryRepository, ContentCategoryBusinessRules contentCategoryBusinessRules)
        {
            _mapper = mapper;
            _contentCategoryRepository = contentCategoryRepository;
            _contentCategoryBusinessRules = contentCategoryBusinessRules;
        }

        public async Task<GetByIdContentCategoryResponse> Handle(GetByIdContentCategoryQuery request, CancellationToken cancellationToken)
        {
            ContentCategory? contentCategory = await _contentCategoryRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _contentCategoryBusinessRules.ContentCategoryShouldExistWhenSelected(contentCategory);

            GetByIdContentCategoryResponse response = _mapper.Map<GetByIdContentCategoryResponse>(contentCategory);
            return response;
        }
    }
}