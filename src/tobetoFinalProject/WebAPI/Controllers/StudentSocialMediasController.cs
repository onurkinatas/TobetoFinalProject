using Application.Features.StudentSocialMedias.Queries.GetList;
using Application.Features.StudentSocialMedias.Queries.GetListByStudentId;
using Application.Features.StudentSocialMedias.Queries.GetListForLoggedStudent;
using Application.Features.StudentSocialMedias.Commands.Create;
using Application.Features.StudentSocialMedias.Commands.Delete;
using Application.Features.StudentSocialMedias.Commands.Update;
using Application.Features.StudentSocialMedias.Queries.GetById;
using Application.Features.StudentSocialMedias.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentSocialMediasController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentSocialMediaCommand createStudentSocialMediaCommand)
    {
        CreatedStudentSocialMediaResponse response = await Mediator.Send(createStudentSocialMediaCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentSocialMediaCommand updateStudentSocialMediaCommand)
    {
        UpdatedStudentSocialMediaResponse response = await Mediator.Send(updateStudentSocialMediaCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentSocialMediaResponse response = await Mediator.Send(new DeleteStudentSocialMediaCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentSocialMediaResponse response = await Mediator.Send(new GetByIdStudentSocialMediaQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentSocialMediaQuery getListStudentSocialMediaQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentSocialMediaListItemDto> response = await Mediator.Send(getListStudentSocialMediaQuery);
        return Ok(response);
    }
    [HttpGet("getListForLoggedStudent")]
    public async Task<IActionResult> GetListForLoggedStudent([FromQuery] PageRequest pageRequest)
    {
        GetListForLoggedStudentSocialMediaQuery getListStudentSocialMediaQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentSocialMediaListItemDto> response = await Mediator.Send(getListStudentSocialMediaQuery);
        return Ok(response);
    }
    [HttpGet("getListByStudentId{studentId}")]
    public async Task<IActionResult> GetListByStudentId([FromQuery] PageRequest pageRequest, [FromRoute] Guid studentId)
    {
        GetListByStudentIdStudentSocialMediaQuery getListStudentSocialMediaQuery = new() { PageRequest = pageRequest, StudentId = studentId };
        GetListResponse<GetListStudentSocialMediaListItemDto> response = await Mediator.Send(getListStudentSocialMediaQuery);
        return Ok(response);
    }
}