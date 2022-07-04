import { FriendService } from 'src/app/domain/services/friend.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit {

  formFriend: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private friendService: FriendService,
    private router: Router,
  )
  {
    this.formFriend = this.formBuilder.group({
      username: [null]
    });
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.friendService.AddFriend(this.formFriend.value)
    .subscribe( () => {
    this.router.navigate(['dashboard/sendmessage'])
    })
  }

  onCancel() {
    this.router.navigate(['dashboard/messagessent'])
  }
}
