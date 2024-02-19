using Application.Features.StudentSocialMedias.Constants;
using Application.Features.StudentSocialMedias.Constants;
using Application.Features.StudentSocialMedias.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentSocialMedias.Constants.StudentSocialMediasOperationClaims;

namespace Application.Features.StudentSocialMedias.Commands.Delete;

public class DeleteStudentSocialMediaCommand : IRequest<DeletedStudentSocialMediaResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentSocialMediasOperationClaims.Delete, "Student" };

    public int? UserId { get; set; }

    public string CacheGroupKey => $"GetStudent{UserId}";
    public bool BypassCache { get; }
    public string? CacheKey { get; }

    public class DeleteStudentSocialMediaCommandHandler : IRequestHandler<DeleteStudentSocialMediaCommand, DeletedStudentSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly StudentSocialMediaBusinessRules _studentSocialMediaBusinessRules;

        public DeleteStudentSocialMediaCommandHandler(IMapper mapper, IStudentSocialMediaRepository studentSocialMediaRepository,
                                         StudentSocialMediaBusinessRules studentSocialMediaBusinessRules)
        {
            _mapper = mapper;
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _studentSocialMediaBusinessRules = studentSocialMediaBusinessRules;
        }

        public async Task<DeletedStudentSocialMediaResponse> Handle(DeleteStudentSocialMediaCommand request, CancellationToken cancellationToken)
        {
            StudentSocialMedia? studentSocialMedia = await _studentSocialMediaRepository.GetAsync(predicate: ssm => ssm.Id == request.Id, cancellationToken: cancellationToken);
            await _studentSocialMediaBusinessRules.StudentSocialMediaShouldExistWhenSelected(studentSocialMedia);

            await _studentSocialMediaRepository.DeleteAsync(studentSocialMedia!);

            DeletedStudentSocialMediaResponse response = _mapper.Map<DeletedStudentSocialMediaResponse>(studentSocialMedia);
            return response;
        }
    }
}