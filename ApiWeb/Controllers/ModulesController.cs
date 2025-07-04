using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModulesController : ControllerBase
{
    private readonly IModuleService _service;

    public ModulesController(IModuleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Los parámetros de paginación deben ser números positivos.");
        var result = await _service.GetAllAsync();
        var paged = result.Skip((page - 1) * pageSize).Take(pageSize);
        return Ok(paged);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser positivo.");
        var module = await _service.GetByIdAsync(id);
        if (module == null) return NotFound();

        var dto = new Application.DTOs.ModuleDto
        {
            Id = module.Id,
            Title = module.Title,
            CourseId = module.CourseId,
            CreatedAt = module.CreatedAt
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ModuleDto dto)
        => Ok(await _service.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ModuleDto dto)
    {
        if (id <= 0)
            return BadRequest("El id debe ser positivo.");
        return Ok(await _service.UpdateAsync(id, dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser positivo.");
        return Ok(await _service.DeleteAsync(id));
    }
}
