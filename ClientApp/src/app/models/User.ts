import { Evento } from "./Evento";

export class User {  
    identificacion:string;
    nombre:string;
    user:string;
    password:string;
    rol:string;
    token:string;

    Evento:Evento;
}

export class Login{
    user:String;
    password:string;
}