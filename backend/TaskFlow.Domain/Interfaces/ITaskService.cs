using TaskFlow.Domain.DTOs;

namespace TaskFlow.Domain.Interfaces;

public interface ITaskService
{
    public List<TaskDto> GetAll();
    public TaskDto? GetById(int id);
    public TaskDto Add(TaskDto taskDto);
    public bool Update(TaskDto taskDto);
    public bool Delete(int id);
}