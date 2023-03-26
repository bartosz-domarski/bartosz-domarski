import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserRegister, UserLogin } from '../models/user.model';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseApiUrl: string = environment.baseApiUrl

  constructor(private http: HttpClient, private storageService: StorageService) { }

  registerUser(createUserRequest: UserRegister): Observable<string> {
    return this.http.post<string>(this.baseApiUrl + '/account/register', createUserRequest)
  }

  loginUser(loginUserRequest: UserLogin): Observable<string> {
    return this.http.post<string>(this.baseApiUrl + '/account/login', loginUserRequest)
      .pipe(
        tap((result: any) => {
          this.storageService.saveJWT(result.token)
        })
      )
  }
}
