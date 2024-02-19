using Application.Features.StudentQuizResults.Commands.Create;
using Application.Features.StudentQuizResults.Commands.Delete;
using Application.Features.StudentQuizResults.Commands.Update;
using Application.Features.StudentQuizResults.Queries.GetByQuizId;
using Application.Features.StudentQuizResults.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentQuizResultsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentQuizResultCommand createStudentQuizResultCommand)
    {
        createStudentQuizResultCommand.UserId = getUserIdFromRequest();
        CreatedStudentQuizResultResponse response = await Mediator.Send(createStudentQuizResultCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentQuizResultCommand updateStudentQuizResultCommand)
    {
        UpdatedStudentQuizResultResponse response = await Mediator.Send(updateStudentQuizResultCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedStudentQuizResultResponse response = await Mediator.Send(new DeleteStudentQuizResultCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdStudentQuizResultResponse response = await Mediator.Send(new GetByIdStudentQuizResultQuery { QuizId = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentQuizResultQuery getListStudentQuizResultQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentQuizResultListItemDto> response = await Mediator.Send(getListStudentQuizResultQuery);
        return Ok(response);
    }
}