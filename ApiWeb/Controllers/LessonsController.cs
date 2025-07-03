using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonsController(ILessonService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Los parámetros de paginación deben ser números positivos.");
        var result = await _service.GetAllAsync();
        var paged = result.Skip((page - 1) * pageSize).Take(pageSize);
        return Ok(paged);
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
        var lesson = await _service.GetByIdAsync(id);
        return lesson == null ? NotFound() : Ok(lesson);
    }

    [HttpPost]
    public async Task<IActionResult> Post(LessonDto dto)
        => Ok(await _service.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, LessonDto dto)
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
