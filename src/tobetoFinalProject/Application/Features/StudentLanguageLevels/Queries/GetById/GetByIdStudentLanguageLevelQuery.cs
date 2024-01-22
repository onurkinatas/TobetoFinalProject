using Application.Features.StudentLanguageLevels.Constants;
using Application.Features.StudentLanguageLevels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentLanguageLevels.Constants.StudentLanguageLevelsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;

namespace Application.Features.StudentLanguageLevels.Queries.GetById;

public class GetByIdStudentLanguageLevelQuery : IRequest<GetByIdStudentLanguageLevelResponse> /*ISecuredRequest*/
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentLanguageLevelQueryHandler : IRequestHandler<GetByIdStudentLanguageLevelQuery, GetByIdStudentLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentLanguageLevelQueryHandler(IMapper mapper, IStudentLanguageLevelRepository studentLanguageLevelRepository, StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentLanguageLevelResponse> Handle(GetByIdStudentLanguageLevelQuery request, CancellationToken cancellationToken)
        {
           

            StudentLanguageLevel? studentLanguageLevel = await _studentLanguageLevelRepository.GetAsync(
                predicate: sll => sll.Id == request.Id,
                include: sll => sll.Include(sll => sll.Student)
                    .Include(sll => sll.LanguageLevel)
                    .ThenInclude(ll => ll.Language),
                cancellationToken: cancellationToken);
            await _studentLanguageLevelBusinessRules.StudentLanguageLevelShouldExistWhenSelected(studentLanguageLevel);

            GetByIdStudentLanguageLevelResponse response = _mapper.Map<GetByIdStudentLanguageLevelResponse>(studentLanguageLevel);
            return response;
        }
    }
}