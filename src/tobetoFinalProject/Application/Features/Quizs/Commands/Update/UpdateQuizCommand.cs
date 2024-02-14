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

namespace Application.Features.Quizs.Commands.Update;

public class UpdateQuizCommand : IRequest<UpdatedQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public Guid ExamId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuizsOperationClaims.Update };

    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, UpdatedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;

        public UpdateQuizCommandHandler(IMapper mapper, IQuizRepository quizRepository,
                                         QuizBusinessRules quizBusinessRules)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
        }

        public async Task<UpdatedQuizResponse> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz? quiz = await _quizRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _quizBusinessRules.QuizShouldExistWhenSelected(quiz);
            quiz = _mapper.Map(request, quiz);

            await _quizRepository.UpdateAsync(quiz!);

            UpdatedQuizResponse response = _mapper.Map<UpdatedQuizResponse>(quiz);
            return response;
        }
    }
}