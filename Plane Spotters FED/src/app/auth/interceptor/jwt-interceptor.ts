import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        let tt_l = JSON.parse(localStorage.getItem('cr_de'));
        if (tt_l && tt_l.cdRe && tt_l.tkd) {
            request = request.clone({
                setHeaders: { 
                    Authorization: `Bearer ${tt_l.cdRe}`
                }
            });
        }

        return next.handle(request);
    }
}