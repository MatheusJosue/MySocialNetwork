export class MessageDTO {
  id: number = 0;
  titulo: string = "";
  texto: string = "";
  data: string = "";
  senderUserId: string = "";
  senderUsername: string = "";
  receiverUserId: string = "";
  receiverUsername: string = "";
  isRead: boolean = false;
}
