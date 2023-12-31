using Application.Features.StudentEducations.Commands.Create;
using Application.Features.StudentEducations.Commands.Delete;
using Application.Features.StudentEducations.Commands.Update;
using Application.Features.StudentEducations.Queries.GetById;
using Application.Features.StudentEducations.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentEducationsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentEducationCommand createStudentEducationCommand)
    {
        CreatedStudentEducationResponse response = await Mediator.Send(createStudentEducationCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentEducationCommand updateStudentEducationCommand)
    {
        UpdatedStudentEducationResponse response = await Mediator.Send(updateStudentEducationCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentEducationResponse response = await Mediator.Send(new DeleteStudentEducationCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentEducationResponse response = await Mediator.Send(new GetByIdStudentEducationQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentEducationQuery getListStudentEducationQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentEducationListItemDto> response = await Mediator.Send(getListStudentEducationQuery);
        return Ok(response);
    }
}