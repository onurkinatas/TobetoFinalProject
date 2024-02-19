using Application.Features.StudentEducations.Constants;
using Application.Features.StudentEducations.Constants;
using Application.Features.StudentEducations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentEducations.Constants.StudentEducationsOperationClaims;

namespace Application.Features.StudentEducations.Commands.Delete;

public class DeleteStudentEducationCommand : IRequest<DeletedStudentEducationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest

{
    public int? UserId { get; set; }

    public string CacheGroupKey => $"GetStudent{UserId}";
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentEducationsOperationClaims.Delete, "Student" };

    public class DeleteStudentEducationCommandHandler : IRequestHandler<DeleteStudentEducationCommand, DeletedStudentEducationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentEducationRepository _studentEducationRepository;
        private readonly StudentEducationBusinessRules _studentEducationBusinessRules;

        public DeleteStudentEducationCommandHandler(IMapper mapper, IStudentEducationRepository studentEducationRepository,
                                         StudentEducationBusinessRules studentEducationBusinessRules)
        {
            _mapper = mapper;
            _studentEducationRepository = studentEducationRepository;
            _studentEducationBusinessRules = studentEducationBusinessRules;
        }

        public async Task<DeletedStudentEducationResponse> Handle(DeleteStudentEducationCommand request, CancellationToken cancellationToken)
        {
            StudentEducation? studentEducation = await _studentEducationRepository.GetAsync(predicate: se => se.Id == request.Id, cancellationToken: cancellationToken);
            await _studentEducationBusinessRules.StudentEducationShouldExistWhenSelected(studentEducation);

            await _studentEducationRepository.DeleteAsync(studentEducation!);

            DeletedStudentEducationResponse response = _mapper.Map<DeletedStudentEducationResponse>(studentEducation);
            return response;
        }
    }
}