using Application.Features.StudentExperiences.Commands.Create;
using Application.Features.StudentExperiences.Commands.Delete;
using Application.Features.StudentExperiences.Commands.Update;
using Application.Features.StudentExperiences.Queries.GetById;
using Application.Features.StudentExperiences.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentExperiencesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentExperienceCommand createStudentExperienceCommand)
    {
        CreatedStudentExperienceResponse response = await Mediator.Send(createStudentExperienceCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentExperienceCommand updateStudentExperienceCommand)
    {
        UpdatedStudentExperienceResponse response = await Mediator.Send(updateStudentExperienceCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentExperienceResponse response = await Mediator.Send(new DeleteStudentExperienceCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentExperienceResponse response = await Mediator.Send(new GetByIdStudentExperienceQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentExperienceQuery getListStudentExperienceQuery = new() { PageRequest = pageRequest};
        GetListResponse<GetListStudentExperienceListItemDto> response = await Mediator.Send(getListStudentExperienceQuery);
        return Ok(response);
    }


}