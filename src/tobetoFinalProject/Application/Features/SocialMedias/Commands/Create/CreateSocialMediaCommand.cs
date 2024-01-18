using Application.Features.SocialMedias.Constants;
using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SocialMedias.Constants.SocialMediasOperationClaims;

namespace Application.Features.SocialMedias.Commands.Create;

public class CreateSocialMediaCommand : IRequest<CreatedSocialMediaResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, SocialMediasOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSocialMedias";

    public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, CreatedSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

        public CreateSocialMediaCommandHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository,
                                         SocialMediaBusinessRules socialMediaBusinessRules)
        {
            _mapper = mapper;
            _socialMediaRepository = socialMediaRepository;
            _socialMediaBusinessRules = socialMediaBusinessRules;
        }

        public async Task<CreatedSocialMediaResponse> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            SocialMedia socialMedia = _mapper.Map<SocialMedia>(request);

            await _socialMediaBusinessRules.SocialMediaNameShouldNotExist(socialMedia, cancellationToken);

            await _socialMediaRepository.AddAsync(socialMedia);

            CreatedSocialMediaResponse response = _mapper.Map<CreatedSocialMediaResponse>(socialMedia);
            return response;
        }
    }
}