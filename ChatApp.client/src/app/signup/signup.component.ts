import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../shared/services/User.service';
import { UserViewModel } from '../shared/models/ViewModels/UserViewModel';
import { Router } from '@angular/router';
import { SignupBindingModel } from '../shared/models/BindingModels/signupBindingModel';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signupform!: FormGroup
  
  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router) {
    this.userService.hideLoginEvent$.next('hide')
   }

  ngOnInit() {
    this.innitForm()
    this.alreadyLoggedIn()
    
  }

  alreadyLoggedIn() {
    let user = JSON.parse(localStorage.getItem('user')!)

    if(user && user.IsAdmin) {
      this.router.navigate(['/admin'])
    }
    if (user) {
      this.router.navigate(['/chat'])
    }
  }
  
  onSubmit() {
    let model = new SignupBindingModel()
    model.UserName = this.getUserName
    model.Password = this.getPassword
    model.FirstName = this.getFirstname
    model.LastName = this.getLastname
    model.IsAdmin = this.signUpAsAdmin

    this.userService.register(model).subscribe({
      next: (response: UserViewModel) => {
        //localStorage.setItem('user', JSON.stringify(response))
        this.router.navigate(['/login']);
      }
    })
    
  }

  innitForm() {
    this.signupform = this.formBuilder.group({
      username: [null, Validators.required],
      password: [null, Validators.required],
      firstname: [null],
      lastname: [null],
      signupAsAdmin: [false]
    });
  }

  get getUserName() {
    return this.signupform.get('username')?.value
  }
  get getPassword() {
    return this.signupform.get('password')?.value
  }
  get getFirstname() {
    return this.signupform.get('firstname')?.value
  }
  get getLastname() {
    return this.signupform.get('lastname')?.value
  }

  get signUpAsAdmin() {
    return this.signupform.get('signupAsAdmin')?.value
  }


  isInValid() {
    if(this.signupform.valid) 
      return false
    
    return true
  }

  

}
