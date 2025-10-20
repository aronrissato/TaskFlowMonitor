import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TaskDto } from '../../models/task.dto';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnInit {
  taskForm: FormGroup;
  isEditMode = false;
  taskId?: number;
  loading = false;
  error = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      isCompleted: [false]
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.taskId = parseInt(id, 10);
      this.loadTask(this.taskId);
    }
  }

  loadTask(id: number): void {
    this.loading = true;
    this.taskService.getById(id).subscribe({
      next: (task: TaskDto) => {
        this.taskForm.patchValue({
          title: task.title,
          description: task.description || '',
          isCompleted: task.isCompleted
        });
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading task';
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.taskForm.invalid) {
      this.taskForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = '';
    this.successMessage = '';

    const taskData = this.taskForm.value;

    if (this.isEditMode && this.taskId) {
      this.taskService.update(this.taskId, { ...taskData, id: this.taskId }).subscribe({
        next: () => {
          this.successMessage = 'Task updated successfully!';
          this.loading = false;
          setTimeout(() => {
            this.router.navigate(['/tasks']);
          }, 1500);
        },
        error: (err) => {
          this.error = 'Error updating task';
          this.loading = false;
        }
      });
    } else {
      this.taskService.create(taskData).subscribe({
        next: () => {
          this.successMessage = 'Task created successfully!';
          this.loading = false;
          setTimeout(() => {
            this.router.navigate(['/tasks']);
          }, 1500);
        },
        error: (err) => {
          this.error = 'Error creating task';
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/tasks']);
  }

  get title() {
    return this.taskForm.get('title');
  }

  get description() {
    return this.taskForm.get('description');
  }

  get isCompleted() {
    return this.taskForm.get('isCompleted');
  }
}

