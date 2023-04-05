import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Authservice } from '../../services/authservice';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  
  repeatPass: string = "none";
  displayMsg: string = "";
  isAccountCreated: boolean = false;  

  constructor(private authservice : Authservice ){}

  ngOnInit(): void {
    
  }

  regitserForm = new FormGroup({
    fullName : new FormControl("" , [Validators.required , Validators.minLength(2), Validators.pattern("[a-zA-z].*")]),
    mobNumber : new FormControl("", [Validators.required , Validators.minLength(10), Validators.maxLength(10), Validators.pattern("[0-9]*")]),
    gender : new FormControl("", [Validators.required]),
    email : new FormControl("", [Validators.required ,Validators.email]),
    psd : new FormControl("", [Validators.required]),
    rpsd : new FormControl("", [Validators.required]),
    role : new FormControl("", [Validators.required])
  });

  
  registerSubmited() : void{
    debugger
    if(this.Psd.value == this.Rpsd.value){
      console.log(this.regitserForm.valid);
      this.repeatPass = "none";

      this.authservice.registerUser([
        this.regitserForm.value.fullName,
        this.regitserForm.value.mobNumber,
        this.regitserForm.value.gender,
        this.regitserForm.value.email,
        this.regitserForm.value.psd,
        this.regitserForm.value.role,
       ]).subscribe(res => {
        if(res == "Success"){
          this.displayMsg = "Account Created Successfully!";
          this.isAccountCreated = true;
        }else if (res == "AlreadyExist"){
          this.displayMsg = "Account Already Exist. Try another Email.";
          this.isAccountCreated = false;
        }else{
          this.displayMsg = "Something went wrong.";
          this.isAccountCreated = false;
        }
      })
    }else{
      this.repeatPass = "inline"
    }
  }

  get FullName(): FormControl{
    return this.regitserForm.get("fullName") as FormControl;
  }
  get MobNumber(): FormControl{
    return this.regitserForm.get("mobNumber") as FormControl;
  }
  get Gender(): FormControl{
    return this.regitserForm.get("gender") as FormControl;
  }
  get Email(): FormControl{
    return this.regitserForm.get("email") as FormControl;
  }
  get Psd(): FormControl{
    return this.regitserForm.get("psd") as FormControl;
  }
  get Rpsd(): FormControl{
    return this.regitserForm.get("rpsd") as FormControl;
  }
  get Role(): FormControl{
    return this.regitserForm.get("role") as FormControl;
  }
}
