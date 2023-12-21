using Application.Features.ClassLectures.Constants;
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

namespace Application.Features.ClassLectures.Commands.Delete;

public class DeleteClassLectureCommand : IRequest<DeletedClassLectureResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassLecturesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassLectures";

    public class DeleteClassLectureCommandHandler : IRequestHandler<DeleteClassLectureCommand, DeletedClassLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly ClassLectureBusinessRules _classLectureBusinessRules;

        public DeleteClassLectureCommandHandler(IMapper mapper, IClassLectureRepository classLectureRepository,
                                         ClassLectureBusinessRules classLectureBusinessRules)
        {
            _mapper = mapper;
            _classLectureRepository = classLectureRepository;
            _classLectureBusinessRules = classLectureBusinessRules;
        }

        public async Task<DeletedClassLectureResponse> Handle(DeleteClassLectureCommand request, CancellationToken cancellationToken)
        {
            ClassLecture? classLecture = await _classLectureRepository.GetAsync(predicate: cl => cl.Id == request.Id, cancellationToken: cancellationToken);
            await _classLectureBusinessRules.ClassLectureShouldExistWhenSelected(classLecture);

            await _classLectureRepository.DeleteAsync(classLecture!);

            DeletedClassLectureResponse response = _mapper.Map<DeletedClassLectureResponse>(classLecture);
            return response;
        }
    }
}