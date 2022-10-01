import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RootComponent } from './components/root/root.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { StatsComponent } from './components/root/stats/stats.component';
import { TransactionsComponent } from './components/root/transactions/transactions.component';
import { ReportsComponent } from './components/root/reports/reports.component';
import { GraphsComponent } from './components/root/graphs/graphs.component';

const routes: Routes = [
  {
    path: '', component: RootComponent, canActivate: [AuthGuard], children: [
      { path: 'stats', component: StatsComponent },
      { path: 'transactions', component: TransactionsComponent },
      { path: 'reports', component: ReportsComponent },
      { path: 'graphs', component: GraphsComponent },
      { path: '', redirectTo: 'stats', pathMatch: 'full' }
    ]
  },

  { path: 'login', component: LoginComponent },

  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
