import { friendDTO } from 'src/app/domain/models/dtos/FriendDTO';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { MessageService } from 'src/app/domain/services/message.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/domain/models/user.model';
import { MessageDTO } from 'src/app/domain/models/dtos/MessageDTO';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  friends: friendDTO[] = [];
  readMessageId: string = "";
  currentUser: User;
  users: User[] = [];
  messages: MessageDTO[] = [];
  currentFriendId: string = "";
  currentFriendUsername: string = "";
  formMsg: FormGroup;

  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private formBuilder : FormBuilder,
  )
  {
    this.currentUser = authenticationService.currentUserValue

    this.route.queryParams.subscribe(params => {
      this.currentFriendId = params['id']
      this.currentFriendUsername = params['username']
    })

    this.formMsg = this.formBuilder.group({
      ReceiverId: [this.currentFriendId],
      texto: [null]
    });
  }

  ngOnInit(): void {
    this.ListAllMessages(this.currentFriendId);
  }

  ListAllMessages(userId: any){
    this.messageService.ListAllMessagesBetweenCurrentUserAndUserId(userId).subscribe((data : any) => {
      this.messages = data;
    })
  }

  onSubmit() {
    this.messageService.SendMessage(this.formMsg.value).subscribe(data => {
      window.location.reload();
    })
  }
}
