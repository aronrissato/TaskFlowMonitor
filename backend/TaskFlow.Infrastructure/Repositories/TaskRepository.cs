using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskRepository(AppDbContext context, IMapper mapper) : ITaskRepository
{
    public List<TaskItem> GetAll() => context.Tasks.ToList();

    public TaskItem? GetById(int id) => context.Tasks
        .AsNoTracking()
        .FirstOrDefault(t => t.Id == id);

    public TaskItem Add(TaskItem item)
    {
        context.Tasks.Add(item);
        context.SaveChanges();
        return item;
    }

    public bool Update(TaskItem item)
    {
        var existing = context.Tasks.Find(item.Id);
        if (existing == null)
            return false;

        mapper.Map(item, existing);
        existing.CompletedAt = existing.IsCompleted ? DateTime.UtcNow : null;
        
        context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var existing = context.Tasks.Find(id);
        if (existing == null)
            return false;

        context.Tasks.Remove(existing);
        context.SaveChanges();
        return true;
    }
}