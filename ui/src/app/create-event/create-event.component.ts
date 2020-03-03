import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {

  api: ApiService;
  router: Router;
  users: any[];
  eventForm = new FormGroup({
    title: new FormControl(''),
    attendees: new FormControl(''),
    datetime: new FormControl('')
  });

  constructor(api: ApiService, router: Router) {
    this.api = api;
    this.router = router;
  }

  ngOnInit() {
    this.api.getFaculty().subscribe(users => this.users = users);
  }

  onSubmit() {
    this.api.createEvent(this.eventForm.value).subscribe(event => {
      // display loader
      // route to event list w/ message
      this.router.navigate(['/events']);
    });
  }
}
