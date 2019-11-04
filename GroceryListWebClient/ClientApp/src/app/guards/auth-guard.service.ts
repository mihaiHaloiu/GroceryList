import { JwtHelperService } from "@auth0/angular-jwt";
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {
  }
  canActivate() {
    const token = localStorage.getItem("jwt");

    const helper = new JwtHelperService();    
    if (token && !helper.isTokenExpired(token)) {      
      return true;
    }    

    this.router.navigate(["login"]);
    return false;    
  }
}
