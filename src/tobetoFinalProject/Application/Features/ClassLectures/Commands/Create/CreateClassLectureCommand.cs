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

namespace Application.Features.ClassLectures.Commands.Create;

public class CreateClassLectureCommand : IRequest<CreatedClassLectureResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid LectureId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassLecturesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassLectures";

    public class CreateClassLectureCommandHandler : IRequestHandler<CreateClassLectureCommand, CreatedClassLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly ClassLectureBusinessRules _classLectureBusinessRules;

        public CreateClassLectureCommandHandler(IMapper mapper, IClassLectureRepository classLectureRepository,
                                         ClassLectureBusinessRules classLectureBusinessRules)
        {
            _mapper = mapper;
            _classLectureRepository = classLectureRepository;
            _classLectureBusinessRules = classLectureBusinessRules;
        }

        public async Task<CreatedClassLectureResponse> Handle(CreateClassLectureCommand request, CancellationToken cancellationToken)
        {
            ClassLecture classLecture = _mapper.Map<ClassLecture>(request);

            await _classLectureRepository.AddAsync(classLecture);

            CreatedClassLectureResponse response = _mapper.Map<CreatedClassLectureResponse>(classLecture);
            return response;
        }
    }
}