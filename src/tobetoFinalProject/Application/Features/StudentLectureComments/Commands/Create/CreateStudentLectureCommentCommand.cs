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
using Application.Services.ContextOperations;

namespace Application.Features.StudentLectureComments.Commands.Create;

public class CreateStudentLectureCommentCommand : IRequest<CreatedStudentLectureCommentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid LectureId { get; set; }
    public Guid? StudentId { get; set; }
    public string Comment { get; set; }

    public string[] Roles => new[] { "Student" };

    public class CreateStudentLectureCommentCommandHandler : IRequestHandler<CreateStudentLectureCommentCommand, CreatedStudentLectureCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly StudentLectureCommentBusinessRules _studentLectureCommentBusinessRules;
        private readonly IContextOperationService _contextOperationService;

        public CreateStudentLectureCommentCommandHandler(IMapper mapper, IStudentLectureCommentRepository studentLectureCommentRepository,
                                         StudentLectureCommentBusinessRules studentLectureCommentBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _studentLectureCommentBusinessRules = studentLectureCommentBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentLectureCommentResponse> Handle(CreateStudentLectureCommentCommand request, CancellationToken cancellationToken)
        {
            Student? getStudent =await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;

            StudentLectureComment studentLectureComment = _mapper.Map<StudentLectureComment>(request);

            await _studentLectureCommentRepository.AddAsync(studentLectureComment);

            CreatedStudentLectureCommentResponse response = _mapper.Map<CreatedStudentLectureCommentResponse>(studentLectureComment);
            return response;
        }
    }
}