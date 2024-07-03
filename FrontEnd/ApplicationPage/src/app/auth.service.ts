import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private api = "http://localhost:5151/api/auth/login";

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  login(login: Login): Observable<any> {
    return this.http.post<Login>(this.api, login);
  }

  public saveData(result : Login) {
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('token'); // remove old tokens if exists
    this.setSession(result)
  }

  private setSession(authResult: any) {
    JSON.stringify(authResult);
    console.log(authResult);
    localStorage.setItem('refreshToken', authResult.refreshToken);
    localStorage.setItem('token', authResult.token);
  }

  logout() {
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

}

export interface Login {
  login: string,
  password: string,
}
