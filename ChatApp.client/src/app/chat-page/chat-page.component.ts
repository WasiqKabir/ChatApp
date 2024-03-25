import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserViewModel } from '../shared/models/ViewModels/UserViewModel';
import { UserService } from '../shared/services/User.service';
import { Subscription } from 'rxjs';
import { SignalRService } from '../shared/services/signalR.service';
import { Notification } from '../shared/models/Notification.model';
import { MessagesComponent } from './messages/messages.component';
import { MessagesService } from '../shared/services/Messages.service';

@Component({
  selector: 'app-chat-page',
  templateUrl: './chat-page.component.html',
  styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit, OnDestroy {


  userName!: string

  user: UserViewModel = new UserViewModel()
  allUsers: UserViewModel[] = []
  messages: Notification[] = [] 
  private messageSubscription!: Subscription



  selectedUserId: string = ''
  selectedUserMessages: Notification[] = [] 

  newMessage: string = ''
  @ViewChild(MessagesComponent) messagesComponent!: MessagesComponent;


  constructor(private router: Router, private userService: UserService, 
    private signalService: SignalRService, 
    private messageService: MessagesService) {
    this.userService.hideLoginEvent$.next('show')
   }

  ngOnInit() {
   
    this.alreadyLoggedIn()
    if(this.user.UserId)
      this.loadOtherUsers()

    if(this.user?.IsAdmin)
      this.router.navigate(['/admin'])
  
    this.recieveMessages()
  }

  ngOnDestroy(){
    this.messageSubscription?.unsubscribe()
  }

  selectFirstUser(): void {
    if (this.allUsers.length > 0) {
      const firstUserId = this.allUsers[0].UserId!
      //this.onSelectUser(firstUserId)
    }
  }


  connectToHub(userId: string): void {
    this.signalService.connectToHub(userId)
  }


  recieveMessages() {
    this.messageSubscription = this.signalService.message$.subscribe({
      next: (message: any) => {
        let notification = new Notification()
        notification.Message = message.message
        notification.Receiver = this.user.FirstName!
        notification.SenderId = message.senderId
        notification.ReceiverId = message.receiverId
        notification.Sender = message.sender
        this.messages.push(notification)
        this.messageService.messageDivEvent$.next(true)
        
        // Update the selected user's status
        this.updateUserStatus(notification.SenderId)
      }
    });
  }

  updateUserStatus(userId: string): void {
    const userIndex = this.allUsers.findIndex(user => user.UserId === userId)
    if (userIndex !== -1) {
      const user = this.allUsers[userIndex]
      this.allUsers.splice(userIndex, 1)
      this.allUsers.unshift(user)
     
      this.allUsers[0].Status = 'green'

      this.selectFirstUser()
    }
  }

  sendMessage(){
    let notification = this.mapNotification()

    // this.signalService.sendToAll(notification)
    this.signalService.sendDirectMessage(notification)
    this.messages.push(notification)
    this.messageService.messageDivEvent$.next(true)
    this.newMessage = ''
  }
 
  mapNotification() {
    let notification = new Notification()
    notification.SenderId = this.user.UserId!
    notification.ReceiverId = this.selectedUserId
    notification.Sender = this.user.FirstName!
    notification.Message = this.newMessage

    return notification
  }

  loadOtherUsers(){
    this.userService.getOtherUsers(this.user.UserId!).subscribe({
      next: (response: UserViewModel[]) => {
        this.allUsers = response.map((item: any) => ({
          UserId: item.userId,
          UserName: item.userName,
          Password: item.password,
          IsAdmin: item.isAdmin,
          FirstName: item.firstName,
          LastName: item.lastName,
          Status: ''
        }))
      },
      error: (error) => {
        console.log(error.error)
      }
    })
  }

  
  onSelectUser(userId: string): void {
    this.selectedUserId = userId
    this.selectedUserMessages = this.messages.filter(msg => msg.ReceiverId === userId)
    this.loadAllMessages(this.user.UserId!, userId)
  }


  alreadyLoggedIn() {
    let user = this.userService.getLoggedInUser()
    if (!user) {
      this.router.navigate(['/login'])
    } 
    else if(this.user.IsAdmin) {
      this.router.navigate(['/admin'])
    }
    else {
      this.user = user
      this.userName = user.FirstName + " " + user.LastName
      this.connectToHub(user.UserId!)
    }
  }

  loadAllMessages(senderId: string, receiverId: string){
    this.messageService.getMessages(senderId, receiverId)
    .subscribe({
      next: (response: Notification[]) => {
        this.messages = response.map((item: any) => ({
          Message : item.message,
          ReceiverId : item.receiverId,
          SenderId : item.senderId,
          Receiver : '',
          Sender : ''
        }))
        this.messageService.messageDivEvent$.next(true)
      },
      error: (error) => {
        console.log(error.error)
      }
    })
  }


}
