import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ApiService } from "../api.service";
import { Event } from "../app.model";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: Event[];
  context: string;

  constructor(private route: ActivatedRoute, private api: ApiService) {
    this.api = api;
    this.route.paramMap.subscribe(params => {
      this.context = params.get('context');
    })
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
