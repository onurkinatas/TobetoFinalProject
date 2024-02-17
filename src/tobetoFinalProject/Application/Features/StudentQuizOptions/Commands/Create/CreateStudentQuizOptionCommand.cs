using Application.Features.StudentQuizOptions.Constants;
using Application.Features.StudentQuizOptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentQuizOptions.Constants.StudentQuizOptionsOperationClaims;
using Application.Services.ContextOperations;
using Application.Services.Questions;
using Application.Services.StudentQuizResults;

namespace Application.Features.StudentQuizOptions.Commands.Create;

public class CreateStudentQuizOptionCommand : IRequest<CreatedStudentQuizOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public Guid? StudentId { get; set; }


    public string[] Roles => new[] { "Student" };

    public class CreateStudentQuizOptionCommandHandler : IRequestHandler<CreateStudentQuizOptionCommand, CreatedStudentQuizOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;
        private readonly StudentQuizOptionBusinessRules _studentQuizOptionBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly IQuestionsService _questionsService;
        private readonly IStudentQuizResultsService _studentQuizResultsService;

        public CreateStudentQuizOptionCommandHandler(IMapper mapper, IStudentQuizOptionRepository studentQuizOptionRepository,
                                         StudentQuizOptionBusinessRules studentQuizOptionBusinessRules, IContextOperationService contextOperationService, IQuestionsService questionsService, IStudentQuizResultsService studentQuizResultsService)
        {
            _mapper = mapper;
            _studentQuizOptionRepository = studentQuizOptionRepository;
            _studentQuizOptionBusinessRules = studentQuizOptionBusinessRules;
            _contextOperationService = contextOperationService;
            _questionsService = questionsService;
            _studentQuizResultsService = studentQuizResultsService;
        }

        public async Task<CreatedStudentQuizOptionResponse> Handle(CreateStudentQuizOptionCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;

            StudentQuizOption studentQuizOption = _mapper.Map<StudentQuizOption>(request);

            await _studentQuizOptionRepository.AddAsync(studentQuizOption);
            await _studentQuizResultsService.UpdateQuizResultAsync(studentQuizOption.QuizId, studentQuizOption.StudentId, studentQuizOption.OptionId, studentQuizOption.QuestionId);

                

            CreatedStudentQuizOptionResponse response = _mapper.Map<CreatedStudentQuizOptionResponse>(studentQuizOption);
            return response;
        }
    }
}