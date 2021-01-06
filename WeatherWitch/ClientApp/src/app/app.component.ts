import { Component } from '@angular/core';
import { AuthenticationService } from "../app/services/authentication.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = "Weather Witch";
  currentUser: any;

  constructor(
      private router: Router,
      private authenticationService: AuthenticationService
  ) {
      this.authenticationService.currentUser$.subscribe(user => this.currentUser = user);
  }
}
