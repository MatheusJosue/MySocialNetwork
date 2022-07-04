import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SsoDTO } from 'src/app/domain/models/dtos/SsoDTO';
import { User } from 'src/app/domain/models/user.model';
import { LoginDTO } from 'src/app/domain/models/dtos/LoginDTO';
import { signupDTO } from 'src/app/domain/models/dtos/SignupDTO';

@Injectable({ providedIn: 'root' })
export class AuthenticationRepository {

  constructor(private http: HttpClient) { }

  login(user: LoginDTO): Observable<SsoDTO> {
    return this.http.post<any>(`${environment.apiUrl}/api/auth/sign-in`, user)
    .pipe(
      map((data) => {
      let ssoDTO: SsoDTO = data;
      return ssoDTO;
    }))
  }

  register(user: signupDTO) {
    return this.http.post<User>(`${environment.apiUrl}/api/auth/sign-up`, user)
  }

  logout() {
    localStorage.removeItem('currentUser');
    localStorage.removeItem('accessToken');
  }

  getCurrentUser() {
    return this.http.get<User>(`${environment.apiUrl}/api/auth/get-current-user`)
  }
}
