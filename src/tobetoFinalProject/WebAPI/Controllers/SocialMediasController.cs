using Application.Features.SocialMedias.Commands.Create;
using Application.Features.SocialMedias.Commands.Delete;
using Application.Features.SocialMedias.Commands.Update;
using Application.Features.SocialMedias.Queries.GetById;
using Application.Features.SocialMedias.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SocialMediasController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSocialMediaCommand createSocialMediaCommand)
    {
        CreatedSocialMediaResponse response = await Mediator.Send(createSocialMediaCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSocialMediaCommand updateSocialMediaCommand)
    {
        UpdatedSocialMediaResponse response = await Mediator.Send(updateSocialMediaCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSocialMediaResponse response = await Mediator.Send(new DeleteSocialMediaCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSocialMediaResponse response = await Mediator.Send(new GetByIdSocialMediaQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSocialMediaQuery getListSocialMediaQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSocialMediaListItemDto> response = await Mediator.Send(getListSocialMediaQuery);
        return Ok(response);
    }
}