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

namespace Application.Features.QuestionOptions.Commands.Update;

public class UpdateQuestionOptionCommand : IRequest<UpdatedQuestionOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuestionOptionsOperationClaims.Update };

    public class UpdateQuestionOptionCommandHandler : IRequestHandler<UpdateQuestionOptionCommand, UpdatedQuestionOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly QuestionOptionBusinessRules _questionOptionBusinessRules;

        public UpdateQuestionOptionCommandHandler(IMapper mapper, IQuestionOptionRepository questionOptionRepository,
                                         QuestionOptionBusinessRules questionOptionBusinessRules)
        {
            _mapper = mapper;
            _questionOptionRepository = questionOptionRepository;
            _questionOptionBusinessRules = questionOptionBusinessRules;
        }

        public async Task<UpdatedQuestionOptionResponse> Handle(UpdateQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            QuestionOption? questionOption = await _questionOptionRepository.GetAsync(predicate: qo => qo.Id == request.Id, cancellationToken: cancellationToken);
            await _questionOptionBusinessRules.QuestionOptionShouldExistWhenSelected(questionOption);
            questionOption = _mapper.Map(request, questionOption);

            await _questionOptionRepository.UpdateAsync(questionOption!);

            UpdatedQuestionOptionResponse response = _mapper.Map<UpdatedQuestionOptionResponse>(questionOption);
            return response;
        }
    }
}