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

namespace Application.Features.SocialMedias.Commands.Update;

public class UpdateSocialMediaCommand : IRequest<UpdatedSocialMediaResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, SocialMediasOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSocialMedias";

    public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, UpdatedSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

        public UpdateSocialMediaCommandHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository,
                                         SocialMediaBusinessRules socialMediaBusinessRules)
        {
            _mapper = mapper;
            _socialMediaRepository = socialMediaRepository;
            _socialMediaBusinessRules = socialMediaBusinessRules;
        }

        public async Task<UpdatedSocialMediaResponse> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            SocialMedia? socialMedia = await _socialMediaRepository.GetAsync(predicate: sm => sm.Id == request.Id, cancellationToken: cancellationToken);
            await _socialMediaBusinessRules.SocialMediaShouldExistWhenSelected(socialMedia);
            socialMedia = _mapper.Map(request, socialMedia);

            await _socialMediaBusinessRules.SocialMediaShouldNotExistsWhenUpdate(socialMedia.Name);

            await _socialMediaRepository.UpdateAsync(socialMedia!);

            UpdatedSocialMediaResponse response = _mapper.Map<UpdatedSocialMediaResponse>(socialMedia);
            return response;
        }
    }
}