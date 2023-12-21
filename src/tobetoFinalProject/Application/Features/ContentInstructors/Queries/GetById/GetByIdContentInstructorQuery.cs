using Application.Features.ContentInstructors.Constants;
using Application.Features.ContentInstructors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentInstructors.Constants.ContentInstructorsOperationClaims;

namespace Application.Features.ContentInstructors.Queries.GetById;

public class GetByIdContentInstructorQuery : IRequest<GetByIdContentInstructorResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentInstructorQueryHandler : IRequestHandler<GetByIdContentInstructorQuery, GetByIdContentInstructorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentInstructorRepository _contentInstructorRepository;
        private readonly ContentInstructorBusinessRules _contentInstructorBusinessRules;

        public GetByIdContentInstructorQueryHandler(IMapper mapper, IContentInstructorRepository contentInstructorRepository, ContentInstructorBusinessRules contentInstructorBusinessRules)
        {
            _mapper = mapper;
            _contentInstructorRepository = contentInstructorRepository;
            _contentInstructorBusinessRules = contentInstructorBusinessRules;
        }

        public async Task<GetByIdContentInstructorResponse> Handle(GetByIdContentInstructorQuery request, CancellationToken cancellationToken)
        {
            ContentInstructor? contentInstructor = await _contentInstructorRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _contentInstructorBusinessRules.ContentInstructorShouldExistWhenSelected(contentInstructor);

            GetByIdContentInstructorResponse response = _mapper.Map<GetByIdContentInstructorResponse>(contentInstructor);
            return response;
        }
    }
}