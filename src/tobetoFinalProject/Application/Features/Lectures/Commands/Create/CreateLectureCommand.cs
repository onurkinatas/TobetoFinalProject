using Application.Features.Lectures.Constants;
using Application.Features.Lectures.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Lectures.Constants.LecturesOperationClaims;
using Application.Features.Exams.Rules;

namespace Application.Features.Lectures.Commands.Create;

public class CreateLectureCommand : IRequest<CreatedLectureResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public double EstimatedDuration { get; set; }
    public Guid ManufacturerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<LectureCourse>? LectureCourses { get; set; }

    public string[] Roles => new[] { Admin, Write, LecturesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectures";

    public class CreateLectureCommandHandler : IRequestHandler<CreateLectureCommand, CreatedLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureRepository _lectureRepository;
        private readonly LectureBusinessRules _lectureBusinessRules;

        public CreateLectureCommandHandler(IMapper mapper, ILectureRepository lectureRepository,
                                         LectureBusinessRules lectureBusinessRules)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _lectureBusinessRules = lectureBusinessRules;
        }

        public async Task<CreatedLectureResponse> Handle(CreateLectureCommand request, CancellationToken cancellationToken)
        {
            Lecture lecture = _mapper.Map<Lecture>(request);

            await _lectureBusinessRules.LectureShouldNotExistsWhenInsert(lecture.Name);

            await _lectureRepository.AddAsync(lecture);

            CreatedLectureResponse response = _mapper.Map<CreatedLectureResponse>(lecture);
            return response;
        }
    }
}