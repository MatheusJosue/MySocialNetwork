import { MessageDTO } from './MessageDTO';

export class friendDTO {
  id: string = "";
  applicationUserId: string = "";
  friendId: string = "";
  friendUsername: string = "";
  status: number = 0;
  message : MessageDTO[] = [];
}
