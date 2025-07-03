namespace ApiWeb.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InstructorsController : ControllerBase
{
    private readonly IInstructorService _service;

    public InstructorsController(IInstructorService service)
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
        var instructor = await _service.GetByIdAsync(id);
        if (instructor == null)
            return NotFound("Instructor no encontrado.");
        return Ok(instructor);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InstructorDto dto)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");
        
        var result = await _service.AddAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] InstructorDto dto)
    {
        if (id <= 0)
            return BadRequest("El id debe ser positivo.");
        if (dto == null)
            return BadRequest("Datos inválidos.");
        
        var result = await _service.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser positivo.");
        var result = await _service.DeleteAsync(id);
        return Ok(result);
    }
}
