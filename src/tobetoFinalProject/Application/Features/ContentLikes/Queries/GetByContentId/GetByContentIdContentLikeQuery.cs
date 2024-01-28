using Application.Features.ContentLikes.Queries.GetById;
using Application.Features.ContentLikes.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContentLikes.Queries.GetByContentId;
public class GetByContentIdContentLikeQuery : IRequest<GetByContentIdContentLikeResponse>, ISecuredRequest
{
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetByContentIdContentLikeQueryHandler : IRequestHandler<GetByContentIdContentLikeQuery, GetByContentIdContentLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly ContentLikeBusinessRules _contentLikeBusinessRules;

        public GetByContentIdContentLikeQueryHandler(IMapper mapper, IContentLikeRepository contentLikeRepository, ContentLikeBusinessRules contentLikeBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _contentLikeRepository = contentLikeRepository;
            _contentLikeBusinessRules = contentLikeBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByContentIdContentLikeResponse> Handle(GetByContentIdContentLikeQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            ContentLike? contentLike = await _contentLikeRepository.GetAsync(
                predicate: ll => ll.ContentId == request.ContentId && ll.StudentId == getStudent.Id,
                cancellationToken: cancellationToken);
            await _contentLikeBusinessRules.ContentLikeShouldExistWhenSelected(contentLike);

            GetByContentIdContentLikeResponse response = _mapper.Map<GetByContentIdContentLikeResponse>(contentLike);
            return response;
        }
    }
}