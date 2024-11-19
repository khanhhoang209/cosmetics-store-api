using CosmeticsStore.Services.DTO.Request;
using CosmeticsStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var serviceResponse = await _categoryService.GetByIdAsync(id);
        if (!serviceResponse.Succeeded)
        {
            return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateDTO category)
    {
        var serviceResponse = await _categoryService.CreateAsync(category);
        if (!serviceResponse.Succeeded)
        {
            return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
    }
}