import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginBindingModel } from '../models/BindingModels/LoginBindingModel';
import { Observable } from 'rxjs/internal/Observable';
import { UserViewModel } from '../models/ViewModels/UserViewModel';
import { map } from 'rxjs/operators';
import { Constants } from '../app.constants';
import { Subject } from 'rxjs';
import { SignupBindingModel } from '../models/BindingModels/signupBindingModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  rootUrl: string = Constants.Host + 'api'

  hideLoginEvent$: Subject<string> = new Subject()
  loggedInEvent$: Subject<boolean> = new Subject()

  constructor(private httpClient: HttpClient) { }

  getLoggedInUser(){
    let user = new UserViewModel()
    let rawuser = JSON.parse(localStorage.getItem('user')!)
    if(!rawuser)
       return null

    user.UserId = rawuser.userId
    user.FirstName = rawuser.firstName
    user.LastName = rawuser.lastName
    user.IsAdmin = rawuser.isAdmin
    user.UserName = rawuser.userName

    return user
  }

  login(request: LoginBindingModel): Observable<UserViewModel> {
    return this.httpClient.post<UserViewModel>(`${this.rootUrl}/login` , request)
      .pipe(map((response: any) => response as UserViewModel))
  }

  register(request: SignupBindingModel) : Observable<UserViewModel> {
    return this.httpClient.post<UserViewModel>(`${this.rootUrl}/register`, request)
      .pipe(map((response: any) => response as UserViewModel))
  }

  getOtherUsers(UserId : string): Observable<UserViewModel[]> {
    return this.httpClient.get<UserViewModel[]>(`${this.rootUrl}/users/other-users/${UserId}`, { responseType: 'json' })
    .pipe(map((response : UserViewModel[]) => response as UserViewModel[]))
  }

  getAllUsers(){
    return this.httpClient.get<UserViewModel[]>(`${this.rootUrl}/users/all`)
    .pipe(map((response : UserViewModel[]) => response as UserViewModel[]))
  }

  getChattedUsers(userId: string) {
    return this.httpClient.get<UserViewModel[]> (`${this.rootUrl}/users/chatted/${userId}`)
    .pipe(map((response : UserViewModel[]) => response as UserViewModel[]))
  }

}
