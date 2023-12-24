import { TaskStatus } from "../../../enums/task-status.enum";

export interface UpdateStatusTaskDto {
  id: string;
  status: TaskStatus;
}
