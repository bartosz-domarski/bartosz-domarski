import { Component } from '@angular/core';
import { Contact } from 'src/app/models/contact.model';
import { ContactsService } from 'src/app/services/contacts.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.css']
})

export class ContactsListComponent {
  contacts: Contact[] = []
  showEmail = true
  showDateOfBirth = true
  showPassword = true

  isLoggedIn() {
    return this.storageService.isLoggedIn()
  }

  constructor(private contactsService: ContactsService, private storageService: StorageService) {
    this.contactsService
      .getAllContacts()
      .subscribe({
        next: (contacts) => {
          this.contacts = contacts
          console.log(contacts)

          if (this.contacts.every(contact => !contact.email)) {
            this.showEmail = false
          }
          if (this.contacts.every(contact => !contact.dateOfBirth)) {
            this.showDateOfBirth = false
          }
          if (this.contacts.every(contact => !contact.password)) {
            this.showPassword = false
          }
        },
        error: (response) => {
          console.log(response)
        }
      });
  }
}
