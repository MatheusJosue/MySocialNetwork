import { Component, OnInit } from '@angular/core';
import { FriendService } from 'src/app/domain/services/friend.service';
import { friendDTO } from 'src/app/domain/models/dtos/FriendDTO';
import { Router } from '@angular/router';
import { ChatDTO } from 'src/app/domain/models/dtos/ChatDTO';

@Component({
  selector: 'app-list-friends',
  templateUrl: './list-friends.component.html',
  styleUrls: ['./list-friends.component.css']
})
export class ListFriendsComponent implements OnInit {

  friends?: friendDTO[];

  constructor(
    private friendService: FriendService,
    private router: Router
  ){ }

  ngOnInit(): void {
    this.ListFriendsAccepted();
  }

  ListFriendsAccepted(){
    this.friendService.ListFriendsAccepted().subscribe((data : any) => {
      this.friends = data;
    })
  }

}
