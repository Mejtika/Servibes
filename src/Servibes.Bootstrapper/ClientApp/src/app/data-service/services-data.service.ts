import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { Service, Employee } from '../shared/interfaces/company';

@Injectable()
export class ServicesDataService extends ApiService<Service> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanyServices(companyId: string): Observable<Service[]> {
    return this.get(`companies/${companyId}/services`);
  }

  public getServiceById(companyId: string, serviceId: string): Observable<Service> {
    return this.get(`companies/${companyId}/services/${serviceId}`);
  }

  public getServiceEmployees(companyId: string, serviceId: string): Observable<Employee[]> {
    return this.get(`companies/${companyId}/services/${serviceId}/employees`);
  }
}
