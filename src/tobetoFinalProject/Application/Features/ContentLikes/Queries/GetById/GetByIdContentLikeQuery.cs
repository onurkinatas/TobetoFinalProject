using Application.Features.ContentLikes.Constants;
using Application.Features.ContentLikes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentLikes.Constants.ContentLikesOperationClaims;

namespace Application.Features.ContentLikes.Queries.GetById;

public class GetByIdContentLikeQuery : IRequest<GetByIdContentLikeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentLikeQueryHandler : IRequestHandler<GetByIdContentLikeQuery, GetByIdContentLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

        public GetByIdContentLikeQueryHandler(IMapper mapper, IContentLikeRepository contentLikeRepository, ContentLikeBusinessRules contentLikeBusinessRules)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
        }

        public async Task<GetByIdContentLikeResponse> Handle(GetByIdContentLikeQuery request, CancellationToken cancellationToken)
        {
            ContentLike? contentLike = await _contentLikeRepository.GetAsync(predicate: cl => cl.Id == request.Id, cancellationToken: cancellationToken);
            await _contentLikeBusinessRules.ContentLikeShouldExistWhenSelected(contentLike);

            GetByIdContentLikeResponse response = _mapper.Map<GetByIdContentLikeResponse>(contentLike);
            return response;
        }
    }
}