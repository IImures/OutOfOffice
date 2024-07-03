import {Component, Input, OnInit} from '@angular/core';
import {AuthService} from "../auth.service";
import {Router} from "@angular/router";
import {JwtParserService} from "../jwt-parser.service";
import {jwtDecode} from "jwt-decode";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  @Input() public fullName: string = "Test";

  constructor(private jwtParser: JwtParserService, private router: Router) { }

  ngOnInit(): void {

    try {
      const decodedToken = this.jwtParser.getDecodedJwtToken();
      if(!decodedToken) {
        this.router.navigate(['/login']);
        return;
      }
      this.fullName = decodedToken.Name;


    } catch (error) {
      console.error('Invalid token:', error);
      this.router.navigate(['/login']);
    }

  }


}
