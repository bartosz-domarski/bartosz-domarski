import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Contact } from 'src/app/models/contact.model';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})

export class ContactsService {

  baseApiUrl: string = environment.baseApiUrl
  
  constructor(private http: HttpClient, private storageService: StorageService) 
  { 

  }

  getAllContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseApiUrl + '/contact', { headers: this.storageService.headers() })
  }

  createContact(createContactRequest: Contact): Observable<string> {
    return this.http.post<string>(this.baseApiUrl + '/contact', createContactRequest, { headers: this.storageService.headers() })
  }

  getContact(id: string): Observable<Contact> {
    return this.http.get<Contact>(this.baseApiUrl + '/contact/' + id, { headers: this.storageService.headers() })
  }

  updateContact(id: string, updateContactRequest: Contact): Observable<string> {
    return this.http.put<string>(this.baseApiUrl + '/contact/' + id, updateContactRequest, { headers: this.storageService.headers() })
  }

  deleteContact(id: string): Observable<string> {
    return this.http.delete<string>(this.baseApiUrl + '/contact/' + id, { headers: this.storageService.headers() })
  }
}
