import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, CanLoad, Route } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthGuard implements CanActivate, CanLoad {


    constructor(private router: Router, protected service: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.isLoggedIn();
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }

    canLoad(route: Route): boolean {        
        return this.isLoggedIn();
    }

    private isLoggedIn(url = ''): boolean {
        if (localStorage.getItem('cr_de')) {
            var e_date = new Date(parseInt(localStorage.getItem('ex_kr')));
            var cur_date = new Date();
            if (e_date && cur_date < e_date) {
                return true;                
            }
            else {
                this.service.signout();
            }
        }
        this.router.navigate(['/login']);
        return false;
        //return true;
    }
}