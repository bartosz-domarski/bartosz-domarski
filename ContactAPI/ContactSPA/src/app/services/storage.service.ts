import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() { }

  clean(): void {
    window.sessionStorage.clear();
  }

  public saveJWT(token: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, token);
  }

  public getJWT(): string {
    const token = window.sessionStorage.getItem(USER_KEY);
    if (token) {
      return token;
    }

    return '';
  }

  public isLoggedIn(): boolean {
    const token = window.sessionStorage.getItem(USER_KEY);
    if (token) {
      return true;
    }

    return false;
  }

  public logout(): void {
    window.sessionStorage.removeItem(USER_KEY);
  }

  public headers(): HttpHeaders {
    const headers = new HttpHeaders()
      .set('Authorization', 'Bearer ' + this.getJWT())

    return headers;
  }
}