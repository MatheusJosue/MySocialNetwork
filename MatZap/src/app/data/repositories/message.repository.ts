import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class MessageRepository {

    apiUrl = 'https://localhost:44334/api'

    constructor(private httpClient: HttpClient) { }

    SendMessage(formMsg: any) {
        return this.httpClient.post(this.apiUrl + '/Message/send-message', formMsg);
    }

    ListMySendMessages() {
        return this.httpClient.get(this.apiUrl + '/Message/list-my-send-messages');
    }

    ListMyReceivedMessages() {
        return this.httpClient.get(this.apiUrl + '/Message/list-my-received-messages');
    }

    ListAllMessagesBetweenCurrentUserAndUserId(userId: any) {
        return this.httpClient.get(this.apiUrl + '/Message/list-all-messages-between-current-user-and-user-id?userId=' + userId);
    }

    ListMyChats() {
        return this.httpClient.get(this.apiUrl + '/Message/list-my-chats');
    }

    GetMessage(id: number) {
        return this.httpClient.get(this.apiUrl + '/Message/get-message?messageId=' + id);
    }

    ReadMessage(id: number) {
        return this.httpClient.put(this.apiUrl + '/Message/read-message', id);
    }

    DeleteMessage(id: number) {
        return this.httpClient.delete(this.apiUrl + '/Message/delete-message?messageId=' + id);
    }
}
