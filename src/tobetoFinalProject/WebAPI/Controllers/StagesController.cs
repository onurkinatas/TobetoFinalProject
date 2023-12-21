using Application.Features.Stages.Commands.Create;
using Application.Features.Stages.Commands.Delete;
using Application.Features.Stages.Commands.Update;
using Application.Features.Stages.Queries.GetById;
using Application.Features.Stages.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStageCommand createStageCommand)
    {
        CreatedStageResponse response = await Mediator.Send(createStageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStageCommand updateStageCommand)
    {
        UpdatedStageResponse response = await Mediator.Send(updateStageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStageResponse response = await Mediator.Send(new DeleteStageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStageResponse response = await Mediator.Send(new GetByIdStageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStageQuery getListStageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStageListItemDto> response = await Mediator.Send(getListStageQuery);
        return Ok(response);
    }
}