import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TaskDto } from '../../models/task.dto';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
  tasks: TaskDto[] = [];
  loading = false;
  error = '';
  successMessage = '';

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.error = '';
    this.successMessage = '';
    this.taskService.getAll().subscribe({
      next: (data) => {
        this.tasks = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading tasks';
        this.loading = false;
      }
    });
  }

  onDelete(task: TaskDto): void {
    if (!confirm(`Are you sure you want to delete the task "${task.title}"?`)) {
      return;
    }

    this.loading = true;
    this.error = '';
    this.successMessage = '';
    
    this.taskService.delete(task.id).subscribe({
      next: () => {
        this.successMessage = 'Task deleted successfully!';
        this.load();
      },
      error: (err) => {
        this.error = 'Error deleting task';
        this.loading = false;
      }
    });
  }
}
