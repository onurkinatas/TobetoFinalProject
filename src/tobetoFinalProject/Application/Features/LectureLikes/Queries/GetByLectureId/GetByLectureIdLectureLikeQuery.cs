using Application.Features.LectureLikes.Queries.GetById;
using Application.Features.LectureLikes.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.LectureLikes.Queries.GetByLectureId;
public class GetByLectureIdLectureLikeQuery : IRequest<GetByLectureIdLectureLikeResponse>, ISecuredRequest
{
    public Guid LectureId { get; set; }

    public string[] Roles => new[] {"Admin","Student"};

    public class GetByLectureIdLectureLikeQueryHandler : IRequestHandler<GetByLectureIdLectureLikeQuery, GetByLectureIdLectureLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

        public GetByLectureIdLectureLikeQueryHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository, LectureLikeBusinessRules lectureLikeBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByLectureIdLectureLikeResponse> Handle(GetByLectureIdLectureLikeQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(
                predicate: ll => ll.LectureId == request.LectureId && ll.StudentId== getStudent.Id, 
                cancellationToken: cancellationToken);

            GetByLectureIdLectureLikeResponse response = _mapper.Map<GetByLectureIdLectureLikeResponse>(lectureLike);
            return response;
        }
    }
}