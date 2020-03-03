import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
//import { RouterModule, Routes }  from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
//import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { CreateEventComponent } from './create-event/create-event.component';
import { AppComponent } from './app.component';
import { EventsComponent } from './events/events.component';

/*const appRoutes: Routes = [
  { path: 'event/new', component: CreateEventComponent },
  { path: 'events',        component: AppComponent },
  { path: '',   redirectTo: '/events', pathMatch: 'full' },
//  { path: '**', component: PageNotFoundComponent }
];*/

@NgModule({
  declarations: [
    AppComponent,
    CreateEventComponent,
    EventsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
