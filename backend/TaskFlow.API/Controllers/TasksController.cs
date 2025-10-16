using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService service, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var tasks = service.GetAll();
        var dtos = mapper.Map<List<TaskDto>>(tasks);
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var task = service.GetById(id);
        if (task == null)
            return NotFound();
        var dto = mapper.Map<TaskDto>(task);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Create(TaskDto taskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var task = mapper.Map<TaskItem>(taskDto);
        var createdTask = service.Add(task);
        var createdDto = mapper.Map<TaskDto>(createdTask);

        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, TaskDto taskDto)
    {
        if (id != taskDto.Id)
            return BadRequest();

        var task = mapper.Map<TaskItem>(taskDto);
        var updated = service.Update(task);
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