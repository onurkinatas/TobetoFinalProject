using Application.Features.StudentAnnouncements.Commands.Create;
using Application.Features.StudentAnnouncements.Commands.Delete;
using Application.Features.StudentAnnouncements.Commands.Update;
using Application.Features.StudentAnnouncements.Queries.GetById;
using Application.Features.StudentAnnouncements.Queries.GetList;
using Application.Features.StudentAnnouncements.Queries.GetListByAnnouncementId;
using Application.Features.StudentAnnouncements.Queries.GetListForLoggedStudent;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentAnnouncementsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentAnnouncementCommand createStudentAnnouncementCommand)
    {
        CreatedStudentAnnouncementResponse response = await Mediator.Send(createStudentAnnouncementCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentAnnouncementCommand updateStudentAnnouncementCommand)
    {
        UpdatedStudentAnnouncementResponse response = await Mediator.Send(updateStudentAnnouncementCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentAnnouncementResponse response = await Mediator.Send(new DeleteStudentAnnouncementCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentAnnouncementResponse response = await Mediator.Send(new GetByIdStudentAnnouncementQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentAnnouncementQuery getListStudentAnnouncementQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentAnnouncementListItemDto> response = await Mediator.Send(getListStudentAnnouncementQuery);
        return Ok(response);
    }
    [HttpGet("getListForLoggedStudent")]
    public async Task<IActionResult> GetListForLoggedStudent([FromQuery] PageRequest pageRequest)
    {
        GetListForLoggedStudentAnnouncementQuery getListStudentAnnouncementQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListForLoggedStudentAnnouncementResponse> response = await Mediator.Send(getListStudentAnnouncementQuery);
        return Ok(response);
    }
    [HttpGet("getListByAnnouncementId{announcementId}")]
    public async Task<IActionResult> GetListByAnnouncementId([FromQuery]PageRequest pageRequest,[FromRoute]Guid announcementId)
    {
        GetListByAnnouncementIdStudentAnnouncementQuery getListStudentAnnouncementQuery = new() {PageRequest=pageRequest,AnnouncementId=announcementId };
        GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse> response = await Mediator.Send(getListStudentAnnouncementQuery);
        return Ok(response);
    }
}