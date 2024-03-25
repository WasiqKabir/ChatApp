import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Notification } from '../models/Notification.model';
import { Constants, SignalRMethods } from '../app.constants';
import { error } from 'console';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: HubConnection
  private messageSubject: BehaviorSubject<Notification> = new BehaviorSubject<Notification>(new Notification())
  public message$ = this.messageSubject.asObservable()

constructor() { }

public connectToHub(userId: string) {
  this.hubConnection = new HubConnectionBuilder()
    .withUrl(`${Constants.ChatHubUrl}?userId=${userId}`) 
    .build()

  this.hubConnection.start()
    .then(() => console.log('SignalR hub connection established successfully'))
    .catch(error => console.error('Error establishing SignalR hub connection:', error))

  // Listen for incoming messages
  this.hubConnection.on(SignalRMethods.ReceiveMessage, (message: any) => {
    this.messageSubject.next(message);
  })
}

public closeConnection() {
  if (this.hubConnection) {
    this.hubConnection.stop()
      .then(() => console.log('SignalR hub connection closed successfully'))
      .catch(error => console.error('Error closing SignalR hub connection:', error));
  }
}

public sendToAll(notification: Notification) {
  this.hubConnection.invoke(SignalRMethods.SendToAll, notification)
  .then(() => console.log('Sent to all successfully'))
  .catch(error => console.log('Error sending to all : ', error))
}

public sendDirectMessage(notification: Notification): void {
  if (this.hubConnection && this.hubConnection.state === 'Connected') {
    this.hubConnection.invoke(SignalRMethods.SendDM, notification )
      .then(() => console.log('Direct message sent successfully'))
      .catch(error => console.error('Error sending direct message:', error))
  } else {
    console.error('SignalR hub connection is not established.')
  }
}
}
