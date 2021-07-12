import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { Router } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(protected router: Router, protected service: AuthService) {}

    intercept( request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401 || err.status === 403) {
                this.service.signout();
                this.router.navigate(['login']);
            }
            else if (err.status === 404) {
                this.router.navigate(['not-found']);
            }
            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    }
}