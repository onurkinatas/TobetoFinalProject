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
using Application.Services.ContextOperations;
using Application.Features.Lectures.Commands.Update;
using Application.Features.LectureLikes.Commands.Update;

namespace Application.Features.LectureLikes.Commands.Create;

public class CreateLectureLikeCommand : IRequest<CreatedLectureLikeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public bool? IsLiked { get; set; }
    public Guid? StudentId { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureLikesOperationClaims.Create ,"Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureLikes";

    public class CreateLectureLikeCommandHandler : IRequestHandler<CreateLectureLikeCommand, CreatedLectureLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureLikeRepository _lectureLikeRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;
        private readonly IMediator _mediator;
        public CreateLectureLikeCommandHandler(IMapper mapper, ILectureLikeRepository lectureLikeRepository,
                                         LectureLikeBusinessRules lectureLikeBusinessRules, IContextOperationService contextOperationService, IMediator mediator)
        {
            _mapper = mapper;
            _lectureLikeRepository = lectureLikeRepository;
            _lectureLikeBusinessRules = lectureLikeBusinessRules;
            _contextOperationService = contextOperationService;
            _mediator = mediator;
        }

        public async Task<CreatedLectureLikeResponse> Handle(CreateLectureLikeCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            LectureLike lectureLike = _mapper.Map<LectureLike>(request);
            lectureLike.StudentId=getStudent.Id;

            LectureLike? doesExistLectureLike = await _lectureLikeRepository.GetAsync(predicate:ll=>ll.StudentId==getStudent.Id&&ll.LectureId==lectureLike.LectureId, cancellationToken:cancellationToken);
            if(doesExistLectureLike is not null)
            {
                doesExistLectureLike.IsLiked = !doesExistLectureLike.IsLiked;
                await _lectureLikeRepository.UpdateAsync(doesExistLectureLike);
                
            }
            if(doesExistLectureLike is null)
            {
                lectureLike.IsLiked = true;
                await _lectureLikeRepository.AddAsync(lectureLike);
            }

            CreatedLectureLikeResponse response = _mapper.Map<CreatedLectureLikeResponse>(lectureLike);
            return response;
        }
    }
}