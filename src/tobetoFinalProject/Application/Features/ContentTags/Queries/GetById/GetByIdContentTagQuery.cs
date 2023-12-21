using Application.Features.ContentTags.Constants;
using Application.Features.ContentTags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentTags.Constants.ContentTagsOperationClaims;

namespace Application.Features.ContentTags.Queries.GetById;

public class GetByIdContentTagQuery : IRequest<GetByIdContentTagResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentTagQueryHandler : IRequestHandler<GetByIdContentTagQuery, GetByIdContentTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTagRepository _contentTagRepository;
        private readonly ContentTagBusinessRules _contentTagBusinessRules;

        public GetByIdContentTagQueryHandler(IMapper mapper, IContentTagRepository contentTagRepository, ContentTagBusinessRules contentTagBusinessRules)
        {
            _mapper = mapper;
            _contentTagRepository = contentTagRepository;
            _contentTagBusinessRules = contentTagBusinessRules;
        }

        public async Task<GetByIdContentTagResponse> Handle(GetByIdContentTagQuery request, CancellationToken cancellationToken)
        {
            ContentTag? contentTag = await _contentTagRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _contentTagBusinessRules.ContentTagShouldExistWhenSelected(contentTag);

            GetByIdContentTagResponse response = _mapper.Map<GetByIdContentTagResponse>(contentTag);
            return response;
        }
    }
}