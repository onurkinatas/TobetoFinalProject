using Application.Features.CourseContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Features.CourseContents.Commands.Create;

public class CreateCourseContentCommand : IRequest<CreatedCourseContentResponse>, ICacheRemoverRequest
{
    public Guid CourseId { get; set; }
    public Guid ContentId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetCourseContents";

    public class CreateCourseContentCommandHandler : IRequestHandler<CreateCourseContentCommand, CreatedCourseContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly CourseContentBusinessRules _courseContentBusinessRules;

        public CreateCourseContentCommandHandler(IMapper mapper, ICourseContentRepository courseContentRepository,
                                         CourseContentBusinessRules courseContentBusinessRules)
        {
            _mapper = mapper;
            _courseContentRepository = courseContentRepository;
            _courseContentBusinessRules = courseContentBusinessRules;
        }

        public async Task<CreatedCourseContentResponse> Handle(CreateCourseContentCommand request, CancellationToken cancellationToken)
        {
            CourseContent courseContent = _mapper.Map<CourseContent>(request);
            await _courseContentBusinessRules.CourseContentShouldNotExistsWhenInsert( courseContent.ContentId,courseContent.CourseId);
            await _courseContentRepository.AddAsync(courseContent);

            CreatedCourseContentResponse response = _mapper.Map<CreatedCourseContentResponse>(courseContent);
            return response;
        }
    }
}