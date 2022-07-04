import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FriendRepository } from 'src/app/data/repositories/friend.repository';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  apiUrl = 'https://localhost:44334/api'

  constructor(
    private httpClient: HttpClient,
    private friendRepository: FriendRepository
  ) { }


  AddFriend(username: string) {
    return this.friendRepository.AddFriend(username);
  }

  RemoveFriend(formMsg: any) {
    return this.friendRepository.RemoveFriend(formMsg);
  }

  ListFriendsAccepted() {
    return this.friendRepository.ListFriendsAccepted();
  }

  ListRequestsPendents() {
    return this.httpClient.get(this.apiUrl + '/Friend/list-requests-pendents')
  }

  AcceptRequest(friendId: string) {
    return this.friendRepository.AcceptRequest(friendId)
}

  RefuseRequest(friendId: any) {
    return this.friendRepository.RefuseRequest(friendId)
  }

}
