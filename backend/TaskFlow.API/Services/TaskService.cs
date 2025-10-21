using AutoMapper;
using TaskFlow.Domain.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.API.Services;

public class TaskService(ITaskRepository repository, IMapper mapper) : ITaskService
{
    public List<TaskDto> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<TaskDto>>(entities);
    }

    public TaskDto? GetById(int id)
    {
        var entity = repository.GetById(id);
        return entity != null ? mapper.Map<TaskDto>(entity) : null;
    }

    public TaskDto Add(TaskDto taskDto)
    {
        var entity = mapper.Map<TaskItem>(taskDto);
        entity.CreatedAt = DateTime.UtcNow;
        repository.Add(entity);
        return mapper.Map<TaskDto>(entity);
    }

    public bool Update(TaskDto taskDto)
    {
        var entity = mapper.Map<TaskItem>(taskDto);
        return repository.Update(entity);
    }

    public bool Delete(int id) => repository.Delete(id);
}