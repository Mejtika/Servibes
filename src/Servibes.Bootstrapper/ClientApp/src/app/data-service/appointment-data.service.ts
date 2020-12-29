import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { Service, Employee, Appointment } from '../shared/interfaces/company';

@Injectable()
export class AppointmentDataService extends ApiService<Appointment> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanyServices(companyId: string): Observable<Appointment> {
    return this.get(`companies/${companyId}/services`);
  }

  public postAppointment(companyId: string, employeeId: string, body: any): Observable<Appointment> {
    return this.post(`companies/${companyId}/employees/${employeeId}/appointments`, body);
  }
}
