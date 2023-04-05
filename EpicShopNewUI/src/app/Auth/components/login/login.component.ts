import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Authservice } from '../../services/authservice';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private loginAuth : Authservice, private router : Router) {
   
  }
  LoginRail = new FormGroup({

    email: new FormControl('', [Validators.required]),
    password:new FormControl('', [Validators.required]),
    
  });

  isUserValid:boolean = false;

 
   

  LoginSubmitted(){
    var token = localStorage.getItem("AccessToken");
    this.loginAuth.loginUser([this.LoginRail.value.email,this.LoginRail.value.password]).subscribe(response =>{

      if(response == 'Failure'){
        this.isUserValid = false;
        alert('Login Unsuccessful');
      }
      else {
        this.isUserValid = true;
        this.loginAuth.setToken(response);
        this.router.navigateByUrl('/');
      }

    })
    console.log(this.LoginRail.value)

  }

  get f() {
    return this.LoginRail.controls;
  }
}
