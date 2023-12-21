using Application.Features.AppealStages.Commands.Create;
using Application.Features.AppealStages.Commands.Delete;
using Application.Features.AppealStages.Commands.Update;
using Application.Features.AppealStages.Queries.GetById;
using Application.Features.AppealStages.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppealStagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAppealStageCommand createAppealStageCommand)
    {
        CreatedAppealStageResponse response = await Mediator.Send(createAppealStageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAppealStageCommand updateAppealStageCommand)
    {
        UpdatedAppealStageResponse response = await Mediator.Send(updateAppealStageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAppealStageResponse response = await Mediator.Send(new DeleteAppealStageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAppealStageResponse response = await Mediator.Send(new GetByIdAppealStageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAppealStageQuery getListAppealStageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAppealStageListItemDto> response = await Mediator.Send(getListAppealStageQuery);
        return Ok(response);
    }
}