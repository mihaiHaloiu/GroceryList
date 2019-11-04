import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { environment } from '../../environments/environment';
import * as signalR from "@aspnet/signalr";
import { JwtHelperService } from "@auth0/angular-jwt";
import { DatePipe } from '@angular/common';

@Component({
  selector: 'grocery-list-details',
  templateUrl: './grocery-list-details.component.html'
})
export class GroceryListDetailsComponent {
  public groceryList: GroceryList;
  public messages: Message[]=[];
  private hubConnection: signalR.HubConnection

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute, private router: Router, private datePipe: DatePipe ) {    
    const id = this.activatedRoute.snapshot.paramMap.get("id");    

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44341/chatHub?groceryListID=' + id, { accessTokenFactory: () => localStorage.getItem("jwt") } )
      .build();

    this.hubConnection.on('ReceiveMessage', (user, msg, date) => {
      const message: Message = <Message>({
        id: null,
        user: user,
        text: msg,
        date: date
      });

      this.messages.push(message);
    });

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))

    const headers = {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    };


    this.http.get<GroceryList>(environment.apiUrl + "groceryLists/" + id, headers).subscribe(result => {
      this.groceryList = result;      
    }, error => console.error(error));
  }

  deleteGroceryList(groceryListID, $event) {
    $event.preventDefault();

    const headers = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    };

    this.http.delete(environment.apiUrl + "groceryLists/" + groceryListID, headers).subscribe(result => {

      this.router.navigate(["/grocery-lists"]);

    }, error => console.error(error));
  }

  removeItem(groceryListID, item) {

    const headers = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    };

    this.http.patch(environment.apiUrl + "groceryLists/" + groceryListID + "/removeItem", '"' + item + '"', headers).subscribe(result => {      

      const elemeIndex = this.groceryList.items.indexOf(item);
      this.groceryList.items.splice(elemeIndex, 1);

    }, error => console.error(error));
  }

  addItem(groceryListID, form: NgForm) {
    let formValues = form.value;

    form.reset();

    const headers = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    };

    this.http.patch(environment.apiUrl + "groceryLists/" + groceryListID + "/addItem", '"' + formValues.item + '"', headers).subscribe(result => {
      if (this.groceryList.items == null) {        
        this.groceryList.items=[];
      }
      this.groceryList.items.push(formValues.item);

      

    }, error => console.error(error));
  }

  sendMessage(groceryListID, form: NgForm) {
    let formValues = form.value;
    const token = localStorage.getItem("jwt");

    form.reset();

    const jwtHelper = new JwtHelperService();
    const user = jwtHelper.decodeToken(token).given_name;
    const date = this.datePipe.transform(new Date(), 'yyyy-MM-dd H:mm:s');
    const message: Message = <Message>({
      id:null,
      user: user,
      text: formValues.message,
      date: date
    }); 

    this.messages.push(message);


    this.hubConnection.invoke("SendMessage", user, formValues.message, date).catch(function (err) {
      return console.error(err.toString());
    });

  }
}

interface GroceryList {
  id: string;
  title: string;
  description: number;
  items: string[];
}

interface Message {
  id: string;
  user: string;
  date: string;
  text: string;
}
