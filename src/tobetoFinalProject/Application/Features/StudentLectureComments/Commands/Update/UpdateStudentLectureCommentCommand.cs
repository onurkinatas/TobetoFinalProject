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

namespace Application.Features.StudentLectureComments.Commands.Update;

public class UpdateStudentLectureCommentCommand : IRequest<UpdatedStudentLectureCommentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentId { get; set; }
    public string Comment { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentLectureCommentsOperationClaims.Update };

    public class UpdateStudentLectureCommentCommandHandler : IRequestHandler<UpdateStudentLectureCommentCommand, UpdatedStudentLectureCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly StudentLectureCommentBusinessRules _studentLectureCommentBusinessRules;

        public UpdateStudentLectureCommentCommandHandler(IMapper mapper, IStudentLectureCommentRepository studentLectureCommentRepository,
                                         StudentLectureCommentBusinessRules studentLectureCommentBusinessRules)
        {
            _mapper = mapper;
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _studentLectureCommentBusinessRules = studentLectureCommentBusinessRules;
        }

        public async Task<UpdatedStudentLectureCommentResponse> Handle(UpdateStudentLectureCommentCommand request, CancellationToken cancellationToken)
        {
            StudentLectureComment? studentLectureComment = await _studentLectureCommentRepository.GetAsync(predicate: slc => slc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentLectureCommentBusinessRules.StudentLectureCommentShouldExistWhenSelected(studentLectureComment);
            studentLectureComment = _mapper.Map(request, studentLectureComment);

            await _studentLectureCommentRepository.UpdateAsync(studentLectureComment!);

            UpdatedStudentLectureCommentResponse response = _mapper.Map<UpdatedStudentLectureCommentResponse>(studentLectureComment);
            return response;
        }
    }
}