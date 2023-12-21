using Application.Features.StudentLanguageLevels.Commands.Create;
using Application.Features.StudentLanguageLevels.Commands.Delete;
using Application.Features.StudentLanguageLevels.Commands.Update;
using Application.Features.StudentLanguageLevels.Queries.GetById;
using Application.Features.StudentLanguageLevels.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentLanguageLevelsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentLanguageLevelCommand createStudentLanguageLevelCommand)
    {
        CreatedStudentLanguageLevelResponse response = await Mediator.Send(createStudentLanguageLevelCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentLanguageLevelCommand updateStudentLanguageLevelCommand)
    {
        UpdatedStudentLanguageLevelResponse response = await Mediator.Send(updateStudentLanguageLevelCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentLanguageLevelResponse response = await Mediator.Send(new DeleteStudentLanguageLevelCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentLanguageLevelResponse response = await Mediator.Send(new GetByIdStudentLanguageLevelQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentLanguageLevelQuery getListStudentLanguageLevelQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentLanguageLevelListItemDto> response = await Mediator.Send(getListStudentLanguageLevelQuery);
        return Ok(response);
    }
}