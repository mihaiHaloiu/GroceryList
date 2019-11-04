import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'grocery-lists',
  templateUrl: './grocery-lists.component.html'
})
export class GroceryListsComponent {
  public groceryItems: GroceryItem[];

  

  constructor(http: HttpClient) {

    const headers = {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    };

    http.get<GroceryItem[]>(environment.apiUrl + "groceryLists", headers).subscribe(result => {
      this.groceryItems = result;
      
    }, error => console.error(error));
  }
}

interface GroceryItem {
  id: string;
  title: string;
  description: number;  
}
