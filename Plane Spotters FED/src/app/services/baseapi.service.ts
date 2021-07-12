import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './basehttp.service';

export class BaseAPIService extends BaseService {

    protected baseEndPoint?:string;

    constructor(http: HttpClient) {
        super(http);
    }

    SetBaseEndPoint(baseEndPoint: string){
        this.baseEndPoint = baseEndPoint
    }

    Get(id:String): Observable<any>{        
        return this.http.get(`${this.configuration.api.endpoint}/${this.baseEndPoint}/${id}`);
    }

    GetAll(): Observable<any> { 
        console.log(this.configuration);
        return this.http.get(`${this.configuration.api.endpoint}/${this.baseEndPoint}`);
    }

    AddorUpdate(BaseModel): Observable<any> {
        if(BaseModel.id != null && BaseModel.id > 0) {
            return this.http.put(`${this.configuration.api.endpoint}/${this.baseEndPoint}/${BaseModel.internalId}`, BaseModel);
        }
        else{
            return this.http.post(`${this.configuration.api.endpoint}/${this.baseEndPoint}`, BaseModel);
        }        
    }

    AddorUpdateFormData(BaseModel): Observable<any> {
        if(BaseModel.get('id') != null && BaseModel.get('id') > 0) {
            return this.http.put(`${this.configuration.api.endpoint}/${this.baseEndPoint}/${BaseModel.get('internalId')}`, BaseModel);
        }
        else{
            return this.http.post(`${this.configuration.api.endpoint}/${this.baseEndPoint}`, BaseModel);
        }        
    }

    Delete(id: string): Observable<any> {        
        return this.http.delete(`${this.configuration.api.endpoint}/${this.baseEndPoint}/${id}`);
    }
}
