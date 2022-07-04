import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from 'src/app/domain/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class UserRepository {

  constructor(
  private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>(`${environment.apiUrl}/users`);
  }

}
