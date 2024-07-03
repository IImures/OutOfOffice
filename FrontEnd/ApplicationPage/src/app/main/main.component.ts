import {Component, OnInit} from '@angular/core';
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {JwtParserService} from "../jwt-parser.service";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    RouterOutlet,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  public fullName: string = "Test";
  public roles : string[] = [];
  public tables = [
    {tn: "Projects", roles:["HR", "PM", "EMP"], href:"projects"},
    {tn: "Employees", roles:["HR", "PM"], href:"employees"},
    {tn: "Leave Requests", roles:["HR", "PM", "EMP"], href:"leave-requests"},
    {tn: "Approval Requests", roles:["HR", "PM"], href:"approval-requests"},
  ]

  constructor(private jwtParser: JwtParserService, private router: Router) { }

  ngOnInit(): void {
    try {
      const decodedToken = this.jwtParser.getDecodedJwtToken();
      if(!decodedToken) {
        this.router.navigate(['/login']);
        return;
      }
      this.fullName = decodedToken.name;
      this.roles = decodedToken.role;

    } catch (error) {
      console.error('Invalid token:', error);
      this.router.navigate(['/login']);
    }
  }

  hasRole(roles: string[]): boolean {
    return roles.some(role => this.jwtParser.hasRole(role));
  }


}
