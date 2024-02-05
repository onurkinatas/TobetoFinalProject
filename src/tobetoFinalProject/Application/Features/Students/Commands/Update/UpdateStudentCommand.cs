using Application.Features.Students.Constants;
using Application.Features.Students.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Students.Constants.StudentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.UsersService;
using Core.Security.Entities;
using Application.Services.ContextOperations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;

namespace Application.Features.Students.Commands.Update;

public class UpdateStudentCommand : IRequest<UpdatedStudentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? CityId { get; set; }
    public Guid? DistrictId { get; set; }
    public string? NationalIdentity { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? AddressDetail { get; set; }
    public string? Description { get; set; }
    public IFormFile? ProfilePhotoPathTemp { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public string? Country { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentsOperationClaims.Update ,"Student"};

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudents";

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, UpdatedStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserService _userService;
        private readonly IContextOperationService _contextOperationService;
        private readonly StudentBusinessRules _studentBusinessRules;
        private ImageServiceBase _imageBaseService;
        public UpdateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository,
                                         StudentBusinessRules studentBusinessRules, IUserService userService, IContextOperationService contextOperationService, ImageServiceBase imageBaseService)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _studentBusinessRules = studentBusinessRules;
            _userService = userService;
            _contextOperationService = contextOperationService;
            _imageBaseService = imageBaseService;
        }

        public async Task<UpdatedStudentResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            User? user = await _userService.GetAsync(predicate: u => u.Id ==getStudent.UserId, cancellationToken: cancellationToken);
            user.FirstName = request.FirstName==null?"":request.FirstName;
            user.LastName = request.LastName == null ? "" : request.LastName;

            request.ProfilePhotoPath=request.ProfilePhotoPathTemp == null ? "" : await _imageBaseService.UploadAsync(request.ProfilePhotoPathTemp);

            Student? student = await _studentRepository.GetAsync(
                predicate: s => s.Id == getStudent.Id,
                cancellationToken: cancellationToken);
            await _studentBusinessRules.StudentShouldExistWhenSelected(student);
            student = _mapper.Map(request, student);

            await _userService.UpdateAsync(user);
            await _studentRepository.UpdateAsync(student!);

            UpdatedStudentResponse response = _mapper.Map<UpdatedStudentResponse>(student);
            return response;
        }
    }
}