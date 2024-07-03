import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';
import {JwtParserService} from "./jwt-parser.service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private jwtParser: JwtParserService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRoles = route.data['roles'] as string[];
    const hasRole = expectedRoles.some(role => this.jwtParser.hasRole(role));

    if (!hasRole) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
