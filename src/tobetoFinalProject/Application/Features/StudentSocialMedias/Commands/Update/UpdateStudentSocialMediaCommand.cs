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

namespace Application.Features.StudentSocialMedias.Commands.Update;

public class UpdateStudentSocialMediaCommand : IRequest<UpdatedStudentSocialMediaResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SocialMediaId { get; set; }
    public string MediaAccountUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentSocialMediasOperationClaims.Update, "Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentSocialMedias";

    public class UpdateStudentSocialMediaCommandHandler : IRequestHandler<UpdateStudentSocialMediaCommand, UpdatedStudentSocialMediaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly StudentSocialMediaBusinessRules _studentSocialMediaBusinessRules;

        public UpdateStudentSocialMediaCommandHandler(IMapper mapper, IStudentSocialMediaRepository studentSocialMediaRepository,
                                         StudentSocialMediaBusinessRules studentSocialMediaBusinessRules)
        {
            _mapper = mapper;
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _studentSocialMediaBusinessRules = studentSocialMediaBusinessRules;
        }

        public async Task<UpdatedStudentSocialMediaResponse> Handle(UpdateStudentSocialMediaCommand request, CancellationToken cancellationToken)
        {
            StudentSocialMedia? studentSocialMedia = await _studentSocialMediaRepository.GetAsync(predicate: ssm => ssm.Id == request.Id, cancellationToken: cancellationToken);
            await _studentSocialMediaBusinessRules.StudentSocialMediaShouldExistWhenSelected(studentSocialMedia);
            studentSocialMedia = _mapper.Map(request, studentSocialMedia);
            await _studentSocialMediaBusinessRules.StudentSocialMediaShouldNotExistsWhenUpdate(studentSocialMedia);
            await _studentSocialMediaRepository.UpdateAsync(studentSocialMedia!);

            UpdatedStudentSocialMediaResponse response = _mapper.Map<UpdatedStudentSocialMediaResponse>(studentSocialMedia);
            return response;
        }
    }
}