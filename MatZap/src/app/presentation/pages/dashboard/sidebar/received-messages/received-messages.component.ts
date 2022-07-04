import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from 'src/app/domain/models/user.model';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { MessageService } from 'src/app/domain/services/message.service';
import { MessageDTO } from 'src/app/domain/models/dtos/MessageDTO';

@Component({
  selector: 'app-received-messages',
  templateUrl: './received-messages.component.html',
  styleUrls: ['./received-messages.component.css']
})
export class ReceivedMessagesComponent implements OnInit {

  deleteMessageId : number = 0;
  readMessageId: any = "";
  currentUser: User;
  users: User[] = [];
  messages: MessageDTO[] = [];

  constructor(
    private messageService: MessageService,
    private authenticationService: AuthenticationService,
    private modalService: NgbModal,
  )
  {
    this.currentUser = authenticationService.currentUserValue
  }

  ngOnInit(): void {
    this.ListMyReceivedMessages();
  }

  ListMyReceivedMessages(){
    this.messageService.ListMyReceivedMessages().subscribe((data : any) => {
      this.messages = data;
    })
  }

  open(content: any, id: any) {
    this.modalService.open(content);
    this.deleteMessageId = id;
  }

  deleteMessage(){
    this.messageService.DeleteMessage(this.deleteMessageId).subscribe((data: any) =>{
      window.location.reload();
    })
  }

  readMessage(){
    this.messageService.ReadMessage(this.readMessageId).subscribe((data: any) =>{
      window.location.reload();
    })
  }
}
