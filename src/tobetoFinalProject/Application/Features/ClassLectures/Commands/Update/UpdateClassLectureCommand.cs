using Application.Features.ClassLectures.Constants;
using Application.Features.ClassLectures.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ClassLectures.Constants.ClassLecturesOperationClaims;

namespace Application.Features.ClassLectures.Commands.Update;

public class UpdateClassLectureCommand : IRequest<UpdatedClassLectureResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassLecturesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassLectures";

    public class UpdateClassLectureCommandHandler : IRequestHandler<UpdateClassLectureCommand, UpdatedClassLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly ClassLectureBusinessRules _classLectureBusinessRules;

        public UpdateClassLectureCommandHandler(IMapper mapper, IClassLectureRepository classLectureRepository,
                                         ClassLectureBusinessRules classLectureBusinessRules)
        {
            _mapper = mapper;
            _classLectureRepository = classLectureRepository;
            _classLectureBusinessRules = classLectureBusinessRules;
        }

        public async Task<UpdatedClassLectureResponse> Handle(UpdateClassLectureCommand request, CancellationToken cancellationToken)
        {
            ClassLecture? classLecture = await _classLectureRepository.GetAsync(predicate: cl => cl.Id == request.Id, cancellationToken: cancellationToken);
            await _classLectureBusinessRules.ClassLectureShouldExistWhenSelected(classLecture);
            classLecture = _mapper.Map(request, classLecture);

            await _classLectureRepository.UpdateAsync(classLecture!);

            UpdatedClassLectureResponse response = _mapper.Map<UpdatedClassLectureResponse>(classLecture);
            return response;
        }
    }
}