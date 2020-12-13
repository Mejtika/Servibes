import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { IService, IEmployee } from '../shared/interfaces/company';

@Injectable()
export class ServicesDataService extends ApiService<IService> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanyServices(companyId: string): Observable<IService[]> {
    return this.get(`companies/${companyId}/services`);
  }

  public getServiceById(companyId: string, serviceId: string): Observable<IService> {
    return this.get(`companies/${companyId}/services/${serviceId}`);
  }

  public getServiceEmployees(companyId: string, serviceId: string): Observable<IEmployee[]> {
    return this.get(`companies/${companyId}/services/${serviceId}/employees`);
  }
}
