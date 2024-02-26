using Application.Features.CommentSubComments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.CommentSubComments.Constants.CommentSubCommentsOperationClaims;

namespace Application.Features.CommentSubComments.Queries.GetList;

public class GetListCommentSubCommentQuery : IRequest<GetListResponse<GetListCommentSubCommentListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListCommentSubCommentQueryHandler : IRequestHandler<GetListCommentSubCommentQuery, GetListResponse<GetListCommentSubCommentListItemDto>>
    {
        private readonly ICommentSubCommentRepository _commentSubCommentRepository;
        private readonly IMapper _mapper;

        public GetListCommentSubCommentQueryHandler(ICommentSubCommentRepository commentSubCommentRepository, IMapper mapper)
        {
            _commentSubCommentRepository = commentSubCommentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCommentSubCommentListItemDto>> Handle(GetListCommentSubCommentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CommentSubComment> commentSubComments = await _commentSubCommentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCommentSubCommentListItemDto> response = _mapper.Map<GetListResponse<GetListCommentSubCommentListItemDto>>(commentSubComments);
            return response;
        }
    }
}