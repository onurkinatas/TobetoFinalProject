using Application.Features.LectureLikes.Constants;
using Application.Features.LectureLikes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LectureLikes.Constants.LectureLikesOperationClaims;

namespace Application.Features.LectureLikes.Queries.GetById;

public class GetByIdLectureLikeQuery : IRequest<GetByIdLectureLikeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLectureLikeQueryHandler : IRequestHandler<GetByIdLectureLikeQuery, GetByIdLectureLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

        public GetByIdLectureLikeQueryHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository, LectureLikeBusinessRules lectureLikeBusinessRules)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
        }

        public async Task<GetByIdLectureLikeResponse> Handle(GetByIdLectureLikeQuery request, CancellationToken cancellationToken)
        {
            LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(predicate: ll => ll.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureLikeBusinessRules.LectureLikeShouldExistWhenSelected(lectureLike);

            GetByIdLectureLikeResponse response = _mapper.Map<GetByIdLectureLikeResponse>(lectureLike);
            return response;
        }
    }
}