using Application.Features.GeneralQuizs.Commands.Create;
using Application.Features.GeneralQuizs.Commands.Delete;
using Application.Features.GeneralQuizs.Commands.Update;
using Application.Features.GeneralQuizs.Queries.GetById;
using Application.Features.GeneralQuizs.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneralQuizsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateGeneralQuizCommand createGeneralQuizCommand)
    {
        CreatedGeneralQuizResponse response = await Mediator.Send(createGeneralQuizCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateGeneralQuizCommand updateGeneralQuizCommand)
    {
        UpdatedGeneralQuizResponse response = await Mediator.Send(updateGeneralQuizCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedGeneralQuizResponse response = await Mediator.Send(new DeleteGeneralQuizCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdGeneralQuizResponse response = await Mediator.Send(new GetByIdGeneralQuizQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListGeneralQuizQuery getListGeneralQuizQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListGeneralQuizListItemDto> response = await Mediator.Send(getListGeneralQuizQuery);
        return Ok(response);
    }
}