import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactsListComponent } from './components/contacts/contacts-list/contacts-list.component';
import { AddContactComponent } from './components/contacts/add-contact/add-contact.component';
import { AddUserComponent } from './components/users/add-user/add-user.component';
import { LoginComponent } from './components/users/login/login.component';
import { UpdateContactComponent } from './components/contacts/update-contact/update-contact.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactsListComponent,
    AddContactComponent,
    AddUserComponent,
    LoginComponent,
    UpdateContactComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
