using Application.Features.StudentClasses.Constants;
using Application.Features.StudentClasses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentClasses.Constants.StudentClassesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentClasses.Queries.GetById;

public class GetByIdStudentClassQuery : IRequest<GetByIdStudentClassResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentClassQueryHandler : IRequestHandler<GetByIdStudentClassQuery, GetByIdStudentClassResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly StudentClassBusinessRules _studentClassBusinessRules;

        public GetByIdStudentClassQueryHandler(IMapper mapper, IStudentClassRepository studentClassRepository, StudentClassBusinessRules studentClassBusinessRules)
        {
            _mapper = mapper;
            _studentClassRepository = studentClassRepository;
            _studentClassBusinessRules = studentClassBusinessRules;
        }

        public async Task<GetByIdStudentClassResponse> Handle(GetByIdStudentClassQuery request, CancellationToken cancellationToken)
        {
            StudentClass? studentClass = await _studentClassRepository.GetAsync(
                predicate: sc => sc.Id == request.Id,
                include: sc => sc.Include(sc => sc.ClassAnnouncements)
               .ThenInclude(ca => ca.Announcement)
               .Include(sc => sc.ClassLectures)
               .ThenInclude(ca => ca.Lecture)
               .Include(sc => sc.ClassExams)
               .ThenInclude(ca => ca.Exam)
               .Include(sc => sc.StudentClassStudentes)
               .ThenInclude(ca => ca.Student)
               .ThenInclude(ss => ss.City)
               .Include(sc => sc.StudentClassStudentes)
               .ThenInclude(ca => ca.Student)
               .ThenInclude(ss => ss.District)
               .Include(sc => sc.ClassSurveys)
               .ThenInclude(ca => ca.Survey),
                cancellationToken: cancellationToken);
            await _studentClassBusinessRules.StudentClassShouldExistWhenSelected(studentClass);

            GetByIdStudentClassResponse response = _mapper.Map<GetByIdStudentClassResponse>(studentClass);
            return response;
        }
    }
}