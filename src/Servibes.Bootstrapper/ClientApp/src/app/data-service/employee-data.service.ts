import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { formatDate } from '@angular/common';
import { Employee, ServiceHours } from '../shared/interfaces/company';
import { TimeOff } from '../business-main/business-modules/profile/models/employee.model';
import { CancelTimeOffRequest, GiveTimeOffRequest } from '../business-main/business-modules/profile/models/requests.model';

@Injectable()
export class EmployeeDataService extends ApiService<Employee> {

  constructor(http: HttpClient) {
    super(http)
  }

  public getAllCompanyEmployees(companyId: string) : Observable<Employee[]> {
    return this.get(`companies/${companyId}/employees`);
  }

  public getEmployeeById(companyId: string, employeeId: string) : Observable<Employee> {
    return this.get(`companies/${companyId}/employees/${employeeId}`);
  }

  public getEmployeeDayAvailability(companyId: string, employeeId: string, date: string, duration: number): Observable<ServiceHours[]> {
    return this.get(`companies/${companyId}/employees/${employeeId}/availability?Date=${date}&Duration=${duration}`);
  }

  public getEmployeeTimeOffs(employeeId: string, companyId: string): Observable<TimeOff[]> {
    return this.get(`companies/${companyId}/employees/${employeeId}/timeOffs`);
  }

  public giveEmployeeTimeOff(employeeId: string, companyId: string, start: Date, end: Date): Observable<TimeOff[]> {
    const body: GiveTimeOffRequest = {
      start: formatDate(start, 'yyyy-MM-dd', 'en_US'), 
      end: formatDate(end, 'yyyy-MM-dd', 'en_US')
    };

    return this.post(`companies/${companyId}/employees/${employeeId}/timeOffs`, body);
  }

  public cancelEmployeeTimeOff(employeeId: string, companyId: string, startDate: string): Observable<TimeOff[]> {
    return this.delete(`companies/${companyId}/employees/${employeeId}/timeOffs/${startDate}/cancel`);
  }
}