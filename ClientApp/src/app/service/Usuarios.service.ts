import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from '../models/User';
import { HandleHttpErrorService } from './handle-http-error.service';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  baseUrl: string;
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private handleErrorService: HandleHttpErrorService
  ) {
    this.baseUrl = baseUrl;
  }

  get(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'api/Register').pipe(
      tap(_ => this.handleErrorService.log('mostrados correctamentes')),
      catchError(this.handleErrorService.handleError<User[]>('consultar empleado', null))
    );
  }

  post(user: User): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'api/Register', user).pipe(
      tap(_ => this.handleErrorService.log('datos enviados correctamentes')),
      catchError(this.handleErrorService.handleError<User>('registrar empleado', null))
    );
  }
}
