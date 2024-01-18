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

namespace Application.Features.Manufacturers.Commands.Update;

public class UpdateManufacturerCommand : IRequest<UpdatedManufacturerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, ManufacturersOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetManufacturers";

    public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturerCommand, UpdatedManufacturerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ManufacturerBusinessRules _manufacturerBusinessRules;

        public UpdateManufacturerCommandHandler(IMapper mapper, IManufacturerRepository manufacturerRepository,
                                         ManufacturerBusinessRules manufacturerBusinessRules)
        {
            _mapper = mapper;
            _manufacturerRepository = manufacturerRepository;
            _manufacturerBusinessRules = manufacturerBusinessRules;
        }

        public async Task<UpdatedManufacturerResponse> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
        {
            Manufacturer? manufacturer = await _manufacturerRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _manufacturerBusinessRules.ManufacturerShouldExistWhenSelected(manufacturer);
            manufacturer = _mapper.Map(request, manufacturer);

            await _manufacturerBusinessRules.ManufacturerNameShouldNotExist(manufacturer, cancellationToken);

            await _manufacturerRepository.UpdateAsync(manufacturer!);

            UpdatedManufacturerResponse response = _mapper.Map<UpdatedManufacturerResponse>(manufacturer);
            return response;
        }
    }
}