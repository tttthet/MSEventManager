import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Event, User } from './app.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private http: HttpClient;
  private readonly url = "https://localhost:5001";

  constructor(client: HttpClient) {
    this.http = client;
  }

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.url + "/Api/events");
  }

  getFaculty(): Observable<User[]> {
    return this.http.get<User[]>(this.url + "/Api/users");
  }

  createEvent(event: Event): Observable<Event> {
    const options = {
      headers: new HttpHeaders({
	  'Content-Type':  'application/json'
      })
    };
      
    return this.http.post<Event>(this.url + "/Api/events", JSON.stringify(event), options);
      //catchError(e => this.handleError(e))
  }
	
  private handleError(e) {
    console.error(e);
  }
}
