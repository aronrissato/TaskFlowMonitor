using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var tasks = service.GetAll();
        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var task = service.GetById(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create(TaskDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdDto = service.Add(taskDto);
        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, TaskDto taskDto)
    {
        if (id != taskDto.Id)
            return BadRequest();

        var updated = service.Update(taskDto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = service.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}