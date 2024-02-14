using Application.Features.Options.Commands.Create;
using Application.Features.Options.Commands.Delete;
using Application.Features.Options.Commands.Update;
using Application.Features.Options.Queries.GetById;
using Application.Features.Options.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OptionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOptionCommand createOptionCommand)
    {
        CreatedOptionResponse response = await Mediator.Send(createOptionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOptionCommand updateOptionCommand)
    {
        UpdatedOptionResponse response = await Mediator.Send(updateOptionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedOptionResponse response = await Mediator.Send(new DeleteOptionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdOptionResponse response = await Mediator.Send(new GetByIdOptionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOptionQuery getListOptionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListOptionListItemDto> response = await Mediator.Send(getListOptionQuery);
        return Ok(response);
    }
}