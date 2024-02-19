using Application.Features.StudentSkills.Queries.GetList;
using Application.Features.StudentSkills.Queries.GetListByStudentId;
using Application.Features.StudentSkills.Queries.GetListForLoggedStudent;
using Application.Features.StudentSkills.Commands.Create;
using Application.Features.StudentSkills.Commands.Delete;
using Application.Features.StudentSkills.Commands.Update;
using Application.Features.StudentSkills.Queries.GetById;
using Application.Features.StudentSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentSkillsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentSkillCommand createStudentSkillCommand)
    {
        createStudentSkillCommand.UserId = getUserIdFromRequest();
        CreatedStudentSkillResponse response = await Mediator.Send(createStudentSkillCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentSkillCommand updateStudentSkillCommand)
    {updateStudentSkillCommand.UserId = getUserIdFromRequest();
        UpdatedStudentSkillResponse response = await Mediator.Send(updateStudentSkillCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentSkillResponse response = await Mediator.Send(new DeleteStudentSkillCommand { Id = id ,UserId=getUserIdFromRequest()});

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentSkillResponse response = await Mediator.Send(new GetByIdStudentSkillQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentSkillQuery getListStudentSkillQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentSkillListItemDto> response = await Mediator.Send(getListStudentSkillQuery);
        return Ok(response);
    }
    [HttpGet("getListForLoggedStudent")]
    public async Task<IActionResult> GetListForLoggedStudent([FromQuery] PageRequest pageRequest)
    {
        GetListForLoggedStudentSkillQuery getListStudentSkillQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentSkillListItemDto> response = await Mediator.Send(getListStudentSkillQuery);
        return Ok(response);
    }
    [HttpGet("getListByStudentId{studentId}")]
    public async Task<IActionResult> GetListByStudentId([FromQuery] PageRequest pageRequest, [FromRoute] Guid studentId)
    {
        GetListByStudentIdStudentSkillQuery getListStudentSkillQuery = new() { PageRequest = pageRequest, StudentId = studentId };
        GetListResponse<GetListStudentSkillListItemDto> response = await Mediator.Send(getListStudentSkillQuery);
        return Ok(response);
    }
}