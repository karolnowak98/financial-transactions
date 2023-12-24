import { TaskPriority } from "../../../enums/task-priority.enum";
import { TaskStatus } from "../../../enums/task-status.enum";
import { CategoryDto } from "../category-dto.interface";

export interface GetTaskDto {
  id: string;
  title: string;
  description: string;
  deadline: Date;
  priority: TaskPriority;
  status: TaskStatus;
  categories: CategoryDto[];
}
