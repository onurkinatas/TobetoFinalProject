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

namespace Application.Features.StudentClasses.Commands.Update;

public class UpdateStudentClassCommand : IRequest<UpdatedStudentClassResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentClassesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentClasses";

    public class UpdateStudentClassCommandHandler : IRequestHandler<UpdateStudentClassCommand, UpdatedStudentClassResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly StudentClassBusinessRules _studentClassBusinessRules;

        public UpdateStudentClassCommandHandler(IMapper mapper, IStudentClassRepository studentClassRepository,
                                         StudentClassBusinessRules studentClassBusinessRules)
        {
            _mapper = mapper;
            _studentClassRepository = studentClassRepository;
            _studentClassBusinessRules = studentClassBusinessRules;
        }

        public async Task<UpdatedStudentClassResponse> Handle(UpdateStudentClassCommand request, CancellationToken cancellationToken)
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
            studentClass = _mapper.Map(request, studentClass);
            await _studentClassBusinessRules.StudentClassShouldNotExistsWhenInsert(studentClass.Name);
            await _studentClassRepository.UpdateAsync(studentClass!);

            UpdatedStudentClassResponse response = _mapper.Map<UpdatedStudentClassResponse>(studentClass);
            return response;
        }
    }
}