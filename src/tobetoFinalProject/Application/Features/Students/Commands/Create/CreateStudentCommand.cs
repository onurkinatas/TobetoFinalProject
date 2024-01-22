using Application.Features.Students.Constants;
using Application.Features.Students.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Students.Constants.StudentsOperationClaims;
using Application.Features.Users.Commands.Create;
using Core.Application.Pipelines.Authorization;
using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.Auth.Commands.Register;
using Core.Application.Dtos;

namespace Application.Features.Students.Commands.Create;

public class CreateStudentCommand : IRequest<CreatedStudentResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string[] Roles;

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudents";

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreatedStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IMediator _mediator;

        public CreateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository, IMediator mediator)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _mediator = mediator;
        }

        public async Task<CreatedStudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
       

            UserForRegisterDto userForRegisterDto = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
            };

            RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto };

            RegisteredResponse result = await _mediator.Send(registerCommand);

            CreateUserOperationClaimCommand createUserOperationClaimCommand = new()
            {
                UserId = result.UserId,
                OperationClaimId = 296,
            };
            await _mediator.Send(createUserOperationClaimCommand);

            Student student = _mapper.Map<Student>(request);
            student.UserId = result.UserId;
            await _studentRepository.AddAsync(student);

            CreatedStudentResponse response = _mapper.Map<CreatedStudentResponse>(student);
            return response;
        }
    }
}