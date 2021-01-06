import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { AuthenticationService } from "../services/authentication.service";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
      const currentUser = this.authenticationService.currentUserValue;

      if (currentUser) return true;
      
      if(currentUser && currentUser.token) this.router.navigate(["weather"]);
      else this.router.navigate(["login"]);

      return false;
  }
  
}
