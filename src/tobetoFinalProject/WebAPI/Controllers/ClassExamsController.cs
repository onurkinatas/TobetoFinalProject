using Application.Features.ClassExams.Commands.Create;
using Application.Features.ClassExams.Commands.Delete;
using Application.Features.ClassExams.Commands.Update;
using Application.Features.ClassExams.Queries.GetById;
using Application.Features.ClassExams.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassExamsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateClassExamCommand createClassExamCommand)
    {
        CreatedClassExamResponse response = await Mediator.Send(createClassExamCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClassExamCommand updateClassExamCommand)
    {
        UpdatedClassExamResponse response = await Mediator.Send(updateClassExamCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedClassExamResponse response = await Mediator.Send(new DeleteClassExamCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdClassExamResponse response = await Mediator.Send(new GetByIdClassExamQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListClassExamQuery getListClassExamQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListClassExamListItemDto> response = await Mediator.Send(getListClassExamQuery);
        return Ok(response);
    }
}