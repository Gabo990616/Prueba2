import { Component, OnInit } from '@angular/core';
import { User } from '../models/User';
import { UsuariosService } from '../service/Usuarios.service';


@Component({
  selector: 'app-registrar-estudiante',
  templateUrl: './registrar-estudiante.component.html',
  styleUrls: ['./registrar-estudiante.component.css']
})
export class RegistrarEstudianteComponent implements OnInit {
  usuario:User;
  constructor(private userService: UsuariosService) { 
    this.usuario = new User();
  }

  ngOnInit() {
  
  }

  guarda() {
   this.userService.post(this.usuario).subscribe(x => {
    if (x != null) {
      alert("Guardado exitoso");
    }
   })
  }

}
