import { inject, Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { map, Observable, tap } from 'rxjs';

import { environment } from "../utils/environment";
import { RegisterDto } from "../interfaces/dtos/register-dto.interface";
import { LoginDto } from "../interfaces/dtos/login-dto.interface";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);
  private router = inject(Router);

  private readonly loginUrl = environment.httpsUrl + '/login'
  private readonly registerUrl = environment.httpsUrl + '/register'
  private readonly tokenKey = 'jwt';

  login(LoginDto: LoginDto): Observable<string> {
    return this.http.post<{ jwt: string }>(this.loginUrl, LoginDto)
      .pipe(
        map(response => response.jwt),
        tap(jwt => {
          if (jwt) {
            localStorage.setItem(this.tokenKey, jwt);
          } else {
            console.error('Invalid response format');
          }
        })
      );
  }

  register(registerDto: RegisterDto): Observable<any> {
    return this.http.post<any>(this.registerUrl, registerDto);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigateByUrl('/');
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return token !== null;
  }
}