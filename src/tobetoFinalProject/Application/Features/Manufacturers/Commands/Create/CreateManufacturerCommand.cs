using Application.Features.Manufacturers.Constants;
using Application.Features.Manufacturers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Manufacturers.Constants.ManufacturersOperationClaims;

namespace Application.Features.Manufacturers.Commands.Create;

public class CreateManufacturerCommand : IRequest<CreatedManufacturerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, ManufacturersOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetManufacturers";

    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, CreatedManufacturerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ManufacturerBusinessRules _manufacturerBusinessRules;

        public CreateManufacturerCommandHandler(IMapper mapper, IManufacturerRepository manufacturerRepository,
                                         ManufacturerBusinessRules manufacturerBusinessRules)
        {
            _mapper = mapper;
            _manufacturerRepository = manufacturerRepository;
            _manufacturerBusinessRules = manufacturerBusinessRules;
        }

        public async Task<CreatedManufacturerResponse> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            Manufacturer manufacturer = _mapper.Map<Manufacturer>(request);

            await _manufacturerBusinessRules.ManufacturerShouldNotExistsWhenInsert(manufacturer.Name);

            await _manufacturerRepository.AddAsync(manufacturer);

            CreatedManufacturerResponse response = _mapper.Map<CreatedManufacturerResponse>(manufacturer);
            return response;
        }
    }
}