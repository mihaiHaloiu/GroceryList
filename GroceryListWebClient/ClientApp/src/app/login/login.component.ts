import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { NgForm } from '@angular/forms';
import { environment } from '../../environments/environment';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  invalidLogin: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);

    const headers = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      })
    };

    this.http.post(environment.apiUrl + "users/authenticate", credentials, headers).subscribe(response => {
      let token = (<any>response).token;
      localStorage.setItem("jwt", token);     

      this.invalidLogin = false;
      this.router.navigate(["/grocery-lists"]);
    }, err => {
      this.invalidLogin = true;
    });
  }

}
