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


        public CreateStudentQuizOptionCommandHandler(IMapper mapper, IStudentQuizOptionRepository studentQuizOptionRepository,
                                         StudentQuizOptionBusinessRules studentQuizOptionBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentQuizOptionRepository = studentQuizOptionRepository;
            _studentQuizOptionBusinessRules = studentQuizOptionBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentQuizOptionResponse> Handle(CreateStudentQuizOptionCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;

            StudentQuizOption studentQuizOption = _mapper.Map<StudentQuizOption>(request);

            StudentQuizOption? existStudentQuizOption = await _studentQuizOptionRepository.GetAsync(
                predicate: sqo => sqo.QuestionId == request.QuestionId && sqo.StudentId == studentQuizOption.StudentId
                );
            
            if ( existStudentQuizOption is not null)
            {
                existStudentQuizOption.OptionId = request.OptionId;
                await _studentQuizOptionRepository.UpdateAsync(existStudentQuizOption);
            }
            else if(existStudentQuizOption is null)
                await _studentQuizOptionRepository.AddAsync(studentQuizOption);

            CreatedStudentQuizOptionResponse response = _mapper.Map<CreatedStudentQuizOptionResponse>(studentQuizOption);
            return response;
        }
    }
}