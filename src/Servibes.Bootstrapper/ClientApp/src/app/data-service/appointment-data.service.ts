import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { IService, IEmployee, IAppointment } from '../shared/interfaces/company';

@Injectable()
export class AppointmentDataService extends ApiService<IAppointment> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanyServices(companyId: string): Observable<IAppointment> {
    return this.get(`companies/${companyId}/services`);
  }

  public postAppointment(companyId: string, employeeId: string, body: any): Observable<IAppointment> {
    return this.post(`companies/${companyId}/employees/${employeeId}/appointments`, body);
  }
}
