using Application.Features.PoolQuestions.Commands.Create;
using Application.Features.PoolQuestions.Commands.Delete;
using Application.Features.PoolQuestions.Commands.Update;
using Application.Features.PoolQuestions.Queries.GetById;
using Application.Features.PoolQuestions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PoolQuestionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePoolQuestionCommand createPoolQuestionCommand)
    {
        CreatedPoolQuestionResponse response = await Mediator.Send(createPoolQuestionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePoolQuestionCommand updatePoolQuestionCommand)
    {
        UpdatedPoolQuestionResponse response = await Mediator.Send(updatePoolQuestionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedPoolQuestionResponse response = await Mediator.Send(new DeletePoolQuestionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdPoolQuestionResponse response = await Mediator.Send(new GetByIdPoolQuestionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPoolQuestionQuery getListPoolQuestionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPoolQuestionListItemDto> response = await Mediator.Send(getListPoolQuestionQuery);
        return Ok(response);
    }
}