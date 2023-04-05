import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {JwtHelperService} from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class Authservice {

  constructor(private http:HttpClient, private router : Router) { }

  private url = 'https://localhost:7277/api/Auth';

  currentUserInfo : BehaviorSubject<any> = new BehaviorSubject(null);
  JwtHelperService = new JwtHelperService();

  registerUser(user: Array<any>){
    return this.http.post(this.url+"/CreateUser",
    {
      FullName:user[0],
      MobileNumber:user[1],
      Gender:user[2],
      Email:user[3],
      Password:user[4],
      Role:user[5],
    }
    ,{
      responseType: 'text'
    });
  }

  loginUser(loginInfo: Array<any>){
    return this.http.post(this.url+"/LoginUser",
    {
     
      Email: loginInfo[0],
      Password:loginInfo[1]
    }
    
    ,{
      responseType: 'text'
    });
  }


  setToken(token:string){
    localStorage.setItem("AccessToken",token);
    this.GetCurrentUser();
  }

  GetCurrentUser(){
   var token = localStorage.getItem("AccessToken");
   var userDetails = token !=null ? this.JwtHelperService.decodeToken(token) : null;
   var UserData = userDetails ? {
    id: userDetails.id,
    firstname: userDetails.firstname,
    lastname: userDetails.lastname,
    email : userDetails.email,
    gender: userDetails.gender
   } : null;
   this.currentUserInfo.next(UserData);
  }
  

  IsUserLoggedIn():any{
    return localStorage.getItem("AccessToken") ? true : this.router.navigateByUrl('/login');
  }

  LogOutToken(){
    return localStorage.removeItem("AccessToken");
  }

  // Search(searchString: string):Observable<any[]>{
  //   return this.http.post<any[]>("https://localhost:7277/api/Buyer/Search?searchString="+searchString , {searchString});
  // }

}
