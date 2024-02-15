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

namespace Application.Features.StudentQuizOptions.Commands.Update;

public class UpdateStudentQuizOptionCommand : IRequest<UpdatedStudentQuizOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public Guid ExamId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
    public Guid StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentQuizOptionsOperationClaims.Update };

    public class UpdateStudentQuizOptionCommandHandler : IRequestHandler<UpdateStudentQuizOptionCommand, UpdatedStudentQuizOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;
        private readonly StudentQuizOptionBusinessRules _studentQuizOptionBusinessRules;

        public UpdateStudentQuizOptionCommandHandler(IMapper mapper, IStudentQuizOptionRepository studentQuizOptionRepository,
                                         StudentQuizOptionBusinessRules studentQuizOptionBusinessRules)
        {
            _mapper = mapper;
            _studentQuizOptionRepository = studentQuizOptionRepository;
            _studentQuizOptionBusinessRules = studentQuizOptionBusinessRules;
        }

        public async Task<UpdatedStudentQuizOptionResponse> Handle(UpdateStudentQuizOptionCommand request, CancellationToken cancellationToken)
        {
            StudentQuizOption? studentQuizOption = await _studentQuizOptionRepository.GetAsync(predicate: sqo => sqo.Id == request.Id, cancellationToken: cancellationToken);
            await _studentQuizOptionBusinessRules.StudentQuizOptionShouldExistWhenSelected(studentQuizOption);
            studentQuizOption = _mapper.Map(request, studentQuizOption);

            await _studentQuizOptionRepository.UpdateAsync(studentQuizOption!);

            UpdatedStudentQuizOptionResponse response = _mapper.Map<UpdatedStudentQuizOptionResponse>(studentQuizOption);
            return response;
        }
    }
}