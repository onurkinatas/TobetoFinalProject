using Application.Features.Pools.Commands.Create;
using Application.Features.Pools.Commands.Delete;
using Application.Features.Pools.Commands.Update;
using Application.Features.Pools.Queries.GetById;
using Application.Features.Pools.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PoolsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePoolCommand createPoolCommand)
    {
        CreatedPoolResponse response = await Mediator.Send(createPoolCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePoolCommand updatePoolCommand)
    {
        UpdatedPoolResponse response = await Mediator.Send(updatePoolCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedPoolResponse response = await Mediator.Send(new DeletePoolCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdPoolResponse response = await Mediator.Send(new GetByIdPoolQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPoolQuery getListPoolQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPoolListItemDto> response = await Mediator.Send(getListPoolQuery);
        return Ok(response);
    }
}