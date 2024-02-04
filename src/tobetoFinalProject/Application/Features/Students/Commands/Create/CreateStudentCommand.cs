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
using Application.Features.Auth.Rules;
using Core.Security.Entities;
using Core.Security.Hashing;
using Application.Services.UsersService;
using Application.Services.UserOperationClaims;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.OperationClaims;

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
        private readonly IUserService _userService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;


        public CreateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository,
                                                IUserService userService, IMediator mediator,
                                                     AuthBusinessRules authBusinessRules, IUserOperationClaimService userOperationClaimService,
                                                        UserOperationClaimBusinessRules userOperationClaimBusinessRules, IOperationClaimService operationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _mediator = mediator;
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _operationClaimService = operationClaimService;
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


            await _authBusinessRules.UserEmailShouldBeNotExists(userForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                userForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User newUser =
                new()
                {
                    Email = userForRegisterDto.Email,
                    FirstName = userForRegisterDto.FirstName,
                    LastName = userForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
            User createdUser = await _userService.AddAsync(newUser);

            OperationClaim studentOperationClaim = await _operationClaimService.GetAsync(predicate: oc => oc.Name == "Student");
            await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
                createdUser.Id,
                studentOperationClaim.Id
            );

            UserOperationClaim userOperationClaim = new(createdUser.Id,studentOperationClaim.Id) ;
            await _userOperationClaimService.AddAsync(userOperationClaim);

            Student student = _mapper.Map<Student>(request);
            student.UserId = createdUser.Id;
            await _studentRepository.AddAsync(student);

            CreatedStudentResponse response = _mapper.Map<CreatedStudentResponse>(student);
            return response;
        }
    }
}