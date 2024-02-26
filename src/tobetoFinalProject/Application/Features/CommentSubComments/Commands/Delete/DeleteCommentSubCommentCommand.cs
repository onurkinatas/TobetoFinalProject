using Application.Features.CommentSubComments.Constants;
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
using Application.Services.ContextOperations;

namespace Application.Features.CommentSubComments.Commands.Delete;

public class DeleteCommentSubCommentCommand : IRequest<DeletedCommentSubCommentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { "Student" };

    public class DeleteCommentSubCommentCommandHandler : IRequestHandler<DeleteCommentSubCommentCommand, DeletedCommentSubCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentSubCommentRepository _commentSubCommentRepository;
        private readonly CommentSubCommentBusinessRules _commentSubCommentBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public DeleteCommentSubCommentCommandHandler(IMapper mapper, ICommentSubCommentRepository commentSubCommentRepository,
                                         CommentSubCommentBusinessRules commentSubCommentBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _commentSubCommentRepository = commentSubCommentRepository;
            _commentSubCommentBusinessRules = commentSubCommentBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<DeletedCommentSubCommentResponse> Handle(DeleteCommentSubCommentCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            CommentSubComment? commentSubComment = await _commentSubCommentRepository.GetAsync(predicate: csc => csc.Id == request.Id, cancellationToken: cancellationToken);
            await _commentSubCommentBusinessRules.CommentSubCommentShouldExistWhenSelected(commentSubComment);
            await _commentSubCommentBusinessRules.HaveToActiveStudent(commentSubComment.StudentId, getStudent.Id);
            await _commentSubCommentRepository.DeleteAsync(commentSubComment!);

            DeletedCommentSubCommentResponse response = _mapper.Map<DeletedCommentSubCommentResponse>(commentSubComment);
            return response;
        }
    }
}