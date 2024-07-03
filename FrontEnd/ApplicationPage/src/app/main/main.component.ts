import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {JwtParserService} from "../jwt-parser.service";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  public fullName: string = "Test";
  public roles : string[] = [];
  public tables = [
    {tn: "Projets", roles:["HR", "PR", "EMP"]},
    {tn: "Employees", roles:["HR", "PR"]},
    {tn: "Leave Requests", roles:["HR", "PR", "EMP"]},
    {tn: "Approval Requests", roles:["HR", "PR"]},
  ]

  constructor(private jwtParser: JwtParserService, private router: Router) { }

  ngOnInit(): void {

    try {
      const decodedToken = this.jwtParser.getDecodedJwtToken();
      if(!decodedToken) {
        this.router.navigate(['/login']);
        return;
      }
      this.fullName = decodedToken.Name;
      this.roles = decodedToken.Roles;

    } catch (error) {
      console.error('Invalid token:', error);
      this.router.navigate(['/login']);
    }

  }

  hasRole(roles: string[]): boolean {
    return roles.some(role => this.jwtParser.hasRole(role));
  }


}
