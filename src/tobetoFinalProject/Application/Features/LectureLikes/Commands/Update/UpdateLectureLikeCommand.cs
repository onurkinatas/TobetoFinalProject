using Application.Features.LectureLikes.Constants;
using Application.Features.LectureLikes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LectureLikes.Constants.LectureLikesOperationClaims;

namespace Application.Features.LectureLikes.Commands.Update;

public class UpdateLectureLikeCommand : IRequest<UpdatedLectureLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureLikesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureLikes";

    public class UpdateLectureLikeCommandHandler : IRequestHandler<UpdateLectureLikeCommand, UpdatedLectureLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

        public UpdateLectureLikeCommandHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository,
                                         LectureLikeBusinessRules lectureLikeBusinessRules)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
        }

        public async Task<UpdatedLectureLikeResponse> Handle(UpdateLectureLikeCommand request, CancellationToken cancellationToken)
        {
            LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(predicate: ll => ll.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureLikeBusinessRules.LectureLikeShouldExistWhenSelected(lectureLike);
            lectureLike = _mapper.Map(request, lectureLike);

            await _lectureLikeRepository.UpdateAsync(lectureLike!);

            UpdatedLectureLikeResponse response = _mapper.Map<UpdatedLectureLikeResponse>(lectureLike);
            return response;
        }
    }
}