import { Component, OnInit } from '@angular/core';
import { friendDTO } from 'src/app/domain/models/dtos/FriendDTO';
import { FriendService } from 'src/app/domain/services/friend.service';

@Component({
  selector: 'app-requestspendents',
  templateUrl: './requestspendents.component.html',
  styleUrls: ['./requestspendents.component.css']
})
export class RequestspendentsComponent implements OnInit {

  friends: friendDTO[] = [];

  constructor(
    private friendService: FriendService
  )
  {
  }

  ngOnInit(): void {
    this.ListRequestsPendents();
  }

  ListRequestsPendents() {
    this.friendService.ListRequestsPendents().subscribe((data : any) => {
      this.friends = data;
    })
  }

  AcceptRequest(friendId: string) {
    this.friendService.AcceptRequest(friendId).subscribe((data : any) => {
      this.friends = data;
    })
  }

  RefuseRequest(friendId: any) {
    this.friendService.RefuseRequest(friendId).subscribe((data : any) => {
    window.location.reload();
    })
  }
}
