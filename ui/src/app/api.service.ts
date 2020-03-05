import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Event, User, Invitation } from './app.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private http: HttpClient;
  private readonly url = "https://localhost:5001";

  constructor(client: HttpClient) {
    this.http = client;
  }

  /* TODO rename this getEventsByCoordinator */
  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.url + "/Api/events");
  }

  getEventsByCoordinator(): Observable<Event[]> {
    return this.http.get<Event[]>(this.url + "/Api/events/coordinator");
  }

  getFaculty(): Observable<User[]> {
    return this.http.get<User[]>(this.url + "/Api/users");
  }

  getInvitesForUser(): Observable<Invitation[]> {
    return this.http.get<Invitation[]>(this.url + "/Api/invitations/user");
  }

  createEvent(event: Event): Observable<Event> {
    const options = {
      headers: new HttpHeaders({
	  'Content-Type':  'application/json'
      })
    };
      
    return this.http.post<Event>(this.url + "/Api/events", JSON.stringify(event), options);
      /* TODO catchError(e => this.handleError(e))*/
  }

  createInvitation(invitation: Invitation): Observable<Invitation> {
    const options = {
      headers: new HttpHeaders({
	  'Content-Type':  'application/json'
      })
    };
      
    return this.http.post<Invitation>(this.url + "/Api/events", JSON.stringify(invitation), options);
      /* TODO catchError(e => this.handleError(e))*/
  }
	
  private handleError(e) {
    console.error(e);
  }
}
