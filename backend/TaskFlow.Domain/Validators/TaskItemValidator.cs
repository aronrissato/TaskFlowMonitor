using FluentValidation;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Validators;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
    public TaskItemValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");
        
        RuleFor(t => t.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(t => t.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Created at must be less than or equal to current date");
    }

}