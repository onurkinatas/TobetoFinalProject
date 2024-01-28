using Application.Features.ContentLikes.Queries.GetByContentId;
using Application.Features.ContentLikes.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContentLikes.Queries.GetLectureLikeCount;
public class GetContentLikeCountQuery : IRequest<GetContentLikeCountQueryResponse>, ISecuredRequest
{
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetContentLikeCountQueryHandler : IRequestHandler<GetContentLikeCountQuery, GetContentLikeCountQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

        public GetContentLikeCountQueryHandler(IMapper mapper, IContentLikeRepository contentLikeRepository, ContentLikeBusinessRules contentLikeBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetContentLikeCountQueryResponse> Handle(GetContentLikeCountQuery request, CancellationToken cancellationToken)
        {
            int contentLikeCount = _contentLikeRepository.GetContentLikeCount(cl => cl.ContentId == request.ContentId&&cl.IsLiked==true);

            GetContentLikeCountQueryResponse getContentLikeCountQueryResponse = new GetContentLikeCountQueryResponse();
            getContentLikeCountQueryResponse.Count = contentLikeCount;
            return getContentLikeCountQueryResponse;
        }
    }
}
