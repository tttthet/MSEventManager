import { Component, OnInit } from '@angular/core';

import { ApiService } from "../api.service";
import { Event } from "../app.model";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: Event[];
  api: ApiService;

  constructor(api: ApiService) {
    this.api = api;
  }

  ngOnInit() {
    this.api
      .getEvents()
      .subscribe(events => {
        console.log('app.component onInit events:', events);

	this.events = events;
      });
  }
}
