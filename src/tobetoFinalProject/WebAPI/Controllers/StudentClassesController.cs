using Application.Features.StudentClasses.Commands.Create;
using Application.Features.StudentClasses.Commands.Delete;
using Application.Features.StudentClasses.Commands.Update;
using Application.Features.StudentClasses.Queries.GetById;
using Application.Features.StudentClasses.Queries.GetList;
using Application.Features.StudentClasses.Queries.GetListForLoggedStudent;
using Application.Services.ContextOperations;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentClassesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentClassCommand createStudentClassCommand)
    {
        CreatedStudentClassResponse response = await Mediator.Send(createStudentClassCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentClassCommand updateStudentClassCommand)
    {
        UpdatedStudentClassResponse response = await Mediator.Send(updateStudentClassCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentClassResponse response = await Mediator.Send(new DeleteStudentClassCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentClassResponse response = await Mediator.Send(new GetByIdStudentClassQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentClassQuery getListStudentClassQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentClasses> response = await Mediator.Send(getListStudentClassQuery);
        return Ok(response);
    }
    [HttpGet("GetListForLoggedStudent")]
    public async Task<IActionResult> GetListForLoggedStudent()
    {
        GetListForLoggedStudentClassQuery getListStudentClassQuery = new() { UserId=getUserIdFromRequest() };
        GetListForLoggedStudentClassListItemDto response = await Mediator.Send(getListStudentClassQuery);
        return Ok(response);
    }

}