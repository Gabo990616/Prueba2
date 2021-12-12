import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/User';
import { AuthenticationService } from 'src/app/service/authentication.service';

@Component({
  selector: 'app-user-session',
  templateUrl: './user-session.component.html'
})
export class UserSessionComponent implements OnInit {
  user: Login;
  constructor(private servicio: AuthenticationService, private router: Router) {

  }

  ngOnInit() {
    this.user = new Login();
  }
  login() {
    this.servicio.login(this.user.user, this.user.password).subscribe(x => {
      if (x != null) {
        alert("Bienvenido " + x.nombre)
        this.router.navigate(['home']);
      }
    })
  }

}
