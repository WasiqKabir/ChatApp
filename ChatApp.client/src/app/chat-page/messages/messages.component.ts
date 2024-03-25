import { AfterViewInit, Component, ElementRef, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Notification } from '../../shared/models/Notification.model';
import { UserViewModel } from 'src/app/shared/models/ViewModels/UserViewModel';
import { SignalRService } from 'src/app/shared/services/signalR.service';
import { UserService } from 'src/app/shared/services/User.service';
import { MessagesService } from 'src/app/shared/services/Messages.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit, OnDestroy, AfterViewInit {

  selectedUserId: string = ''
  Messages: Notification[] = []
  newMessage: string = ''

  @Input() user: UserViewModel = new UserViewModel()

  @Input() selectedUserMessages: Notification[] = []
  @Input() userId: string = ''

  @ViewChild('messageContainer') messageContainer!: ElementRef

  messageDivSubscription: Subscription = new Subscription()

  isAdmin : boolean = false
  constructor(private messageService: MessagesService, private userService: UserService) {
    this.getLoggedInUser()
   }

  ngOnInit() {
    
  }

  // ngOnChanges(changes: SimpleChanges): void {
  //     console.log(changes)
  //     this.scrollToBottom()
  // }

  ngAfterViewInit(): void {
    this.messageDivSubscription = this.messageService.messageDivEvent$.subscribe((response: boolean) => {
      setTimeout(() => {
        this.scrollToBottom()
      }, 0);
    });
  }


  ngOnDestroy() {
      this.messageDivSubscription.unsubscribe()
  }


 getLoggedInUser() {
  let user = this.userService.getLoggedInUser()
  if(user?.IsAdmin) {
    this.isAdmin = true
  } else {
    this.isAdmin = false
  }
 }

 scrollToBottom(): void {
  try {
    this.messageContainer.nativeElement.scrollTop = this.messageContainer.nativeElement.scrollHeight
  } catch (err) {
    console.error("Error scrolling to bottom:", err)
  }
}



}
