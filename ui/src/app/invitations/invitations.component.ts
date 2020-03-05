import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Invitation } from '../app.model';

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.css']
})
export class InvitationsComponent implements OnInit {
  private api: ApiService;
  public invites: Invitation[];

  constructor(api: ApiService) {
    this.api = api;
  }

  ngOnInit() {
    this.api
      .getInvitesForUser()
      .subscribe(invites => this.invites = invites);
  }

}
