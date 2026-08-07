using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.DTOs.Categories;
using Momentum.Application.Interfaces.Categories;
using System.Security.Claims;

namespace Momentum.API.Controllers.Categories;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create(
        [FromBody] CreateCategoryRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
        {
            return Unauthorized();
        }

        if(!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var category = await _categoryService.CreateAsync(userId, request);

        return Ok(category);
    }    
}