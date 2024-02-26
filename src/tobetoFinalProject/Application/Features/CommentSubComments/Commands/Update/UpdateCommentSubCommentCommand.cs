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

namespace Application.Features.CommentSubComments.Commands.Update;

public class UpdateCommentSubCommentCommand : IRequest<UpdatedCommentSubCommentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int UserLectureCommentId { get; set; }
    public Guid StudentId { get; set; }
    public string SubComment { get; set; }

    public string[] Roles => new[] { Admin, Write, CommentSubCommentsOperationClaims.Update };

    public class UpdateCommentSubCommentCommandHandler : IRequestHandler<UpdateCommentSubCommentCommand, UpdatedCommentSubCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentSubCommentRepository _commentSubCommentRepository;
        private readonly CommentSubCommentBusinessRules _commentSubCommentBusinessRules;

        public UpdateCommentSubCommentCommandHandler(IMapper mapper, ICommentSubCommentRepository commentSubCommentRepository,
                                         CommentSubCommentBusinessRules commentSubCommentBusinessRules)
        {
            _mapper = mapper;
            _commentSubCommentRepository = commentSubCommentRepository;
            _commentSubCommentBusinessRules = commentSubCommentBusinessRules;
        }

        public async Task<UpdatedCommentSubCommentResponse> Handle(UpdateCommentSubCommentCommand request, CancellationToken cancellationToken)
        {
            CommentSubComment? commentSubComment = await _commentSubCommentRepository.GetAsync(predicate: csc => csc.Id == request.Id, cancellationToken: cancellationToken);
            await _commentSubCommentBusinessRules.CommentSubCommentShouldExistWhenSelected(commentSubComment);
            commentSubComment = _mapper.Map(request, commentSubComment);

            await _commentSubCommentRepository.UpdateAsync(commentSubComment!);

            UpdatedCommentSubCommentResponse response = _mapper.Map<UpdatedCommentSubCommentResponse>(commentSubComment);
            return response;
        }
    }
}