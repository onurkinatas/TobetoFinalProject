using Application.Features.LectureLikes.Queries.GetList;
using Application.Features.LectureLikes.Queries.GetListByLectureId;
using Application.Features.LectureViews.Commands.Create;
using Application.Features.LectureViews.Commands.Delete;
using Application.Features.LectureViews.Commands.Update;
using Application.Features.LectureViews.Queries.GelAllForLoggedStudent;
using Application.Features.LectureViews.Queries.GetById;
using Application.Features.LectureViews.Queries.GetLectureViewCount;
using Application.Features.LectureViews.Queries.GetList;
using Application.Features.LectureViews.Queries.GetListByLectureAndContentId;
using Application.Features.LectureViews.Queries.GetListForLoggedStudent;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.LectureViews.Queries.GetListByLectureAndContentId.GetListByLectureAndContentIdLectureViewQuery;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LectureViewsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLectureViewCommand createLectureViewCommand)
    {
        createLectureViewCommand.UserId = getUserIdFromRequest();
        CreatedLectureViewResponse response = await Mediator.Send(createLectureViewCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLectureViewCommand updateLectureViewCommand)
    {
        UpdatedLectureViewResponse response = await Mediator.Send(updateLectureViewCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLectureViewResponse response = await Mediator.Send(new DeleteLectureViewCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLectureViewResponse response = await Mediator.Send(new GetByIdLectureViewQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLectureViewQuery getListLectureViewQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureViewListItemDto> response = await Mediator.Send(getListLectureViewQuery);
        return Ok(response);
    }
    [HttpGet("getForLoggedStudent{lectureId}")]
    public async Task<IActionResult> GetAllForLoggedStudent([FromRoute] Guid lectureId)
    {
        GetListLectureViewForLoggedStudentQuery getListLectureViewQuery = new() { LectureId=lectureId};
        ICollection<LectureView> response = await Mediator.Send(getListLectureViewQuery);
        return Ok(response);
    }
    [HttpGet("getListWithLectureId")]
    public async Task<IActionResult> GetListByLectureAndContentId([FromQuery] PageRequest pageRequest, [FromQuery] Guid lectureId, [FromQuery] Guid contentId)
    {
        GetListByLectureAndContentIdLectureViewQuery getListByLectureIdLikeQuery = new() { PageRequest = pageRequest, LectureId = lectureId ,ContentId=contentId};
        GetListResponse<GetListLectureViewListItemDto> response = await Mediator.Send(getListByLectureIdLikeQuery);
        return Ok(response);
    }
    [HttpGet("getCountWithLectureAndContentId")]
    public async Task<IActionResult> GetCountWithLectureAndContentId([FromQuery] Guid lectureId, [FromQuery] Guid contentId)
    {
        GetLectureViewCountQuery getListByLectureLikeQuery = new() {  LectureId = lectureId, ContentId = contentId };
        GetLectureViewCountQueryResponse response = await Mediator.Send(getListByLectureLikeQuery);
        return Ok(response);
    }
    [HttpGet("getAllForLoggedStudent")]
    public async Task<IActionResult> GetAllForLoggedStudent()
    {
        GetAllLectureViewForLoggedStudentQuery getListByLectureIdLikeQuery = new() { };
        GetAllLectureViewForLoggedStudentItemDto response = await Mediator.Send(getListByLectureIdLikeQuery);
        return Ok(response);
    }
}