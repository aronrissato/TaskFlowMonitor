import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list';

export const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },
  { path: 'tasks', component: TaskListComponent }
];
