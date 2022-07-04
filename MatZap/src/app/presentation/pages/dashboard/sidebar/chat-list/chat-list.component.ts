import { Router } from '@angular/router';
import { ChatDTO } from './../../../../../domain/models/dtos/ChatDTO';
import { MessageService } from 'src/app/domain/services/message.service';
import { Component, OnInit } from '@angular/core';
import { friendDTO } from 'src/app/domain/models/dtos/FriendDTO';
import { MessageDTO } from 'src/app/domain/models/dtos/MessageDTO';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css']
})
export class ChatListComponent implements OnInit {

  chats: ChatDTO[] = [];

  constructor(
    private router: Router,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.ListMyChats();
  }

  ListMyChats() {
    return this.messageService.ListMyChats().subscribe((data : any) => {
      this.chats = data;
      }
  )}

  onSubmit(id: string, username: string) {
    this.router.navigate(['dashboard/chat'], {queryParams: {id: id, username: username}})
  }
}
