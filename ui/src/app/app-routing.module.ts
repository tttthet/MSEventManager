import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateEventComponent } from './create-event/create-event.component';
import { EventsComponent } from './events/events.component';

const routes: Routes = [
  { path: 'events',        component: EventsComponent },
  { path: 'events/new', component: CreateEventComponent },
  { path: '',   redirectTo: '/events', pathMatch: 'full' },
  //{ path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
