using Application.Features.ClassQuizs.Commands.Create;
using Application.Features.ClassQuizs.Commands.Delete;
using Application.Features.ClassQuizs.Commands.Update;
using Application.Features.ClassQuizs.Queries.GetById;
using Application.Features.ClassQuizs.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassQuizsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateClassQuizCommand createClassQuizCommand)
    {
        CreatedClassQuizResponse response = await Mediator.Send(createClassQuizCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClassQuizCommand updateClassQuizCommand)
    {
        UpdatedClassQuizResponse response = await Mediator.Send(updateClassQuizCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedClassQuizResponse response = await Mediator.Send(new DeleteClassQuizCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdClassQuizResponse response = await Mediator.Send(new GetByIdClassQuizQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListClassQuizQuery getListClassQuizQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListClassQuizListItemDto> response = await Mediator.Send(getListClassQuizQuery);
        return Ok(response);
    }
}