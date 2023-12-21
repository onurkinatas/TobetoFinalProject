using Application.Features.StudentStages.Commands.Create;
using Application.Features.StudentStages.Commands.Delete;
using Application.Features.StudentStages.Commands.Update;
using Application.Features.StudentStages.Queries.GetById;
using Application.Features.StudentStages.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentStagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentStageCommand createStudentStageCommand)
    {
        CreatedStudentStageResponse response = await Mediator.Send(createStudentStageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentStageCommand updateStudentStageCommand)
    {
        UpdatedStudentStageResponse response = await Mediator.Send(updateStudentStageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentStageResponse response = await Mediator.Send(new DeleteStudentStageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentStageResponse response = await Mediator.Send(new GetByIdStudentStageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentStageQuery getListStudentStageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentStageListItemDto> response = await Mediator.Send(getListStudentStageQuery);
        return Ok(response);
    }
}