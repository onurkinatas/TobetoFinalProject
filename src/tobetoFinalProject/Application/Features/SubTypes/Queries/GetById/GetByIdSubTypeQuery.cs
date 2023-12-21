using Application.Features.SubTypes.Constants;
using Application.Features.SubTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SubTypes.Constants.SubTypesOperationClaims;

namespace Application.Features.SubTypes.Queries.GetById;

public class GetByIdSubTypeQuery : IRequest<GetByIdSubTypeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdSubTypeQueryHandler : IRequestHandler<GetByIdSubTypeQuery, GetByIdSubTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly SubTypeBusinessRules _subTypeBusinessRules;

        public GetByIdSubTypeQueryHandler(IMapper mapper, ISubTypeRepository subTypeRepository, SubTypeBusinessRules subTypeBusinessRules)
        {
            _mapper = mapper;
            _subTypeRepository = subTypeRepository;
            _subTypeBusinessRules = subTypeBusinessRules;
        }

        public async Task<GetByIdSubTypeResponse> Handle(GetByIdSubTypeQuery request, CancellationToken cancellationToken)
        {
            SubType? subType = await _subTypeRepository.GetAsync(predicate: st => st.Id == request.Id, cancellationToken: cancellationToken);
            await _subTypeBusinessRules.SubTypeShouldExistWhenSelected(subType);

            GetByIdSubTypeResponse response = _mapper.Map<GetByIdSubTypeResponse>(subType);
            return response;
        }
    }
}