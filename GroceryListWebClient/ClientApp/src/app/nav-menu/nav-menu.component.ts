import { Component } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { CanActivate, Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private router: Router) {
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut($event) {
    $event.preventDefault();    

    localStorage.removeItem("jwt");
    this.router.navigate([""]);
  }

  isUserAuthenticated() {
    const helper = new JwtHelperService();
   
    let token: string = localStorage.getItem("jwt");
    if (token && !helper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
}
