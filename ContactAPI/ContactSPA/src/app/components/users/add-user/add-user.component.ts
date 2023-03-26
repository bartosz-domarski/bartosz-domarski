import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserRegister } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {

  register: UserRegister = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: ''
  }

  errorMessage: any

  constructor(private usersService: UsersService, private router: Router) {
  }


  registerUser() {
    this.usersService
      .registerUser(this.register)
      .subscribe({
        next: (result) => {
          console.log(result)
          this.router.navigate(['users/login'])
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
}