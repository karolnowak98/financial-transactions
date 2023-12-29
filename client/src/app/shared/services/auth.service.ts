import { inject, Injectable } from '@angular/core';
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
  readonly http = inject(HttpClient);
  readonly router = inject(Router);
  readonly loginUrl = environment.httpsUrl + '/login'
  readonly registerUrl = environment.httpsUrl + '/register'
  readonly tokenKey = 'jwt';
  tokenExpirationTimer: any;

  login(LoginDto: LoginDto): Observable<string> {
    return this.http.post<{ jwt: string }>(this.loginUrl, LoginDto).pipe(
      map((response) => response.jwt),
      tap((jwt) => {
        if (jwt) {
          localStorage.setItem(this.tokenKey, jwt);
          this.setLogoutTimer();
        } else {
          console.error('Invalid response format');
        }
      }),
    );
  }

  register(registerDto: RegisterDto): Observable<any> {
    return this.http.post<any>(this.registerUrl, registerDto);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigateByUrl('/');
    this.clearLogoutTimer();
  }

  getToken(): string | null {return localStorage.getItem(this.tokenKey);}
  isLoggedIn(): boolean {return (this.getToken() != null);}

  private setLogoutTimer() {
    const token = this.getToken();
    if (token) {
      const tokenData = JSON.parse(atob(token.split('.')[1]));
      const expirationTime = tokenData.exp * 1000 - Date.now();

      this.tokenExpirationTimer = setTimeout(() => {
        this.logout();
      }, expirationTime);
    }
  }

  private clearLogoutTimer() {
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
      this.tokenExpirationTimer = null;
    }
  }
}
