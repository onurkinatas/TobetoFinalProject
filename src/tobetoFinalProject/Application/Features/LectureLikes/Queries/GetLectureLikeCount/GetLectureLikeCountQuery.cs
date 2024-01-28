using Application.Features.LectureLikes.Queries.GetByLectureId;
using Application.Features.LectureLikes.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureLikes.Queries.GetLectureLikeCount;
public class GetLectureLikeCountQuery : IRequest<GetLectureLikeCountQueryResponse>, ISecuredRequest
{
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetLectureLikeCountQueryHandler : IRequestHandler<GetLectureLikeCountQuery, GetLectureLikeCountQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

        public GetLectureLikeCountQueryHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository, LectureLikeBusinessRules lectureLikeBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetLectureLikeCountQueryResponse> Handle(GetLectureLikeCountQuery request, CancellationToken cancellationToken)
        {
            int lectureLikeCount = _lectureLikeRepository.GetLectureLikeCount(cl => cl.LectureId == request.LectureId && cl.IsLiked == true);

            GetLectureLikeCountQueryResponse getLectureLikeCountQueryResponse = new GetLectureLikeCountQueryResponse();
            getLectureLikeCountQueryResponse.Count = lectureLikeCount;
            return getLectureLikeCountQueryResponse;
        }
    }
}
