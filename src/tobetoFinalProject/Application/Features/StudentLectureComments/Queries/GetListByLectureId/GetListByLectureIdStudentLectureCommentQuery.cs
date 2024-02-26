
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentLectureComments.Queries.GetListByLectureId;
public class GetListByLectureIdStudentLectureCommentQuery : IRequest<GetListResponse<GetListByLectureIdStudentLectureCommentListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid LectureId { get; set; }
    public string[] Roles => new[] {"Student"};

    public class GetListByLectureIdStudentLectureCommentQueryHandler : IRequestHandler<GetListByLectureIdStudentLectureCommentQuery, GetListResponse<GetListByLectureIdStudentLectureCommentListItemDto>>
    {
        private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListByLectureIdStudentLectureCommentQueryHandler(IStudentLectureCommentRepository studentLectureCommentRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _studentLectureCommentRepository = studentLectureCommentRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListByLectureIdStudentLectureCommentListItemDto>> Handle(GetListByLectureIdStudentLectureCommentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();


            IPaginate<StudentLectureComment> studentLectureComments = await _studentLectureCommentRepository.GetListAsync(
                include:slc=>slc.Include(slc=>slc.Student)
                                  .ThenInclude(slc=>slc.User)
                                  .Include(slc=>slc.CommentSubComments)
                                  .ThenInclude(csc=>csc.Student)
                                  .ThenInclude(s=>s.User),
                predicate:slc=>slc.LectureId==request.LectureId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );
            
            GetListResponse<GetListByLectureIdStudentLectureCommentListItemDto> response = _mapper.Map<GetListResponse<GetListByLectureIdStudentLectureCommentListItemDto>>(studentLectureComments);
            response.Items.ToList().ForEach(item =>item.IsDeletable= getStudent.Id==item.StudentId?true:false);
            response.Items.ToList().ForEach(item => item.CommentSubComments.ToList().ForEach(item => item.IsDeletable = getStudent.Id == item.StudentId));
            return response;
        }
    }
}