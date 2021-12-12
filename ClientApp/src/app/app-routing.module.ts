import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegistrarEstudianteComponent } from 'src/app/registrar-estudiante/registrar-estudiante.component';
import { UserSessionComponent } from './user-session/user-session.component';

const routes: Routes = [
  {path: 'home',component: HomeComponent},
  {path: 'login',component: UserSessionComponent},
  {path: 'registrate',component: RegistrarEstudianteComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })
  ]
})
export class AppRoutingModule { }
