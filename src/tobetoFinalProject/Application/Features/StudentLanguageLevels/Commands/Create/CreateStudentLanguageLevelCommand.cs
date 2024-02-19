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

namespace Application.Features.StudentLanguageLevels.Commands.Create;

public class CreateStudentLanguageLevelCommand : IRequest<CreatedStudentLanguageLevelResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int? UserId { get; set; }

    public string CacheGroupKey => $"GetStudent{UserId}";
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public Guid? StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentLanguageLevelsOperationClaims.Create, "Student" };


    public class CreateStudentLanguageLevelCommandHandler : IRequestHandler<CreateStudentLanguageLevelCommand, CreatedStudentLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public CreateStudentLanguageLevelCommandHandler(IMapper mapper, IStudentLanguageLevelRepository studentLanguageLevelRepository,
                                         StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentLanguageLevelResponse> Handle(CreateStudentLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;
            StudentLanguageLevel studentLanguageLevel = _mapper.Map<StudentLanguageLevel>(request);
            await _studentLanguageLevelBusinessRules.StudentLanguageLevelShouldNotExistsWhenInsert(studentLanguageLevel.LanguageLevelId, studentLanguageLevel.StudentId);
            await _studentLanguageLevelRepository.AddAsync(studentLanguageLevel);

            CreatedStudentLanguageLevelResponse response = _mapper.Map<CreatedStudentLanguageLevelResponse>(studentLanguageLevel);
            return response;
        }
    }
}