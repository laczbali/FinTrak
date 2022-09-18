import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private auth: AuthService) { }

  ngOnInit() {
  }

  public password: string = '';

  async login(event: KeyboardEvent) {
    if(event.key !== 'Enter') return;
    var loginOk = await firstValueFrom(this.auth.login(this.password));
    console.log(`Login - ${loginOk}`);
  }

}
