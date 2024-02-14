using Application.Features.QuestionOptions.Constants;
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

namespace Application.Features.QuestionOptions.Commands.Delete;

public class DeleteQuestionOptionCommand : IRequest<DeletedQuestionOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, QuestionOptionsOperationClaims.Delete };

    public class DeleteQuestionOptionCommandHandler : IRequestHandler<DeleteQuestionOptionCommand, DeletedQuestionOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly QuestionOptionBusinessRules _questionOptionBusinessRules;

        public DeleteQuestionOptionCommandHandler(IMapper mapper, IQuestionOptionRepository questionOptionRepository,
                                         QuestionOptionBusinessRules questionOptionBusinessRules)
        {
            _mapper = mapper;
            _questionOptionRepository = questionOptionRepository;
            _questionOptionBusinessRules = questionOptionBusinessRules;
        }

        public async Task<DeletedQuestionOptionResponse> Handle(DeleteQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            QuestionOption? questionOption = await _questionOptionRepository.GetAsync(predicate: qo => qo.Id == request.Id, cancellationToken: cancellationToken);
            await _questionOptionBusinessRules.QuestionOptionShouldExistWhenSelected(questionOption);

            await _questionOptionRepository.DeleteAsync(questionOption!);

            DeletedQuestionOptionResponse response = _mapper.Map<DeletedQuestionOptionResponse>(questionOption);
            return response;
        }
    }
}