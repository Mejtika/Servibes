import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';

import { IEmployee } from '../shared/interfaces/company';

@Injectable()
export class EmployeeDataService extends ApiService<IEmployee> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanyEmployees(companyId: string) : Observable<IEmployee[]> {
    return this.get(`companies/${companyId}/employees`);
  }

  public getEmployeeById(companyId: string, employeeId: string) : Observable<IEmployee> {
    return this.get(`companies/${companyId}/employees/${employeeId}`);
  }
}
