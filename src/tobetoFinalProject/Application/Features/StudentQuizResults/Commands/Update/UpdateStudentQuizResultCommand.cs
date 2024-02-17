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

namespace Application.Features.StudentQuizResults.Commands.Update;

public class UpdateStudentQuizResultCommand : IRequest<UpdatedStudentQuizResultResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public Guid StudentId { get; set; }
    public int QuizId { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int WrongAnswerCount { get; set; }
    public int EmptyAnswerCount { get; set; }
    public Student? Student { get; set; }
    public Quiz? Quiz { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentQuizResultsOperationClaims.Update };

    public class UpdateStudentQuizResultCommandHandler : IRequestHandler<UpdateStudentQuizResultCommand, UpdatedStudentQuizResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly StudentQuizResultBusinessRules _studentQuizResultBusinessRules;

        public UpdateStudentQuizResultCommandHandler(IMapper mapper, IStudentQuizResultRepository studentQuizResultRepository,
                                         StudentQuizResultBusinessRules studentQuizResultBusinessRules)
        {
            _mapper = mapper;
            _studentQuizResultRepository = studentQuizResultRepository;
            _studentQuizResultBusinessRules = studentQuizResultBusinessRules;
        }

        public async Task<UpdatedStudentQuizResultResponse> Handle(UpdateStudentQuizResultCommand request, CancellationToken cancellationToken)
        {
            StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(predicate: sqr => sqr.Id == request.Id, cancellationToken: cancellationToken);
            await _studentQuizResultBusinessRules.StudentQuizResultShouldExistWhenSelected(studentQuizResult);
            studentQuizResult = _mapper.Map(request, studentQuizResult);

            await _studentQuizResultRepository.UpdateAsync(studentQuizResult!);

            UpdatedStudentQuizResultResponse response = _mapper.Map<UpdatedStudentQuizResultResponse>(studentQuizResult);
            return response;
        }
    }
}