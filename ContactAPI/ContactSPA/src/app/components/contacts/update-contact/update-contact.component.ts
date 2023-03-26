import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from 'src/app/models/contact.model';
import { ContactsService } from 'src/app/services/contacts.service';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrls: ['./update-contact.component.css']
})

export class UpdateContactComponent {

  contact: Contact = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    phone: 123456789,
    dateOfBirth: new Date,
    password: '',
    category: '',
    subcategory: ''
  }

  errorMessage: any

  constructor(private route: ActivatedRoute, private contactsService: ContactsService,
    private router: Router) {
    this.route.paramMap
      .subscribe({
        next: (params) => {
          const id = params.get('id')

          if (id) {
            this.contactsService
              .getContact(id)
              .subscribe({
                next: (result) => {
                  this.contact = result
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
      })
  }

  updateContact() {
    this.contactsService
      .updateContact(this.contact.id, this.contact)
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

  deleteContact(id: string) {
    this.contactsService
      .deleteContact(id)
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
}
