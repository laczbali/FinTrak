import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import {
  trigger,
  state,
  style,
  animate,
  transition
} from '@angular/animations';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [
    trigger('openClose', [
      state('open', style({
        top: 0,
        opacity: '100%'
      })),
      state('closed', style({
        top: '-1.5rem',
        opacity: '0%'
      })),
      transition('* => *', [
        animate('100ms')
      ])
    ])
  ]
})
export class LoginComponent implements OnInit {

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit() {
  }

  /** Password to send to the backend */
  public password: string = '';
  /** Set to false when login attempt fails */
  public loginOk = true;
  /** Set to true while a query is running */
  public queryRunning = false;

  async login(event: KeyboardEvent) {
    if(event.key !== 'Enter') return;

    this.queryRunning = true;
    this.loginOk = true;
    this.loginOk = await firstValueFrom(this.auth.login(this.password));
    this.queryRunning = false;

    if(this.loginOk) {
      this.router.navigate(['/']);
    }
  }

}
