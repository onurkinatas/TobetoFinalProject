using Application.Features.Appeals.Commands.Create;
using Application.Features.Appeals.Commands.Delete;
using Application.Features.Appeals.Commands.Update;
using Application.Features.Appeals.Queries.GetById;
using Application.Features.Appeals.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppealsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAppealCommand createAppealCommand)
    {
        CreatedAppealResponse response = await Mediator.Send(createAppealCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAppealCommand updateAppealCommand)
    {
        UpdatedAppealResponse response = await Mediator.Send(updateAppealCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAppealResponse response = await Mediator.Send(new DeleteAppealCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAppealResponse response = await Mediator.Send(new GetByIdAppealQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAppealQuery getListAppealQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAppealListItemDto> response = await Mediator.Send(getListAppealQuery);
        return Ok(response);
    }
}