using Application.Features.StudentLanguageLevels.Constants;
using Application.Features.StudentLanguageLevels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentLanguageLevels.Constants.StudentLanguageLevelsOperationClaims;

namespace Application.Features.StudentLanguageLevels.Queries.GetById;

public class GetByIdStudentLanguageLevelQuery : IRequest<GetByIdStudentLanguageLevelResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentLanguageLevelQueryHandler : IRequestHandler<GetByIdStudentLanguageLevelQuery, GetByIdStudentLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;

        public GetByIdStudentLanguageLevelQueryHandler(IMapper mapper, IStudentLanguageLevelRepository studentLanguageLevelRepository, StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules)
        {
            _mapper = mapper;
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
        }

        public async Task<GetByIdStudentLanguageLevelResponse> Handle(GetByIdStudentLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            StudentLanguageLevel? studentLanguageLevel = await _studentLanguageLevelRepository.GetAsync(predicate: sll => sll.Id == request.Id, cancellationToken: cancellationToken);
            await _studentLanguageLevelBusinessRules.StudentLanguageLevelShouldExistWhenSelected(studentLanguageLevel);

            GetByIdStudentLanguageLevelResponse response = _mapper.Map<GetByIdStudentLanguageLevelResponse>(studentLanguageLevel);
            return response;
        }
    }
}