import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateEventComponent } from './create-event/create-event.component';
import { EventsComponent } from './events/events.component';
import { InvitationsComponent } from './invitations/invitations.component';

const routes: Routes = [
  { path: 'events',        component: EventsComponent },
  { path: 'events/:context',        component: EventsComponent },
  { path: 'events/new', component: CreateEventComponent },
  { path: 'invitations/user',        component: InvitationsComponent },
  { path: '',   redirectTo: '/events', pathMatch: 'full' },
  /* TODO generic pages, 404 etc. 
  { path: '**', component: PageNotFoundComponent }
  */
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
