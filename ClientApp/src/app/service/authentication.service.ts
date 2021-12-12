import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { User } from "../models/User";
import { HandleHttpErrorService } from "./handle-http-error.service";

@Injectable({
  providedIn: "root",
})
export class AuthenticationService {
  
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  baseUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private handleErrorService: HandleHttpErrorService,
    private router: Router) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
    this.baseUrl = baseUrl;
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(user, password) {
    return this.http.post<any>(`${this.baseUrl}api/login`, { user, password })
      .pipe(map(user => {
        // store user and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  register(usuario: User){
    return this.http.post<User>(this.baseUrl + 'api/Registro', usuario)
      .pipe(
        tap(() => this.handleErrorService.log('Se envio a guardar')),
        catchError(this.handleErrorService.handleError<User>('Registrar usuario', null))
      );
  }

  logout() {
    // location.reload();
    localStorage.removeItem("currentUser");
    this.currentUserSubject.next(null);
   
    this.router.navigate(["/"]);
  }
}
