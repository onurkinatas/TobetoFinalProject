using Application.Features.QuestionOptions.Commands.Create;
using Application.Features.QuestionOptions.Commands.Delete;
using Application.Features.QuestionOptions.Commands.Update;
using Application.Features.QuestionOptions.Queries.GetById;
using Application.Features.QuestionOptions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionOptionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQuestionOptionCommand createQuestionOptionCommand)
    {
        CreatedQuestionOptionResponse response = await Mediator.Send(createQuestionOptionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuestionOptionCommand updateQuestionOptionCommand)
    {
        UpdatedQuestionOptionResponse response = await Mediator.Send(updateQuestionOptionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedQuestionOptionResponse response = await Mediator.Send(new DeleteQuestionOptionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdQuestionOptionResponse response = await Mediator.Send(new GetByIdQuestionOptionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuestionOptionQuery getListQuestionOptionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuestionOptionListItemDto> response = await Mediator.Send(getListQuestionOptionQuery);
        return Ok(response);
    }
}