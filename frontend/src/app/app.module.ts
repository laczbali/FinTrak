import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RootComponent } from './components/root/root.component';
import { LoginComponent } from './components/login/login.component';
import { NavComponent } from './components/nav/nav.component';
import { TransactionsComponent } from './components/root/transactions/transactions.component';
import { ReportsComponent } from './components/root/reports/reports.component';
import { GraphsComponent } from './components/root/graphs/graphs.component';
import { StatsComponent } from './components/root/stats/stats.component';

@NgModule({
  declarations: [
    AppComponent,
    RootComponent,
    LoginComponent,
    NavComponent,
    TransactionsComponent,
    ReportsComponent,
    GraphsComponent,
    StatsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
