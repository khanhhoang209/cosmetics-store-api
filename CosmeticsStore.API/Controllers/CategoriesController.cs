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
    public async Task<IActionResult> GetAllAsync([FromQuery] CategoryGetDTO category)
    {
        var serviceResponse = await _categoryService.GetAllAsync(category);
        if (!serviceResponse.Succeeded)
        {
            return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
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

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateDTO category)
    {
        var serviceResponse = await _categoryService.UpdateAsync(category);
        if (!serviceResponse.Succeeded)
        {
            return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> RemoveAsync([FromRoute] int id)
    {
        var serviceResponse = await _categoryService.RemoveAsync(id);
        if (!serviceResponse.Succeeded)
        {
            return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        return NoContent();
    }

    [HttpPut]
    [Route("{id:int}/Restore")]
    public async Task<IActionResult> RestoreAsync([FromRoute] int id)
    {
        var serviceResponse = await _categoryService.RestoreAsync(id);
        if (!serviceResponse.Succeeded)
        {
            return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
    }

}