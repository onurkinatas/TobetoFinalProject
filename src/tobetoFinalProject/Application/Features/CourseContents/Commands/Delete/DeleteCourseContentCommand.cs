using Application.Features.CourseContents.Constants;
using Application.Features.CourseContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Features.CourseContents.Commands.Delete;

public class DeleteCourseContentCommand : IRequest<DeletedCourseContentResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetCourseContents";

    public class DeleteCourseContentCommandHandler : IRequestHandler<DeleteCourseContentCommand, DeletedCourseContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly CourseContentBusinessRules _courseContentBusinessRules;

        public DeleteCourseContentCommandHandler(IMapper mapper, ICourseContentRepository courseContentRepository,
                                         CourseContentBusinessRules courseContentBusinessRules)
        {
            _mapper = mapper;
            _courseContentRepository = courseContentRepository;
            _courseContentBusinessRules = courseContentBusinessRules;
        }

        public async Task<DeletedCourseContentResponse> Handle(DeleteCourseContentCommand request, CancellationToken cancellationToken)
        {
            CourseContent? courseContent = await _courseContentRepository.GetAsync(predicate: cc => cc.Id == request.Id, cancellationToken: cancellationToken);
            await _courseContentBusinessRules.CourseContentShouldExistWhenSelected(courseContent);

            await _courseContentRepository.DeleteAsync(courseContent!);

            DeletedCourseContentResponse response = _mapper.Map<DeletedCourseContentResponse>(courseContent);
            return response;
        }
    }
}