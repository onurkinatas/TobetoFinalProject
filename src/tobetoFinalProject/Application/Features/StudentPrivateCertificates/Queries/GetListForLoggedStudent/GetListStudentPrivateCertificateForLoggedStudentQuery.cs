using Application.Features.StudentPrivateCertificates.Queries.GetList;
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

namespace Application.Features.StudentPrivateCertificates.Queries.GetListForLoggedStudent;
public class GetListStudentPrivateCertificateForLoggedStudentQuery : IRequest<GetListResponse<GetListStudentPrivateCertificateListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student"};


    public class GetListStudentPrivateCertificateForLoggedStudentQueryHandler : IRequestHandler<GetListStudentPrivateCertificateForLoggedStudentQuery, GetListResponse<GetListStudentPrivateCertificateListItemDto>>
    {
        private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentPrivateCertificateForLoggedStudentQueryHandler(IStudentPrivateCertificateRepository studentPrivateCertificateRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentPrivateCertificateListItemDto>> Handle(GetListStudentPrivateCertificateForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentPrivateCertificate> studentPrivateCertificates = await _studentPrivateCertificateRepository.GetListAsync(
                predicate:spc=>spc.StudentId==getStudent.Id,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentPrivateCertificateListItemDto> response = _mapper.Map<GetListResponse<GetListStudentPrivateCertificateListItemDto>>(studentPrivateCertificates);
            return response;
        }
    }
}