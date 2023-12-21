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

namespace Application.Features.LectureLikes.Commands.Create;

public class CreateLectureLikeCommand : IRequest<CreatedLectureLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public bool IsLiked { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureLikesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureLikes";

    public class CreateLectureLikeCommandHandler : IRequestHandler<CreateLectureLikeCommand, CreatedLectureLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

        public CreateLectureLikeCommandHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository,
                                         LectureLikeBusinessRules lectureLikeBusinessRules)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
        }

        public async Task<CreatedLectureLikeResponse> Handle(CreateLectureLikeCommand request, CancellationToken cancellationToken)
        {
            LectureLike lectureLike = _mapper.Map<LectureLike>(request);

            await _lectureLikeRepository.AddAsync(lectureLike);

            CreatedLectureLikeResponse response = _mapper.Map<CreatedLectureLikeResponse>(lectureLike);
            return response;
        }
    }
}