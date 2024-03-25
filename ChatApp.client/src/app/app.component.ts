import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './shared/services/User.service';
import { Subscription } from 'rxjs';
import { SignalRService } from './shared/services/signalR.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ChatApp'
  showButton: boolean = true
  userName? : string | null

  loginSubscription : Subscription = new Subscription()
  loginButtonSubscription : Subscription = new Subscription()

  constructor(private router: Router, private userService: UserService, private signalService: SignalRService) { }

  ngOnInit(): void {
    this.loginButtonSubscription = this.userService.hideLoginEvent$.subscribe((response: string) => {
      if(response == 'show') {
        this.showButton = true
      }
      if(response == 'hide') {
        this.showButton = false
      }
    })

    this.getLoggedInUser()
    this.loginSubscription = this.userService.loggedInEvent$.subscribe((response: boolean) => {
      if(response) {
        this.getLoggedInUser()
      }
    })
  }

  getLoggedInUser() {
    let user = this.userService.getLoggedInUser()
    if(user) {
      this.userName = user.FirstName + " " + user.LastName
      if(this.userName == ' ')
        this.userName = user.UserName!
    }
  }

  logOut(){
    this.signalService.closeConnection()
    localStorage.removeItem('user')
    this.router.navigate(['/login'])
    this.userName = null
  }
  
}
