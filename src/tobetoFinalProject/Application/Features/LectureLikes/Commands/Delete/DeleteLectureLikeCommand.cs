using Application.Features.LectureLikes.Constants;
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

namespace Application.Features.LectureLikes.Commands.Delete;

public class DeleteLectureLikeCommand : IRequest<DeletedLectureLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureLikesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureLikes";

    public class DeleteLectureLikeCommandHandler : IRequestHandler<DeleteLectureLikeCommand, DeletedLectureLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

        public DeleteLectureLikeCommandHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository,
                                         LectureLikeBusinessRules lectureLikeBusinessRules)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
        }

        public async Task<DeletedLectureLikeResponse> Handle(DeleteLectureLikeCommand request, CancellationToken cancellationToken)
        {
            LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(predicate: ll => ll.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureLikeBusinessRules.LectureLikeShouldExistWhenSelected(lectureLike);

            await _lectureLikeRepository.DeleteAsync(lectureLike!);

            DeletedLectureLikeResponse response = _mapper.Map<DeletedLectureLikeResponse>(lectureLike);
            return response;
        }
    }
}