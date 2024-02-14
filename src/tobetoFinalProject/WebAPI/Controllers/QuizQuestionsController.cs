using Application.Features.QuizQuestions.Commands.Create;
using Application.Features.QuizQuestions.Commands.Delete;
using Application.Features.QuizQuestions.Commands.Update;
using Application.Features.QuizQuestions.Queries.GetById;
using Application.Features.QuizQuestions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizQuestionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQuizQuestionCommand createQuizQuestionCommand)
    {
        CreatedQuizQuestionResponse response = await Mediator.Send(createQuizQuestionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuizQuestionCommand updateQuizQuestionCommand)
    {
        UpdatedQuizQuestionResponse response = await Mediator.Send(updateQuizQuestionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedQuizQuestionResponse response = await Mediator.Send(new DeleteQuizQuestionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdQuizQuestionResponse response = await Mediator.Send(new GetByIdQuizQuestionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuizQuestionQuery getListQuizQuestionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuizQuestionListItemDto> response = await Mediator.Send(getListQuizQuestionQuery);
        return Ok(response);
    }
}