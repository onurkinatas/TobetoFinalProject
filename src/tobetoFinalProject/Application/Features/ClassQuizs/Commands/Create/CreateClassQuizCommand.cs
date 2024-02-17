using Application.Features.ClassQuizs.Constants;
using Application.Features.ClassQuizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ClassQuizs.Constants.ClassQuizsOperationClaims;

namespace Application.Features.ClassQuizs.Commands.Create;

public class CreateClassQuizCommand : IRequest<CreatedClassQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassQuizsOperationClaims.Create };

    public class CreateClassQuizCommandHandler : IRequestHandler<CreateClassQuizCommand, CreatedClassQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassQuizRepository _classQuizRepository;
        private readonly ClassQuizBusinessRules _classQuizBusinessRules;

        public CreateClassQuizCommandHandler(IMapper mapper, IClassQuizRepository classQuizRepository,
                                         ClassQuizBusinessRules classQuizBusinessRules)
        {
            _mapper = mapper;
            _classQuizRepository = classQuizRepository;
            _classQuizBusinessRules = classQuizBusinessRules;
        }

        public async Task<CreatedClassQuizResponse> Handle(CreateClassQuizCommand request, CancellationToken cancellationToken)
        {
            ClassQuiz classQuiz = _mapper.Map<ClassQuiz>(request);

            await _classQuizRepository.AddAsync(classQuiz);

            CreatedClassQuizResponse response = _mapper.Map<CreatedClassQuizResponse>(classQuiz);
            return response;
        }
    }
}