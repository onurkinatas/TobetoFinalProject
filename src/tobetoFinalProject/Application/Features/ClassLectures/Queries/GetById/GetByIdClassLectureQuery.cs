using Application.Features.ClassLectures.Constants;
using Application.Features.ClassLectures.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ClassLectures.Constants.ClassLecturesOperationClaims;

namespace Application.Features.ClassLectures.Queries.GetById;

public class GetByIdClassLectureQuery : IRequest<GetByIdClassLectureResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdClassLectureQueryHandler : IRequestHandler<GetByIdClassLectureQuery, GetByIdClassLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly ClassLectureBusinessRules _classLectureBusinessRules;

        public GetByIdClassLectureQueryHandler(IMapper mapper, IClassLectureRepository classLectureRepository, ClassLectureBusinessRules classLectureBusinessRules)
        {
            _mapper = mapper;
            _classLectureRepository = classLectureRepository;
            _classLectureBusinessRules = classLectureBusinessRules;
        }

        public async Task<GetByIdClassLectureResponse> Handle(GetByIdClassLectureQuery request, CancellationToken cancellationToken)
        {
            ClassLecture? classLecture = await _classLectureRepository.GetAsync(predicate: cl => cl.Id == request.Id, cancellationToken: cancellationToken);
            await _classLectureBusinessRules.ClassLectureShouldExistWhenSelected(classLecture);

            GetByIdClassLectureResponse response = _mapper.Map<GetByIdClassLectureResponse>(classLecture);
            return response;
        }
    }
}