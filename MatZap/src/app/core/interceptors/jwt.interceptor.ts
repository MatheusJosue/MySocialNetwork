import { environment } from 'src/environments/environment';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService : AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const accessToken = this.authenticationService.accessTokenValue;
    const isLoggedIn = accessToken;
    const isApiUrl = request.url.startsWith(environment.apiUrl);

    if(isLoggedIn && isApiUrl){
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${accessToken}`
      }
      });
    }

    return next.handle(request);
  }
}
