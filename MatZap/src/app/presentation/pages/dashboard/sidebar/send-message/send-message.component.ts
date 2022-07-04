import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { MessageService } from 'src/app/domain/services/message.service';
import { friendDTO } from 'src/app/domain/models/dtos/FriendDTO';
import { FriendService } from 'src/app/domain/services/friend.service';

@Component({
  selector: 'app-send-message',
  templateUrl: './send-message.component.html',
  styleUrls: ['./send-message.component.css']
})
export class SendMessageComponent implements OnInit {

  friends: friendDTO[] = [];
  formMsg: FormGroup;

  constructor(
    private formBuilder : FormBuilder,
    private messageService: MessageService,
    private router: Router,
    private authenticationService: AuthenticationService,
    private friendService: FriendService
  )
  {
    this.formMsg = this.formBuilder.group({
      ReceiverId: [null],
      titulo: [null],
      texto: [null]
    });
  }

  ngOnInit(): void {
    this.ListFriendsAccepted();
  }

  ListFriendsAccepted(){
    this.friendService.ListFriendsAccepted().subscribe((data : any) => {
      this.friends = data;
    })
  }

  onSubmit() {
    this.messageService.SendMessage(this.formMsg.value).subscribe(data => {
      this.router.navigate(['dashboard/chatlist'])
    })
  }

  onCancel() {
    this.router.navigate(['dashboard/messagessent'])
  }

}
