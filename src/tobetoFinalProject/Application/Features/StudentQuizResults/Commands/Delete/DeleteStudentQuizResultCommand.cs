using Application.Features.StudentQuizResults.Constants;
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

namespace Application.Features.StudentQuizResults.Commands.Delete;

public class DeleteStudentQuizResultCommand : IRequest<DeletedStudentQuizResultResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentQuizResultsOperationClaims.Delete };

    public class DeleteStudentQuizResultCommandHandler : IRequestHandler<DeleteStudentQuizResultCommand, DeletedStudentQuizResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly StudentQuizResultBusinessRules _studentQuizResultBusinessRules;

        public DeleteStudentQuizResultCommandHandler(IMapper mapper, IStudentQuizResultRepository studentQuizResultRepository,
                                         StudentQuizResultBusinessRules studentQuizResultBusinessRules)
        {
            _mapper = mapper;
            _studentQuizResultRepository = studentQuizResultRepository;
            _studentQuizResultBusinessRules = studentQuizResultBusinessRules;
        }

        public async Task<DeletedStudentQuizResultResponse> Handle(DeleteStudentQuizResultCommand request, CancellationToken cancellationToken)
        {
            StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(predicate: sqr => sqr.Id == request.Id, cancellationToken: cancellationToken);
            await _studentQuizResultBusinessRules.StudentQuizResultShouldExistWhenSelected(studentQuizResult);

            await _studentQuizResultRepository.DeleteAsync(studentQuizResult!);

            DeletedStudentQuizResultResponse response = _mapper.Map<DeletedStudentQuizResultResponse>(studentQuizResult);
            return response;
        }
    }
}