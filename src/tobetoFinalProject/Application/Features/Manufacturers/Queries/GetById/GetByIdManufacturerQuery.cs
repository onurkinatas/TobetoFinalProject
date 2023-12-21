using Application.Features.Manufacturers.Constants;
using Application.Features.Manufacturers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Manufacturers.Constants.ManufacturersOperationClaims;

namespace Application.Features.Manufacturers.Queries.GetById;

public class GetByIdManufacturerQuery : IRequest<GetByIdManufacturerResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdManufacturerQueryHandler : IRequestHandler<GetByIdManufacturerQuery, GetByIdManufacturerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ManufacturerBusinessRules _manufacturerBusinessRules;

        public GetByIdManufacturerQueryHandler(IMapper mapper, IManufacturerRepository manufacturerRepository, ManufacturerBusinessRules manufacturerBusinessRules)
        {
            _mapper = mapper;
            _manufacturerRepository = manufacturerRepository;
            _manufacturerBusinessRules = manufacturerBusinessRules;
        }

        public async Task<GetByIdManufacturerResponse> Handle(GetByIdManufacturerQuery request, CancellationToken cancellationToken)
        {
            Manufacturer? manufacturer = await _manufacturerRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _manufacturerBusinessRules.ManufacturerShouldExistWhenSelected(manufacturer);

            GetByIdManufacturerResponse response = _mapper.Map<GetByIdManufacturerResponse>(manufacturer);
            return response;
        }
    }
}