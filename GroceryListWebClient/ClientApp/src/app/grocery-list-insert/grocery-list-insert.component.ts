import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { environment } from '../../environments/environment';

@Component({
  selector: 'grocery-list-insert',
  templateUrl: './grocery-list-insert.component.html'
})
export class GroceryListInsertComponent {
  constructor(private http: HttpClient, private router: Router ) {

  }

  addGroceryList(form: NgForm) {
    let groceryListValues = JSON.stringify(form.value);  

    const headers = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    };

    this.http.post(environment.apiUrl + "groceryLists", groceryListValues, headers).subscribe(result => {      
      this.router.navigate(["/grocery-lists"]);

    }, error => console.error(error));
  }
}
