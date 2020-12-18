import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api.service';
import { IEmployee, IService } from '../models';

@Injectable()
export class ServicesService extends ApiService<IService> {
    constructor(http: HttpClient) {
        super(http)
    }

    public getAllServices(companyId: string) : Observable<IService[]> {
        return this.get(`companies/${companyId}/services`);
    }

    public getSingleService(companyId: string, serviceId: string) : Observable<IService> {
        return this.get(`companies/${companyId}/services/${serviceId}`);
    }

    public addService(companyId: string, service: IService) : Observable<IService> {
        return this.post(`companies/${companyId}/services`, service);
    }

    public updateService(companyId: string, service: IService) : Observable<IService> {
        return this.put(`companies/${companyId}/services/${service.serviceId}`, service);
    }

    public removeService(companyId: string, serviceId: string) : Observable<IService> {
        return this.delete(`companies/${companyId}/services/${serviceId}`);
    }
}
