import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserViewModel } from '../shared/models/ViewModels/UserViewModel';
import { UserService } from '../shared/services/User.service';
import { Router } from '@angular/router';
import { Notification } from '../shared/models/Notification.model';
import { MessagesService } from '../shared/services/Messages.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {

  
  userName!: string
  selectedUserId: string = ''
  selectedChatterId: string = ''
  allUsers: UserViewModel[] = []
  user: UserViewModel = new UserViewModel()
  chattedUsers: UserViewModel[] = []
  messages: Notification[] = [] 

  userChangeSubscription : Subscription = new Subscription()

  constructor(private userService: UserService, private router: Router, private messageService: MessagesService) {
    this.userService.hideLoginEvent$.next('show')
   }

  ngOnInit() {
    
    this.userChangeSubscription = this.messageService.UserChangedEvent$.subscribe((response: boolean) => {
      if(response)
        this.messages = []
    })
    this.alreadyLoggedIn()
    this.loadAllUsers()
  }

  ngOnDestroy() {
      this.userChangeSubscription.unsubscribe()
  }

  alreadyLoggedIn() {
    let user = this.userService.getLoggedInUser()
    if(!user) {
      this.router.navigate(['/login'])
    }
    if (user?.IsAdmin == false) {
      this.router.navigate(['/chat'])
    } 
    this.userName = user?.FirstName + " " + user?.LastName
  }

  onSelectUser(userId: string): void {
    this.selectedUserId = userId
    this.loadChatters(userId)
  }

  loadChatters(userId: string) {
    this.userService.getChattedUsers(userId).subscribe({
      next: (response: UserViewModel[]) => {
            this.chattedUsers = response.map((item: any) => ({
              UserId: item.userId,
              UserName: item.userName,
              Password: item.password,
              IsAdmin: item.isAdmin,
              FirstName: item.firstName,
              LastName: item.lastName,
              Status: ''
            }))
            this.messageService.UserChangedEvent$.next(true)
            this.selectedChatterId = ''
          },
          error: (error) => {
            console.log(error.error)
          }
    })
  }

  onSelectChatter(chatterId: string) {
    this.selectedChatterId = chatterId
    this.loadMessages(this.selectedUserId, this.selectedChatterId)
  }

  loadAllUsers(){
    this.userService.getAllUsers().subscribe({
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

    // this.userService.getOtherUsers(this.user.UserId!).subscribe({
    //   next: (response: UserViewModel[]) => {
    //     this.allUsers = response.map((item: any) => ({
    //       UserId: item.userId,
    //       UserName: item.username,
    //       Password: item.password,
    //       IsAdmin: item.isAdmin,
    //       FirstName: item.firstName,
    //       LastName: item.lastName,
    //       Status: ''
    //     }))
    //   },
    //   error: (error) => {
    //     console.log(error.error)
    //   }
    // })

  }

  loadMessages(senderId: string, receiverId: string){
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
