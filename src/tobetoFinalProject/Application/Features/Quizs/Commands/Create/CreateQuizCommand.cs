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

namespace Application.Features.Quizs.Commands.Create;

public class CreateQuizCommand : IRequest<CreatedQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public Guid ExamId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuizsOperationClaims.Create };

    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, CreatedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;

        public CreateQuizCommandHandler(IMapper mapper, IQuizRepository quizRepository,
                                         QuizBusinessRules quizBusinessRules)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
        }

        public async Task<CreatedQuizResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz quiz = _mapper.Map<Quiz>(request);

            await _quizRepository.AddAsync(quiz);

            CreatedQuizResponse response = _mapper.Map<CreatedQuizResponse>(quiz);
            return response;
        }
    }
}