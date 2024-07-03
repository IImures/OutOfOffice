import { Injectable } from '@angular/core';
import {jwtDecode} from "jwt-decode";
import {Router} from "@angular/router";
import {LocalStorageService} from "./local-storage.service";

@Injectable({
  providedIn: 'root'
})
export class JwtParserService {

  constructor(private storageService: LocalStorageService, private router: Router) { }

  getDecodedJwtToken() : JwtDecode | null {
    try{
      const token = this.storageService.getItem('token');
      if (!token) {
        this.router.navigate(['/login']);
        return null;
      }
      return jwtDecode<JwtDecode>(token);
    }
    catch(err){
      console.log(err);
      return null;
    }
  }

  public hasRole(role: string): boolean {
    const token= this.getDecodedJwtToken();
    if(!token){
      return false;
    }
    return token.Roles.includes(role);
  }

}

export interface JwtDecode {
  Name: string,
  Roles: string[],
  exp: string,
  issuer: string,
  aud: string
}
