import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Authservice } from 'src/app/auth/services/authservice';
@Component({
  selector: 'app-user-navbar',
  templateUrl: './navbar-user.component.html',
  styleUrls: ['./navbar-user.component.css']
})
export class NavbarUserComponent implements OnInit{
  constructor(private authService : Authservice, private router : Router, private inputRef: ElementRef){
  }
  searchString: string = '';
  searchResults: any[] = [];
  loggedIn = false;

  ngOnInit(): void {
    if((localStorage.getItem("AccessToken") ? true : false) == true){
      this.loggedIn = true;
    }
    
  }

  logout(){
    this.authService.LogOutToken();
    this.loggedIn = false;
    window.location.reload();
  }
  
  loginPage(){
    this.router.navigate(["/login"]);
  }

  viewCart(){
    this.router.navigate(["/view-cart"]);
  }

  // searchProducts(searchString: string) {
  //   this.authService.Search(searchString).subscribe(
  //     (data: any[]) => {
  //       this.searchResults = data;
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }
}
