import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseAPIService } from './baseapi.service';

@Injectable()
export class SpotterService extends BaseAPIService {

    constructor(http: HttpClient) {
        super(http);
        this.SetBaseEndPoint("plotter");
    }
    
   
}
