import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageRepository } from 'src/app/data/repositories/message.repository';


@Injectable({
  providedIn: 'root'
})
export class MessageService {

  apiUrl = 'https://localhost:44334/api'

  constructor(
    private httpClient: HttpClient,
    private messageRepository: MessageRepository
  ) { }

  SendMessage(formMsg: any) {
    return this.messageRepository.SendMessage(formMsg);
  }

  ListMySendMessages() {
    return this.messageRepository.ListMySendMessages();
  }

  ListMyReceivedMessages() {
    return this.messageRepository.ListMyReceivedMessages();
  }

  ListAllMessagesBetweenCurrentUserAndUserId(userId: any) {
    return this.messageRepository.ListAllMessagesBetweenCurrentUserAndUserId(userId);
  }

  ListMyChats() {
    return this.messageRepository.ListMyChats();
  }

  GetMessage(id: number) {
    return this.messageRepository.GetMessage(id);
  }

  ReadMessage(id: number) {
    return this.messageRepository.ReadMessage(id);
  }

  DeleteMessage(id: number) {
    return this.messageRepository.DeleteMessage(id);
  }
}
