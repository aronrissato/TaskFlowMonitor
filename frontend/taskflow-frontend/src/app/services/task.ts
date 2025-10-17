import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskDto } from '../models/task.dto';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl = '/api/tasks';

  constructor(private http: HttpClient) {}

  getAll(): Observable<TaskDto[]> {
    return this.http.get<TaskDto[]>(this.baseUrl);
  }

  getById(id: number): Observable<TaskDto> {
    return this.http.get<TaskDto>(`${this.baseUrl}/${id}`);
  }

  create(task: Partial<TaskDto>): Observable<TaskDto> {
    return this.http.post<TaskDto>(this.baseUrl, task);
  }

  update(id: number, task: Partial<TaskDto>): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, task);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
