import { AuthGuard } from './guards/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { GroceryListsComponent } from './grocery-lists/grocery-lists.component';
import { GroceryListDetailsComponent } from './grocery-list-details/grocery-list-details.component';
import { GroceryListInsertComponent } from './grocery-list-insert/grocery-list-insert.component';
import { GroceryListImportComponent } from './grocery-list-import/grocery-list-import.componet';
import { LoginComponent } from './login/login.component';

import { JwtHelperService } from "@auth0/angular-jwt";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    GroceryListsComponent,
    GroceryListDetailsComponent,
    GroceryListInsertComponent,
    GroceryListImportComponent,

    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'grocery-lists', component: GroceryListsComponent, canActivate: [AuthGuard] },
      { path: 'grocery-list-insert', component: GroceryListInsertComponent, canActivate: [AuthGuard] },
      { path: 'grocery-list-details/:id', component: GroceryListDetailsComponent, canActivate: [AuthGuard] },
      { path: 'grocery-list-import', component: GroceryListImportComponent, canActivate: [AuthGuard] },
    ])
  ],
  providers: [JwtHelperService, AuthGuard, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
