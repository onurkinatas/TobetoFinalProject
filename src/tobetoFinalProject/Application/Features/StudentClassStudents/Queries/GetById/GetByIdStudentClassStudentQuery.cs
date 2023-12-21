using Application.Features.StudentClassStudents.Constants;
using Application.Features.StudentClassStudents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentClassStudents.Constants.StudentClassStudentsOperationClaims;

namespace Application.Features.StudentClassStudents.Queries.GetById;

public class GetByIdStudentClassStudentQuery : IRequest<GetByIdStudentClassStudentResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentClassStudentQueryHandler : IRequestHandler<GetByIdStudentClassStudentQuery, GetByIdStudentClassStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassStudentRepository _studentClassStudentRepository;
        private readonly StudentClassStudentBusinessRules _studentClassStudentBusinessRules;

        public GetByIdStudentClassStudentQueryHandler(IMapper mapper, IStudentClassStudentRepository studentClassStudentRepository, StudentClassStudentBusinessRules studentClassStudentBusinessRules)
        {
            _mapper = mapper;
            _studentClassStudentRepository = studentClassStudentRepository;
            _studentClassStudentBusinessRules = studentClassStudentBusinessRules;
        }

        public async Task<GetByIdStudentClassStudentResponse> Handle(GetByIdStudentClassStudentQuery request, CancellationToken cancellationToken)
        {
            StudentClassStudent? studentClassStudent = await _studentClassStudentRepository.GetAsync(predicate: scs => scs.Id == request.Id, cancellationToken: cancellationToken);
            await _studentClassStudentBusinessRules.StudentClassStudentShouldExistWhenSelected(studentClassStudent);

            GetByIdStudentClassStudentResponse response = _mapper.Map<GetByIdStudentClassStudentResponse>(studentClassStudent);
            return response;
        }
    }
}