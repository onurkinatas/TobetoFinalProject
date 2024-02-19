using Application.Features.StudentLanguageLevels.Constants;
using Application.Features.StudentLanguageLevels.Constants;
using Application.Features.StudentLanguageLevels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentLanguageLevels.Constants.StudentLanguageLevelsOperationClaims;

namespace Application.Features.StudentLanguageLevels.Commands.Delete;

public class DeleteStudentLanguageLevelCommand : IRequest<DeletedStudentLanguageLevelResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentLanguageLevelsOperationClaims.Delete, "Student" };

    public int? UserId { get; set; }

    public string CacheGroupKey => $"GetStudent{UserId}";
    public bool BypassCache { get; }
    public string? CacheKey { get; }

    public class DeleteStudentLanguageLevelCommandHandler : IRequestHandler<DeleteStudentLanguageLevelCommand, DeletedStudentLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;

        public DeleteStudentLanguageLevelCommandHandler(IMapper mapper, IStudentLanguageLevelRepository studentLanguageLevelRepository,
                                         StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules)
        {
            _mapper = mapper;
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
        }

        public async Task<DeletedStudentLanguageLevelResponse> Handle(DeleteStudentLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            StudentLanguageLevel? studentLanguageLevel = await _studentLanguageLevelRepository.GetAsync(predicate: sll => sll.Id == request.Id, cancellationToken: cancellationToken);
            await _studentLanguageLevelBusinessRules.StudentLanguageLevelShouldExistWhenSelected(studentLanguageLevel);

            await _studentLanguageLevelRepository.DeleteAsync(studentLanguageLevel!);

            DeletedStudentLanguageLevelResponse response = _mapper.Map<DeletedStudentLanguageLevelResponse>(studentLanguageLevel);
            return response;
        }
    }
}