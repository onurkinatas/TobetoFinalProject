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
using Microsoft.EntityFrameworkCore;
using Application.Features.Exams.Rules;

namespace Application.Features.Lectures.Commands.Update;

public class UpdateLectureCommand : IRequest<UpdatedLectureResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public double EstimatedDuration { get; set; }
    public Guid ManufacturerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<LectureCourse> LectureCourses { get; set; }

    public string[] Roles => new[] { Admin, Write, LecturesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectures";

    public class UpdateLectureCommandHandler : IRequestHandler<UpdateLectureCommand, UpdatedLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureRepository _lectureRepository;
        private readonly LectureBusinessRules _lectureBusinessRules;

        public UpdateLectureCommandHandler(IMapper mapper, ILectureRepository lectureRepository,
                                         LectureBusinessRules lectureBusinessRules)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _lectureBusinessRules = lectureBusinessRules;
        }

        public async Task<UpdatedLectureResponse> Handle(UpdateLectureCommand request, CancellationToken cancellationToken)
        {
            Lecture? lecture = await _lectureRepository.GetAsync(
                predicate: l => l.Id == request.Id,
                include: m => m.Include(m => m.ClassLectures)
                    .Include(m => m.LectureCourses),
                cancellationToken: cancellationToken);

            await _lectureBusinessRules.LectureShouldExistWhenSelected(lecture);
            lecture = _mapper.Map(request, lecture);

            await _lectureBusinessRules.LectureNameShouldNotExist(lecture, cancellationToken);

            await _lectureRepository.UpdateAsync(lecture!);

            UpdatedLectureResponse response = _mapper.Map<UpdatedLectureResponse>(lecture);
            return response;
        }
    }
}