using Application.Features.ClassAnnouncements.Commands.Create;
using Application.Features.ClassAnnouncements.Commands.Delete;
using Application.Features.ClassAnnouncements.Commands.Update;
using Application.Features.ClassAnnouncements.Queries.GetById;
using Application.Features.ClassAnnouncements.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassAnnouncementsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateClassAnnouncementCommand createClassAnnouncementCommand)
    {
        CreatedClassAnnouncementResponse response = await Mediator.Send(createClassAnnouncementCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClassAnnouncementCommand updateClassAnnouncementCommand)
    {
        UpdatedClassAnnouncementResponse response = await Mediator.Send(updateClassAnnouncementCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedClassAnnouncementResponse response = await Mediator.Send(new DeleteClassAnnouncementCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdClassAnnouncementResponse response = await Mediator.Send(new GetByIdClassAnnouncementQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListClassAnnouncementQuery getListClassAnnouncementQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListClassAnnouncementListItemDto> response = await Mediator.Send(getListClassAnnouncementQuery);
        return Ok(response);
    }
}