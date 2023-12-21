using Application.Features.Contents.Constants;
using Application.Features.Contents.Constants;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Contents.Constants.ContentsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Contents.Commands.Delete;

public class DeleteContentCommand : IRequest<DeletedContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContents";

    public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, DeletedContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;
        private readonly ContentBusinessRules _contentBusinessRules;

        public DeleteContentCommandHandler(IMapper mapper, IContentRepository contentRepository,
                                         ContentBusinessRules contentBusinessRules)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
            _contentBusinessRules = contentBusinessRules;
        }

        public async Task<DeletedContentResponse> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
        {
            Content? content = await _contentRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                include: c => c.Include(c => c.CourseContents)
                .Include(c => c.ContentInstructors),
                cancellationToken: cancellationToken);

            await _contentBusinessRules.ContentShouldExistWhenSelected(content);

            await _contentRepository.DeleteAsync(content!);

            DeletedContentResponse response = _mapper.Map<DeletedContentResponse>(content);
            return response;
        }
    }
}