using Application.Features.StudentLectureComments.Constants;
using Application.Features.StudentLectureComments.Constants;
using Application.Features.StudentLectureComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentLectureComments.Constants.StudentLectureCommentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.ContextOperations;

namespace Application.Features.StudentLectureComments.Commands.Delete;

public class DeleteStudentLectureCommentCommand : IRequest<DeletedStudentLectureCommentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { "Student" };

    public class DeleteStudentLectureCommentCommandHandler : IRequestHandler<DeleteStudentLectureCommentCommand, DeletedStudentLectureCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly StudentLectureCommentBusinessRules _studentLectureCommentBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public DeleteStudentLectureCommentCommandHandler(IMapper mapper, IStudentLectureCommentRepository studentLectureCommentRepository,
                                         StudentLectureCommentBusinessRules studentLectureCommentBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _studentLectureCommentBusinessRules = studentLectureCommentBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<DeletedStudentLectureCommentResponse> Handle(DeleteStudentLectureCommentCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            StudentLectureComment? studentLectureComment = await _studentLectureCommentRepository.GetAsync(
                include:slc=>slc.Include(slc=>slc.CommentSubComments),
                predicate: slc => slc.Id == request.Id, cancellationToken: cancellationToken
                );
            await _studentLectureCommentBusinessRules.StudentLectureCommentShouldExistWhenSelected(studentLectureComment);
            await _studentLectureCommentBusinessRules.HaveToActiveStudent(studentLectureComment,getStudent.Id);

            await _studentLectureCommentRepository.DeleteAsync(studentLectureComment!);

            DeletedStudentLectureCommentResponse response = _mapper.Map<DeletedStudentLectureCommentResponse>(studentLectureComment);
            return response;
        }
    }
}