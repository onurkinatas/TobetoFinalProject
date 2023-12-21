using Application.Features.StudentClassStudents.Commands.Create;
using Application.Features.StudentClassStudents.Commands.Delete;
using Application.Features.StudentClassStudents.Commands.Update;
using Application.Features.StudentClassStudents.Queries.GetById;
using Application.Features.StudentClassStudents.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentClassStudentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentClassStudentCommand createStudentClassStudentCommand)
    {
        CreatedStudentClassStudentResponse response = await Mediator.Send(createStudentClassStudentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentClassStudentCommand updateStudentClassStudentCommand)
    {
        UpdatedStudentClassStudentResponse response = await Mediator.Send(updateStudentClassStudentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentClassStudentResponse response = await Mediator.Send(new DeleteStudentClassStudentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentClassStudentResponse response = await Mediator.Send(new GetByIdStudentClassStudentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentClassStudentQuery getListStudentClassStudentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentClassStudentListItemDto> response = await Mediator.Send(getListStudentClassStudentQuery);
        return Ok(response);
    }
}