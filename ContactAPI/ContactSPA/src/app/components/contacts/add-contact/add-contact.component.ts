import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Contact } from 'src/app/models/contact.model';
import { ContactsService } from 'src/app/services/contacts.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent {
  category: string = 'Private';
  subcategory: string = 'Client';
  customCategory: string = '';

  createContactRequest: Contact = {
    id: '0',
    firstName: '',
    lastName: '',
    email: '',
    phone: null,
    dateOfBirth: null,
    password: '',
    category: 'Private',
    subcategory: 'Client'
  }

  errorMessage: any

  constructor(private contactsService: ContactsService, private storageService: StorageService,
    private router: Router) {
    if (!storageService.isLoggedIn()) {
      router.navigate(['users/login'])
    }
  }

  createContact() {
    if (this.createContactRequest.category !== "Business") {
      this.createContactRequest.subcategory = ''
    }

    if (this.createContactRequest.dateOfBirth !== null) {
      const date = new Date(this.createContactRequest.dateOfBirth.toString());
      const timestamp = date.getTime();
      const utcDate = new Date(timestamp);
      this.createContactRequest.dateOfBirth = utcDate
    }

    this.contactsService
      .createContact(this.createContactRequest)
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
      });
  }
}