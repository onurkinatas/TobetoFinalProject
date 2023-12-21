using Application.Features.SocialMedias.Constants;
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

namespace Application.Features.SocialMedias.Commands.Delete;

public class DeleteSocialMediaCommand : IRequest<DeletedSocialMediaResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, SocialMediasOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSocialMedias";

    public class DeleteSocialMediaCommandHandler : IRequestHandler<DeleteSocialMediaCommand, DeletedSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

        public DeleteSocialMediaCommandHandler(IMapper mapper, ISocialMediaRepository socialMediaRepository,
                                         SocialMediaBusinessRules socialMediaBusinessRules)
        {
            _mapper = mapper;
            _socialMediaRepository = socialMediaRepository;
            _socialMediaBusinessRules = socialMediaBusinessRules;
        }

        public async Task<DeletedSocialMediaResponse> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
        {
            SocialMedia? socialMedia = await _socialMediaRepository.GetAsync(predicate: sm => sm.Id == request.Id, cancellationToken: cancellationToken);
            await _socialMediaBusinessRules.SocialMediaShouldExistWhenSelected(socialMedia);

            await _socialMediaRepository.DeleteAsync(socialMedia!);

            DeletedSocialMediaResponse response = _mapper.Map<DeletedSocialMediaResponse>(socialMedia);
            return response;
        }
    }
}