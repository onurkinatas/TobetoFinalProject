using Application.Features.StudentSocialMedias.Constants;
using Application.Features.StudentSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentSocialMedias.Constants.StudentSocialMediasOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;
using Application.Features.StudentSkills.Constants;

namespace Application.Features.StudentSocialMedias.Queries.GetById;

public class GetByIdStudentSocialMediaQuery : IRequest<GetByIdStudentSocialMediaResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentSocialMediaQueryHandler : IRequestHandler<GetByIdStudentSocialMediaQuery, GetByIdStudentSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly StudentSocialMediaBusinessRules _studentSocialMediaBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentSocialMediaQueryHandler(IMapper mapper, IStudentSocialMediaRepository studentSocialMediaRepository, StudentSocialMediaBusinessRules studentSocialMediaBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _studentSocialMediaBusinessRules = studentSocialMediaBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentSocialMediaResponse> Handle(GetByIdStudentSocialMediaQuery request, CancellationToken cancellationToken)
        {
           

            StudentSocialMedia? studentSocialMedia = await _studentSocialMediaRepository.GetAsync(
                predicate: ssm => ssm.Id == request.Id,
                include: se => se.Include(se => se.SocialMedia)
                .Include(se => se.Student)
                .ThenInclude(s => s.User),
                cancellationToken: cancellationToken);
            await _studentSocialMediaBusinessRules.StudentSocialMediaShouldExistWhenSelected(studentSocialMedia);

            GetByIdStudentSocialMediaResponse response = _mapper.Map<GetByIdStudentSocialMediaResponse>(studentSocialMedia);
            return response;
        }
    }
}