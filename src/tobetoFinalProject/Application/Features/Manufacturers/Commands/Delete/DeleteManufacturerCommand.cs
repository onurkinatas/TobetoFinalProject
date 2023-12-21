using Application.Features.Manufacturers.Constants;
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

namespace Application.Features.Manufacturers.Commands.Delete;

public class DeleteManufacturerCommand : IRequest<DeletedManufacturerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ManufacturersOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetManufacturers";

    public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturerCommand, DeletedManufacturerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ManufacturerBusinessRules _manufacturerBusinessRules;

        public DeleteManufacturerCommandHandler(IMapper mapper, IManufacturerRepository manufacturerRepository,
                                         ManufacturerBusinessRules manufacturerBusinessRules)
        {
            _mapper = mapper;
            _manufacturerRepository = manufacturerRepository;
            _manufacturerBusinessRules = manufacturerBusinessRules;
        }

        public async Task<DeletedManufacturerResponse> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            Manufacturer? manufacturer = await _manufacturerRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _manufacturerBusinessRules.ManufacturerShouldExistWhenSelected(manufacturer);

            await _manufacturerRepository.DeleteAsync(manufacturer!);

            DeletedManufacturerResponse response = _mapper.Map<DeletedManufacturerResponse>(manufacturer);
            return response;
        }
    }
}