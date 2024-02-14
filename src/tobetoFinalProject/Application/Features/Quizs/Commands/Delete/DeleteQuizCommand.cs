using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;

namespace Application.Features.Quizs.Commands.Delete;

public class DeleteQuizCommand : IRequest<DeletedQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, QuizsOperationClaims.Delete };

    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, DeletedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;

        public DeleteQuizCommandHandler(IMapper mapper, IQuizRepository quizRepository,
                                         QuizBusinessRules quizBusinessRules)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
        }

        public async Task<DeletedQuizResponse> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz? quiz = await _quizRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _quizBusinessRules.QuizShouldExistWhenSelected(quiz);

            await _quizRepository.DeleteAsync(quiz!);

            DeletedQuizResponse response = _mapper.Map<DeletedQuizResponse>(quiz);
            return response;
        }
    }
}