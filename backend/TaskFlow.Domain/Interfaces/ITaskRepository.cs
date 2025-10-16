using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

public interface ITaskRepository
{
    public List<TaskItem> GetAll();
    public TaskItem? GetById(int id);
    public TaskItem Add(TaskItem item);
    public bool Update(TaskItem item);
    public bool Delete(int id);
}