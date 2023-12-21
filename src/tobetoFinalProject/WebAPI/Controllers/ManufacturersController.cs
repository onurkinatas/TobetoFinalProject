using Application.Features.Manufacturers.Commands.Create;
using Application.Features.Manufacturers.Commands.Delete;
using Application.Features.Manufacturers.Commands.Update;
using Application.Features.Manufacturers.Queries.GetById;
using Application.Features.Manufacturers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufacturersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateManufacturerCommand createManufacturerCommand)
    {
        CreatedManufacturerResponse response = await Mediator.Send(createManufacturerCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateManufacturerCommand updateManufacturerCommand)
    {
        UpdatedManufacturerResponse response = await Mediator.Send(updateManufacturerCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedManufacturerResponse response = await Mediator.Send(new DeleteManufacturerCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdManufacturerResponse response = await Mediator.Send(new GetByIdManufacturerQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListManufacturerQuery getListManufacturerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListManufacturerListItemDto> response = await Mediator.Send(getListManufacturerQuery);
        return Ok(response);
    }
}