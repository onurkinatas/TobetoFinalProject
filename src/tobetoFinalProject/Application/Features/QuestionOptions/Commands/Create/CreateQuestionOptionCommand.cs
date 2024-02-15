using Application.Features.QuestionOptions.Constants;
using Application.Features.QuestionOptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.QuestionOptions.Constants.QuestionOptionsOperationClaims;

namespace Application.Features.QuestionOptions.Commands.Create;

public class CreateQuestionOptionCommand : IRequest<CreatedQuestionOptionResponse>, /*ISecuredRequest, */ILoggableRequest, ITransactionalRequest
{
    public int OptionId { get; set; }
    public int QuestionId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuestionOptionsOperationClaims.Create };

    public class CreateQuestionOptionCommandHandler : IRequestHandler<CreateQuestionOptionCommand, CreatedQuestionOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly QuestionOptionBusinessRules _questionOptionBusinessRules;

        public CreateQuestionOptionCommandHandler(IMapper mapper, IQuestionOptionRepository questionOptionRepository,
                                         QuestionOptionBusinessRules questionOptionBusinessRules)
        {
            _mapper = mapper;
            _questionOptionRepository = questionOptionRepository;
            _questionOptionBusinessRules = questionOptionBusinessRules;
        }

        public async Task<CreatedQuestionOptionResponse> Handle(CreateQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            QuestionOption questionOption = _mapper.Map<QuestionOption>(request);

            await _questionOptionRepository.AddAsync(questionOption);

            CreatedQuestionOptionResponse response = _mapper.Map<CreatedQuestionOptionResponse>(questionOption);
            return response;
        }
    }
}