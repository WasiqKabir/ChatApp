import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginBindingModel } from '../shared/models/BindingModels/LoginBindingModel';
import { UserService } from '../shared/services/User.service';
import { UserViewModel } from '../shared/models/ViewModels/UserViewModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup
  errorMessage?: string

  constructor(private formBuilder: FormBuilder, private router: Router, private userService: UserService) {
    this.userService.hideLoginEvent$.next('hide')
   }

  ngOnInit() {
    this.innitForm()
    this.alreadyLoggedIn()
  }


  alreadyLoggedIn() {
    let user = JSON.parse(localStorage.getItem('user')!)

    if (user) {
      this.router.navigate(['/chat'])
    }
  }

  innitForm() {
    this.loginForm = this.formBuilder.group({
      username: [null, Validators.required],
      password: [null, Validators.required],
      loginAsAdmin: [false]
    });
  }

  get getUserName() {
    return this.loginForm.get('username')?.value
  }

  get getPassword() {
    return this.loginForm.get('password')?.value
  }

  get isAdminLogin() {
    return this.loginForm.get('loginAsAdmin')?.value;
  }

  onSubmit() {
    
    // Create the user object
    let model = new LoginBindingModel()
    model.UserName = this.getUserName
    model.Password = this.getPassword
    model.IsAdmin = this.isAdminLogin

    this.userService.login(model)
      .subscribe({next:  (response: UserViewModel) => {
        localStorage.setItem('user', JSON.stringify(response))
        this.userService.loggedInEvent$.next(true)
        if(model.IsAdmin)
        {
          this.router.navigate(['/admin'])
        } else 
        {
          this.router.navigate(['/chat'])
        }
      },
      error: (error) => {
        this.displayError(error)
      }
    })
  }


  displayError(error: any){
    this.errorMessage = error.error.Message;
      setTimeout(() => {
        this.errorMessage = ''
      }, 3000)
  }

  isInValid() {
    if(this.loginForm.valid) 
      return false
    
    return true
  }

}
