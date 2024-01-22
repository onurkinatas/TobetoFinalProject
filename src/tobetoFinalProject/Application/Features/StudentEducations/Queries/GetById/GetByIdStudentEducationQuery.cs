using Application.Features.StudentEducations.Constants;
using Application.Features.StudentEducations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentEducations.Constants.StudentEducationsOperationClaims;
using Application.Services.CacheForMemory;

namespace Application.Features.StudentEducations.Queries.GetById;

public class GetByIdStudentEducationQuery : IRequest<GetByIdStudentEducationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentEducationQueryHandler : IRequestHandler<GetByIdStudentEducationQuery, GetByIdStudentEducationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentEducationRepository _studentEducationRepository;
        private readonly StudentEducationBusinessRules _studentEducationBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentEducationQueryHandler(IMapper mapper, IStudentEducationRepository studentEducationRepository, StudentEducationBusinessRules studentEducationBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentEducationRepository = studentEducationRepository;
            _studentEducationBusinessRules = studentEducationBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentEducationResponse> Handle(GetByIdStudentEducationQuery request, CancellationToken cancellationToken)
        {

            StudentEducation? studentEducation = await _studentEducationRepository.GetAsync(
                predicate: se => se.Id == request.Id ,
                cancellationToken: cancellationToken);
            await _studentEducationBusinessRules.StudentEducationShouldExistWhenSelected(studentEducation);

            GetByIdStudentEducationResponse response = _mapper.Map<GetByIdStudentEducationResponse>(studentEducation);
            return response;
        }
    }
}