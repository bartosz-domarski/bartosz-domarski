import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddContactComponent } from './components/contacts/add-contact/add-contact.component';
import { ContactsListComponent } from './components/contacts/contacts-list/contacts-list.component';
import { UpdateContactComponent } from './components/contacts/update-contact/update-contact.component';
import { AddUserComponent } from './components/users/add-user/add-user.component';
import { LoginComponent } from './components/users/login/login.component';

const routes: Routes = [
  {
    path: '',
    component: ContactsListComponent
  },
  {
    path: 'contacts',
    component: ContactsListComponent
  },
  {
    path: 'contacts/add',
    component: AddContactComponent
  },
  {
    path: 'contacts/update/:id',
    component: UpdateContactComponent
  },
  {
    path: 'users/register',
    component: AddUserComponent
  },
  {
    path: 'users/login',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
