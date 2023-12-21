using Application.Features.StudentClasses.Constants;
using Application.Features.StudentClasses.Constants;
using Application.Features.StudentClasses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentClasses.Constants.StudentClassesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentClasses.Commands.Delete;

public class DeleteStudentClassCommand : IRequest<DeletedStudentClassResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentClassesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentClasses";

    public class DeleteStudentClassCommandHandler : IRequestHandler<DeleteStudentClassCommand, DeletedStudentClassResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly StudentClassBusinessRules _studentClassBusinessRules;

        public DeleteStudentClassCommandHandler(IMapper mapper, IStudentClassRepository studentClassRepository,
                                         StudentClassBusinessRules studentClassBusinessRules)
        {
            _mapper = mapper;
            _studentClassRepository = studentClassRepository;
            _studentClassBusinessRules = studentClassBusinessRules;
        }

        public async Task<DeletedStudentClassResponse> Handle(DeleteStudentClassCommand request, CancellationToken cancellationToken)
        {
            StudentClass? studentClass = await _studentClassRepository.GetAsync(
                predicate: sc => sc.Id == request.Id,
                include: sc => sc.Include(sc => sc.ClassAnnouncements)
                    .Include(sc => sc.ClassLectures)
                    .Include(sc => sc.ClassExams)
                    .Include(sc => sc.ClassSurveys)
                    .Include(sc => sc.StudentClassStudentes),
                cancellationToken: cancellationToken);
            await _studentClassBusinessRules.StudentClassShouldExistWhenSelected(studentClass);

            await _studentClassRepository.DeleteAsync(studentClass!);

            DeletedStudentClassResponse response = _mapper.Map<DeletedStudentClassResponse>(studentClass);
            return response;
        }
    }
}