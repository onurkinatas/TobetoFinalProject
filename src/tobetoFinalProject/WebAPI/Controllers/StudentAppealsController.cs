using Application.Features.StudentAppeals.Commands.Create;
using Application.Features.StudentAppeals.Commands.Delete;
using Application.Features.StudentAppeals.Commands.Update;
using Application.Features.StudentAppeals.Queries.GetById;
using Application.Features.StudentAppeals.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentAppealsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentAppealCommand createStudentAppealCommand)
    {
        CreatedStudentAppealResponse response = await Mediator.Send(createStudentAppealCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentAppealCommand updateStudentAppealCommand)
    {
        UpdatedStudentAppealResponse response = await Mediator.Send(updateStudentAppealCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentAppealResponse response = await Mediator.Send(new DeleteStudentAppealCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentAppealResponse response = await Mediator.Send(new GetByIdStudentAppealQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentAppealQuery getListStudentAppealQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentAppealListItemDto> response = await Mediator.Send(getListStudentAppealQuery);
        return Ok(response);
    }
}