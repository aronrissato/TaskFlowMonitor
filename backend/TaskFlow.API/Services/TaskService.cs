using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.API.Services;

public class TaskService(ITaskRepository repository) : ITaskService
{
    public List<TaskItem> GetAll() => repository.GetAll();

    public TaskItem? GetById(int id) => repository.GetById(id);

    public TaskItem Add(TaskItem item)
    {
        item.CreatedAt = DateTime.UtcNow;
        repository.Add(item);
        return item;
    }

    public bool Update(TaskItem item) => repository.Update(item);

    public bool Delete(int id) => repository.Delete(id);
}