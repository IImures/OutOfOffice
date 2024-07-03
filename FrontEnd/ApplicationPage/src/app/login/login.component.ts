import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {AuthService, Login} from "../auth.service";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {}

  onLogin() {
    const login: Login = {
      login: this.username,
      password: this.password,
    }
    this.authService.login(login).subscribe({
      next: (result) => {
        this.authService.saveData(result);
        this.router.navigate(['/main']);
      },
      error: (error) => {
        this.errorMessage = 'Login failed: ' + (error.error?.message || 'Unknown error');
      }
    });
  }

}
