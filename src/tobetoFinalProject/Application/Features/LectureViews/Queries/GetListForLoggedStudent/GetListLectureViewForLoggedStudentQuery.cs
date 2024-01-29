using Application.Features.LectureViews.Queries.GetList;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureViews.Queries.GetListForLoggedStudent;
public class GetListLectureViewForLoggedStudentQuery : IRequest<ICollection<LectureView>>, ISecuredRequest/*, ICachableRequest*/
{
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetListLectureViewForLoggedStudentQueryHandler : IRequestHandler<GetListLectureViewForLoggedStudentQuery, ICollection<LectureView>>
    {
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListLectureViewForLoggedStudentQueryHandler(ILectureViewRepository lectureViewRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _lectureViewRepository = lectureViewRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<ICollection<LectureView>> Handle(GetListLectureViewForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            ICollection<LectureView> lectureViews = await _lectureViewRepository.GetAll(
                lv=>
                lv.StudentId==getStudent.Id &&
                lv.LectureId==request.LectureId);

            return lectureViews;
        }
    }
}

