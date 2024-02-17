using Application.Features.StudentQuizResults.Constants;
using Application.Features.StudentQuizResults.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentQuizResults.Constants.StudentQuizResultsOperationClaims;
using Application.Services.ContextOperations;

namespace Application.Features.StudentQuizResults.Commands.Create;

public class CreateStudentQuizResultCommand : IRequest<CreatedStudentQuizResultResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? StudentId { get; set; }
    public int QuizId { get; set; }

    public string[] Roles => new[] { Admin,"Student" };

    public class CreateStudentQuizResultCommandHandler : IRequestHandler<CreateStudentQuizResultCommand, CreatedStudentQuizResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly StudentQuizResultBusinessRules _studentQuizResultBusinessRules;
        private readonly IContextOperationService _contextOperationService;

        public CreateStudentQuizResultCommandHandler(IMapper mapper, IStudentQuizResultRepository studentQuizResultRepository,
                                         StudentQuizResultBusinessRules studentQuizResultBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentQuizResultRepository = studentQuizResultRepository;
            _studentQuizResultBusinessRules = studentQuizResultBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentQuizResultResponse> Handle(CreateStudentQuizResultCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;
            StudentQuizResult studentQuizResult = _mapper.Map<StudentQuizResult>(request);

            await _studentQuizResultBusinessRules.StudentJoinControl(studentQuizResult.QuizId, studentQuizResult.StudentId);   

            await _studentQuizResultRepository.AddAsync(studentQuizResult);

            CreatedStudentQuizResultResponse response = _mapper.Map<CreatedStudentQuizResultResponse>(studentQuizResult);
            return response;
        }
    }
}