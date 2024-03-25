import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChatPageComponent } from './chat-page/chat-page.component';
import { AppRoutingRoutes } from './app-routing.routing';
import { HttpClientModule } from '@angular/common/http';
import { MessagesComponent } from './chat-page/messages/messages.component';
import { AdminComponent } from './admin/admin.component';


@NgModule({
  declarations: [				
      AppComponent,
      LoginComponent,
      SignupComponent,
      ChatPageComponent,
      MessagesComponent,
      AdminComponent
   ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingRoutes,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
