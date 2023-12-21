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

namespace Application.Features.StudentLanguageLevels.Commands.Create;

public class CreateStudentLanguageLevelCommand : IRequest<CreatedStudentLanguageLevelResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentLanguageLevelsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentLanguageLevels";

    public class CreateStudentLanguageLevelCommandHandler : IRequestHandler<CreateStudentLanguageLevelCommand, CreatedStudentLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;

        public CreateStudentLanguageLevelCommandHandler(IMapper mapper, IStudentLanguageLevelRepository studentLanguageLevelRepository,
                                         StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules)
        {
            _mapper = mapper;
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
        }

        public async Task<CreatedStudentLanguageLevelResponse> Handle(CreateStudentLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            StudentLanguageLevel studentLanguageLevel = _mapper.Map<StudentLanguageLevel>(request);

            await _studentLanguageLevelRepository.AddAsync(studentLanguageLevel);

            CreatedStudentLanguageLevelResponse response = _mapper.Map<CreatedStudentLanguageLevelResponse>(studentLanguageLevel);
            return response;
        }
    }
}