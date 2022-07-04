import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class FriendRepository {

  apiUrl = 'https://localhost:44334/api'

  constructor(private httpClient: HttpClient) { }

  AddFriend(username: string) {
  return this.httpClient.post(this.apiUrl + '/Friend/add-friend', username);
  }

  RemoveFriend(formMsg: any) {
  return this.httpClient.delete(this.apiUrl + '/Friend/remove-friend', formMsg);
  }

  ListFriendsAccepted() {
  return this.httpClient.get(this.apiUrl + '/Friend/list-requests-accepted');
  }

  ListRequestsPendents() {
  return this.httpClient.get(this.apiUrl + '/Friend/list-requests-pendents')
  }

  AcceptRequest(friendId: string) {
  return this.httpClient.post(this.apiUrl + '/Friend/accept-friend-request-by-id','"' + friendId + '"' , { headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    });
  }

  RefuseRequest(friendId: any) {
  return this.httpClient.delete(this.apiUrl + '/Friend/refuse-friend-request-by-id', {body: '"' + friendId + '"', headers: new HttpHeaders({
      'Content-Type': 'application/json',
      })
    });
  }
}
