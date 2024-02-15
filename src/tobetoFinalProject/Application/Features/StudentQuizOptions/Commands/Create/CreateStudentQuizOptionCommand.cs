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
    public List<StudentQuizOption> StudentQuizOptions { get; set; }


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

            request.StudentQuizOptions =  request.StudentQuizOptions.Select(sqo => { sqo.StudentId = getStudent.Id;return sqo; }).ToList();

            await _studentQuizOptionRepository.AddRangeAsync(request.StudentQuizOptions);

            CreatedStudentQuizOptionResponse response = _mapper.Map<CreatedStudentQuizOptionResponse>(request.StudentQuizOptions);
            return response;
        }
    }
}