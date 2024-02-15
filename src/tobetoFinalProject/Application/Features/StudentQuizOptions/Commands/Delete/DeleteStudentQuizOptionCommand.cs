using Application.Features.StudentQuizOptions.Constants;
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

namespace Application.Features.StudentQuizOptions.Commands.Delete;

public class DeleteStudentQuizOptionCommand : IRequest<DeletedStudentQuizOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentQuizOptionsOperationClaims.Delete };

    public class DeleteStudentQuizOptionCommandHandler : IRequestHandler<DeleteStudentQuizOptionCommand, DeletedStudentQuizOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;
        private readonly StudentQuizOptionBusinessRules _studentQuizOptionBusinessRules;

        public DeleteStudentQuizOptionCommandHandler(IMapper mapper, IStudentQuizOptionRepository studentQuizOptionRepository,
                                         StudentQuizOptionBusinessRules studentQuizOptionBusinessRules)
        {
            _mapper = mapper;
            _studentQuizOptionRepository = studentQuizOptionRepository;
            _studentQuizOptionBusinessRules = studentQuizOptionBusinessRules;
        }

        public async Task<DeletedStudentQuizOptionResponse> Handle(DeleteStudentQuizOptionCommand request, CancellationToken cancellationToken)
        {
            StudentQuizOption? studentQuizOption = await _studentQuizOptionRepository.GetAsync(predicate: sqo => sqo.Id == request.Id, cancellationToken: cancellationToken);
            await _studentQuizOptionBusinessRules.StudentQuizOptionShouldExistWhenSelected(studentQuizOption);

            await _studentQuizOptionRepository.DeleteAsync(studentQuizOption!);

            DeletedStudentQuizOptionResponse response = _mapper.Map<DeletedStudentQuizOptionResponse>(studentQuizOption);
            return response;
        }
    }
}