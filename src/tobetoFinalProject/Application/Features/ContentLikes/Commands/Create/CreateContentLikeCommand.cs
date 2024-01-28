using Application.Features.ContentLikes.Constants;
using Application.Features.ContentLikes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentLikes.Constants.ContentLikesOperationClaims;
using Application.Services.ContextOperations;

namespace Application.Features.ContentLikes.Commands.Create;

public class CreateContentLikeCommand : IRequest<CreatedContentLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public bool? IsLiked { get; set; }
    public Guid? StudentId { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentLikesOperationClaims.Create,"Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentLikes";

    public class CreateContentLikeCommandHandler : IRequestHandler<CreateContentLikeCommand, CreatedContentLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public CreateContentLikeCommandHandler(IMapper mapper, IContentLikeRepository contentLikeRepository,
                                         ContentLikeBusinessRules contentLikeBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedContentLikeResponse> Handle(CreateContentLikeCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            ContentLike contentLike = _mapper.Map<ContentLike>(request);
            contentLike.StudentId = getStudent.Id;

            ContentLike? existContentLike = await _contentLikeRepository.GetAsync(predicate: cl => cl.StudentId == getStudent.Id && cl.ContentId == contentLike.ContentId, cancellationToken: cancellationToken);
            if (existContentLike is not null)
            {
                existContentLike.IsLiked = !existContentLike.IsLiked;
                await _contentLikeRepository.UpdateAsync(existContentLike);
            }
            if (existContentLike is null)
            {
                contentLike.IsLiked = true;
                await _contentLikeRepository.AddAsync(contentLike);
            }

            CreatedContentLikeResponse response = _mapper.Map<CreatedContentLikeResponse>(contentLike);
            return response;
        }
    }
}