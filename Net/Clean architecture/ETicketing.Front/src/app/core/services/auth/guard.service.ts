import { Injectable } from "@angular/core";
import { SharedService } from "../shared/shared.service";
import { AuthService } from "./auth.service";
import { CanActivate } from "@angular/router";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private shared: SharedService
  ) { }

  canActivate(): boolean {
    if (this.auth.hasValidToken()) {
      return true;
    }

    this.shared.navigate('/login');
    return false;
  }
}


@Injectable({ providedIn: 'root' })
export class ReverseAuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private shared: SharedService
  ) { }

  canActivate(): boolean {
    if (this.auth.hasValidToken()) {
      this.shared.navigate('/home');
      return false;
    }

    return true;
  }
}

