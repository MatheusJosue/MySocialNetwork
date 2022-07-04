import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from 'src/app/domain/models/user.model';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { MessageService } from 'src/app/domain/services/message.service';
import { MessageDTO } from 'src/app/domain/models/dtos/MessageDTO';

@Component({
  selector: 'app-messages-sent',
  templateUrl: './messages-sent.component.html',
  styleUrls: ['./messages-sent.component.css']
})
export class MessagesSentComponent implements OnInit {

  deleteMessageId : number = 0;
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
    this.ListMySendMessages();
  }

  ListMySendMessages(){
    this.messageService.ListMySendMessages().subscribe((data : any) => {
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
}
