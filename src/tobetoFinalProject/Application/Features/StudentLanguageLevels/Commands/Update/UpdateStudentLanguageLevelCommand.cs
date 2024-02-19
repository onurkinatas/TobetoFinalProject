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
using Application.Services.ContextOperations;

namespace Application.Features.StudentLanguageLevels.Commands.Update;

public class UpdateStudentLanguageLevelCommand : IRequest<UpdatedStudentLanguageLevelResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid? StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentLanguageLevelsOperationClaims.Update, "Student" };

    public int? UserId { get; set; }

    public string CacheGroupKey => $"GetStudent{UserId}";
    public bool BypassCache { get; }
    public string? CacheKey { get; }

    public class UpdateStudentLanguageLevelCommandHandler : IRequestHandler<UpdateStudentLanguageLevelCommand, UpdatedStudentLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public UpdateStudentLanguageLevelCommandHandler(IMapper mapper, IStudentLanguageLevelRepository studentLanguageLevelRepository,
                                         StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<UpdatedStudentLanguageLevelResponse> Handle(UpdateStudentLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;
            StudentLanguageLevel? studentLanguageLevel = await _studentLanguageLevelRepository.GetAsync(predicate: sll => sll.Id == request.Id, cancellationToken: cancellationToken);
            await _studentLanguageLevelBusinessRules.StudentLanguageLevelShouldExistWhenSelected(studentLanguageLevel);
            studentLanguageLevel = _mapper.Map(request, studentLanguageLevel);

            await _studentLanguageLevelRepository.UpdateAsync(studentLanguageLevel!);

            UpdatedStudentLanguageLevelResponse response = _mapper.Map<UpdatedStudentLanguageLevelResponse>(studentLanguageLevel);
            return response;
        }
    }
}