using Application.Features.StudentLectureComments.Constants;
using Application.Features.StudentLectureComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentLectureComments.Constants.StudentLectureCommentsOperationClaims;

namespace Application.Features.StudentLectureComments.Queries.GetById;

public class GetByIdStudentLectureCommentQuery : IRequest<GetByIdStudentLectureCommentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentLectureCommentQueryHandler : IRequestHandler<GetByIdStudentLectureCommentQuery, GetByIdStudentLectureCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly StudentLectureCommentBusinessRules _studentLectureCommentBusinessRules;

        public GetByIdStudentLectureCommentQueryHandler(IMapper mapper, IStudentLectureCommentRepository studentLectureCommentRepository, StudentLectureCommentBusinessRules studentLectureCommentBusinessRules)
        {
            _mapper = mapper;
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _studentLectureCommentBusinessRules = studentLectureCommentBusinessRules;
        }

        public async Task<GetByIdStudentLectureCommentResponse> Handle(GetByIdStudentLectureCommentQuery request, CancellationToken cancellationToken)
        {
            StudentLectureComment? studentLectureComment = await _studentLectureCommentRepository.GetAsync(predicate: slc => slc.Id == request.Id, cancellationToken: cancellationToken);
            await _studentLectureCommentBusinessRules.StudentLectureCommentShouldExistWhenSelected(studentLectureComment);

            GetByIdStudentLectureCommentResponse response = _mapper.Map<GetByIdStudentLectureCommentResponse>(studentLectureComment);
            return response;
        }
    }
}