import { Component } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { UserLogin } from 'src/app/models/user.model';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';
import { tap } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {

  login: UserLogin = {
    email: '',
    password: ''
  }
  errorMessage: any

  constructor(private usersService: UsersService, private storageService: StorageService, private router: Router) {
  }

  loginUser() {
    this.usersService
      .loginUser(this.login)
      .subscribe({
        next: (result) => {
          console.log(result)
          this.router.navigate(['contacts'])
        },
        error: (result) => {
          console.log(result)
          if (result.error.errors) {
            this.errorMessage = result.error.errors
          } else {
            this.errorMessage = result.error
          }
        }
      })
  }

  isLoggedIn() {
    return this.storageService.isLoggedIn()
  }

  logoutUser() {
    this.storageService.logout()
    this.router.navigate(['contacts'])
  }
}