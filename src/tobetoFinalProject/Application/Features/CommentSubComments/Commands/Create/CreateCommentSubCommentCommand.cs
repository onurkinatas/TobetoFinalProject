using Application.Features.CommentSubComments.Constants;
using Application.Features.CommentSubComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CommentSubComments.Constants.CommentSubCommentsOperationClaims;

namespace Application.Features.CommentSubComments.Commands.Create;

public class CreateCommentSubCommentCommand : IRequest<CreatedCommentSubCommentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int UserLectureCommentId { get; set; }
    public Guid StudentId { get; set; }
    public string SubComment { get; set; }

    public string[] Roles => new[] { Admin, Write, CommentSubCommentsOperationClaims.Create };

    public class CreateCommentSubCommentCommandHandler : IRequestHandler<CreateCommentSubCommentCommand, CreatedCommentSubCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentSubCommentRepository _commentSubCommentRepository;
        private readonly CommentSubCommentBusinessRules _commentSubCommentBusinessRules;

        public CreateCommentSubCommentCommandHandler(IMapper mapper, ICommentSubCommentRepository commentSubCommentRepository,
                                         CommentSubCommentBusinessRules commentSubCommentBusinessRules)
        {
            _mapper = mapper;
            _commentSubCommentRepository = commentSubCommentRepository;
            _commentSubCommentBusinessRules = commentSubCommentBusinessRules;
        }

        public async Task<CreatedCommentSubCommentResponse> Handle(CreateCommentSubCommentCommand request, CancellationToken cancellationToken)
        {
            CommentSubComment commentSubComment = _mapper.Map<CommentSubComment>(request);

            await _commentSubCommentRepository.AddAsync(commentSubComment);

            CreatedCommentSubCommentResponse response = _mapper.Map<CreatedCommentSubCommentResponse>(commentSubComment);
            return response;
        }
    }
}