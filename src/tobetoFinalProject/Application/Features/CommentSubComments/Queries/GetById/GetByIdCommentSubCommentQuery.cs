using Application.Features.CommentSubComments.Constants;
using Application.Features.CommentSubComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.CommentSubComments.Constants.CommentSubCommentsOperationClaims;

namespace Application.Features.CommentSubComments.Queries.GetById;

public class GetByIdCommentSubCommentQuery : IRequest<GetByIdCommentSubCommentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdCommentSubCommentQueryHandler : IRequestHandler<GetByIdCommentSubCommentQuery, GetByIdCommentSubCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentSubCommentRepository _commentSubCommentRepository;
        private readonly CommentSubCommentBusinessRules _commentSubCommentBusinessRules;

        public GetByIdCommentSubCommentQueryHandler(IMapper mapper, ICommentSubCommentRepository commentSubCommentRepository, CommentSubCommentBusinessRules commentSubCommentBusinessRules)
        {
            _mapper = mapper;
            _commentSubCommentRepository = commentSubCommentRepository;
            _commentSubCommentBusinessRules = commentSubCommentBusinessRules;
        }

        public async Task<GetByIdCommentSubCommentResponse> Handle(GetByIdCommentSubCommentQuery request, CancellationToken cancellationToken)
        {
            CommentSubComment? commentSubComment = await _commentSubCommentRepository.GetAsync(predicate: csc => csc.Id == request.Id, cancellationToken: cancellationToken);
            await _commentSubCommentBusinessRules.CommentSubCommentShouldExistWhenSelected(commentSubComment);

            GetByIdCommentSubCommentResponse response = _mapper.Map<GetByIdCommentSubCommentResponse>(commentSubComment);
            return response;
        }
    }
}