using Application.Features.SubTypes.Commands.Create;
using Application.Features.SubTypes.Commands.Delete;
using Application.Features.SubTypes.Commands.Update;
using Application.Features.SubTypes.Queries.GetById;
using Application.Features.SubTypes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubTypesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubTypeCommand createSubTypeCommand)
    {
        CreatedSubTypeResponse response = await Mediator.Send(createSubTypeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubTypeCommand updateSubTypeCommand)
    {
        UpdatedSubTypeResponse response = await Mediator.Send(updateSubTypeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSubTypeResponse response = await Mediator.Send(new DeleteSubTypeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSubTypeResponse response = await Mediator.Send(new GetByIdSubTypeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubTypeQuery getListSubTypeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSubTypeListItemDto> response = await Mediator.Send(getListSubTypeQuery);
        return Ok(response);
    }
}