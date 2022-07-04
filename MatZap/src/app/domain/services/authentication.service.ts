import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { LoginDTO } from '../models/dtos/LoginDTO';
import { signupDTO } from '../models/dtos/SignupDTO';
import { AuthenticationRepository } from 'src/app/data/repositories/authentication.repository';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  private accessTokenSubject: BehaviorSubject<string>;
  public accessToken: Observable<User>;

  constructor(
    private http: HttpClient,
    private authenticationRepository: AuthenticationRepository
  )

  {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')!));
    this.currentUser = this.currentUserSubject.asObservable();

    this.accessTokenSubject = new BehaviorSubject<string>(JSON.parse(localStorage.getItem('accessToken')!));
    this.accessToken = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  public get accessTokenValue(): string {
    return this.accessTokenSubject.value;
  }

  login(user: LoginDTO) {
    return this.authenticationRepository.login(user)
    .pipe(map(data => {
      localStorage.setItem('accessToken', JSON.stringify(data.access_token));
      this.accessTokenSubject.next(data.access_token);
      this.currentUserSubject.next(data.me);
      localStorage.setItem('currentUser', JSON.stringify(data.me));
      return data;
    }))
  }

  register(user: signupDTO) {
    return this.authenticationRepository.register(user)
  }

  logout() {
    this.authenticationRepository.logout();
    this.currentUserSubject.next(null!);
  }

  getCurrentUser() {
    return this.authenticationRepository.getCurrentUser();
  }
}
