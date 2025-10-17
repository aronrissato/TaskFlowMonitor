export interface TaskDto {
  id: number;
  title: string;
  description?: string | null;
  isCompleted: boolean;
}