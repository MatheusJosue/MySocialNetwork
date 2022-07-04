import { MessageDTO } from './MessageDTO';

export class ChatDTO {
  memberMe: string = "";
  otherMember: string = "";
  message : MessageDTO[] = [];
  otherMemberId: string = "";
}
