import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "src/app/services/basehttp.service";

@Injectable()
export class AuthService extends BaseService {

    constructor(http: HttpClient) {
        super(http);
    }
    
    public sigin(signinModel): Observable<any> {
        return this.http.post(`${this.configuration.api.endpoint}/account/signin`, signinModel);    
    }

    public signout()
    {
        localStorage.removeItem('cr_de');
        localStorage.removeItem('ex_kr');
        localStorage.removeItem('mnu_iet');
        sessionStorage.clear();
    }
}