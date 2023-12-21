using Application.Features.Lectures.Constants;
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

namespace Application.Features.Lectures.Commands.Delete;

public class DeleteLectureCommand : IRequest<DeletedLectureResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LecturesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectures";

    public class DeleteLectureCommandHandler : IRequestHandler<DeleteLectureCommand, DeletedLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureRepository _lectureRepository;
        private readonly LectureBusinessRules _lectureBusinessRules;

        public DeleteLectureCommandHandler(IMapper mapper, ILectureRepository lectureRepository,
                                         LectureBusinessRules lectureBusinessRules)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _lectureBusinessRules = lectureBusinessRules;
        }

        public async Task<DeletedLectureResponse> Handle(DeleteLectureCommand request, CancellationToken cancellationToken)
        {
            Lecture? lecture = await _lectureRepository.GetAsync(
                predicate: l => l.Id == request.Id,
                include: m => m.Include(m => m.ClassLectures)
                    .Include(m => m.LectureCourses),
                cancellationToken: cancellationToken);

            await _lectureBusinessRules.LectureShouldExistWhenSelected(lecture);

            await _lectureRepository.DeleteAsync(lecture!);

            DeletedLectureResponse response = _mapper.Map<DeletedLectureResponse>(lecture);
            return response;
        }
    }
}