using Application.Features.StudentLectureComments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentLectureComments.Constants.StudentLectureCommentsOperationClaims;

namespace Application.Features.StudentLectureComments.Queries.GetList;

public class GetListStudentLectureCommentQuery : IRequest<GetListResponse<GetListStudentLectureCommentListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListStudentLectureCommentQueryHandler : IRequestHandler<GetListStudentLectureCommentQuery, GetListResponse<GetListStudentLectureCommentListItemDto>>
    {
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly IMapper _mapper;

        public GetListStudentLectureCommentQueryHandler(IStudentLectureCommentRepository studentLectureCommentRepository, IMapper mapper)
        {
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentLectureCommentListItemDto>> Handle(GetListStudentLectureCommentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentLectureComment> studentLectureComments = await _studentLectureCommentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentLectureCommentListItemDto> response = _mapper.Map<GetListResponse<GetListStudentLectureCommentListItemDto>>(studentLectureComments);
            return response;
        }
    }
}