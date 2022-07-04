import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRepository } from 'src/app/data/repositories/user.repository';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient,
    private userRepository: UserRepository) { }

    getAll() {
      return this.userRepository.getAll();
    }
}
