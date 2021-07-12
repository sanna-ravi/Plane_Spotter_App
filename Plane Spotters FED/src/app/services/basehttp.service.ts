import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


export abstract class BaseService {
    protected readonly configuration: any = environment;

    constructor(protected http: HttpClient) {}
}
