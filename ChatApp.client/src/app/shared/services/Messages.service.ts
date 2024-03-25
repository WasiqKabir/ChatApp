import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../app.constants';
import { Observable, Subject, map } from 'rxjs';
import { Notification } from '../models/Notification.model';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  rootURL : string = Constants.Host

  public UserChangedEvent$ : Subject<boolean> = new Subject<boolean>()
  public messageDivEvent$ : Subject<boolean> = new Subject<boolean>()
  
constructor(private http: HttpClient) { }

public getMessages(senderId: string, receiverId: string): Observable<Notification[]> {
  let url = `${this.rootURL}api/Communication/get-messages?senderId=${senderId}&receiverId=${receiverId}`
  return this.http.get<Notification[]>(url)
  .pipe(map((response : Notification[]) => response as Notification[]))
}

}
