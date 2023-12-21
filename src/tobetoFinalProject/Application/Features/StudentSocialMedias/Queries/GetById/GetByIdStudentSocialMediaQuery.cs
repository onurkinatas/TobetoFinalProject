using Application.Features.StudentSocialMedias.Constants;
using Application.Features.StudentSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentSocialMedias.Constants.StudentSocialMediasOperationClaims;

namespace Application.Features.StudentSocialMedias.Queries.GetById;

public class GetByIdStudentSocialMediaQuery : IRequest<GetByIdStudentSocialMediaResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentSocialMediaQueryHandler : IRequestHandler<GetByIdStudentSocialMediaQuery, GetByIdStudentSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly StudentSocialMediaBusinessRules _studentSocialMediaBusinessRules;

        public GetByIdStudentSocialMediaQueryHandler(IMapper mapper, IStudentSocialMediaRepository studentSocialMediaRepository, StudentSocialMediaBusinessRules studentSocialMediaBusinessRules)
        {
            _mapper = mapper;
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _studentSocialMediaBusinessRules = studentSocialMediaBusinessRules;
        }

        public async Task<GetByIdStudentSocialMediaResponse> Handle(GetByIdStudentSocialMediaQuery request, CancellationToken cancellationToken)
        {
            StudentSocialMedia? studentSocialMedia = await _studentSocialMediaRepository.GetAsync(predicate: ssm => ssm.Id == request.Id, cancellationToken: cancellationToken);
            await _studentSocialMediaBusinessRules.StudentSocialMediaShouldExistWhenSelected(studentSocialMedia);

            GetByIdStudentSocialMediaResponse response = _mapper.Map<GetByIdStudentSocialMediaResponse>(studentSocialMedia);
            return response;
        }
    }
}