using Application.Features.StudentLectureComments.Constants;
using Application.Features.StudentLectureComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentLectureComments.Constants.StudentLectureCommentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.ContextOperations;

namespace Application.Features.StudentLectureComments.Queries.GetById;

public class GetByIdStudentLectureCommentQuery : IRequest<GetByIdStudentLectureCommentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read ,"Student"};

    public class GetByIdStudentLectureCommentQueryHandler : IRequestHandler<GetByIdStudentLectureCommentQuery, GetByIdStudentLectureCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly StudentLectureCommentBusinessRules _studentLectureCommentBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public GetByIdStudentLectureCommentQueryHandler(IMapper mapper, IStudentLectureCommentRepository studentLectureCommentRepository, StudentLectureCommentBusinessRules studentLectureCommentBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _studentLectureCommentBusinessRules = studentLectureCommentBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByIdStudentLectureCommentResponse> Handle(GetByIdStudentLectureCommentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();

            StudentLectureComment? studentLectureComment = await _studentLectureCommentRepository.GetAsync(
                include: slc => slc.Include(slc => slc.Student)
                                  .ThenInclude(slc => slc.User)
                                  .Include(slc => slc.CommentSubComments)
                                  .ThenInclude(csc => csc.Student)
                                  .ThenInclude(s => s.User),
                predicate: slc => slc.Id == request.Id, 
               cancellationToken: cancellationToken);
            await _studentLectureCommentBusinessRules.StudentLectureCommentShouldExistWhenSelected(studentLectureComment);

            GetByIdStudentLectureCommentResponse response = _mapper.Map<GetByIdStudentLectureCommentResponse>(studentLectureComment);
            response.CommentSubComments.ToList().ForEach(item => item.IsDeletable = getStudent.Id == item.StudentId);
            return response;
        }
    }
}