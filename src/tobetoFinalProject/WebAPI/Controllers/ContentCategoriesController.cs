using Application.Features.ContentCategories.Commands.Create;
using Application.Features.ContentCategories.Commands.Delete;
using Application.Features.ContentCategories.Commands.Update;
using Application.Features.ContentCategories.Queries.GetById;
using Application.Features.ContentCategories.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentCategoriesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentCategoryCommand createContentCategoryCommand)
    {
        CreatedContentCategoryResponse response = await Mediator.Send(createContentCategoryCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentCategoryCommand updateContentCategoryCommand)
    {
        UpdatedContentCategoryResponse response = await Mediator.Send(updateContentCategoryCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedContentCategoryResponse response = await Mediator.Send(new DeleteContentCategoryCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdContentCategoryResponse response = await Mediator.Send(new GetByIdContentCategoryQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentCategoryQuery getListContentCategoryQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentCategoryListItemDto> response = await Mediator.Send(getListContentCategoryQuery);
        return Ok(response);
    }
}