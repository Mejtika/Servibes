import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api.service';
import { IEmployee, IHoursRange } from '../models';

@Injectable()
export class EmployeeService extends ApiService<IEmployee> {
    constructor(http: HttpClient) {
        super(http)
    }

    public getAllEmployees(companyId: string) : Observable<IEmployee[]> {
        return this.get(`companies/${companyId}/employees`);
    }

    public getSingleEmployee(companyId: string, employeeId: string) : Observable<IEmployee> {
        return this.get(`companies/${companyId}/employees/${employeeId}`);
    }

    public addEmployee(companyId: string, employee: IEmployee) : Observable<IEmployee> {
        return this.post(`companies/${companyId}/employees`, employee);
    }

    public updateEmployee(companyId: string, employee: IEmployee) : Observable<IEmployee> {
        return this.put(`companies/${companyId}/employees/${employee.employeeId}`, employee);
    }

    public removeEmployee(companyId: string, employeeId: string) : Observable<IEmployee> {
        return this.delete(`companies/${companyId}/employees/${employeeId}`);
    }

    public getWorkingHours(companyId: string, employeeId: string) : Observable<IHoursRange[]> {
        return this.get(`companies/${companyId}/employees/${employeeId}/workingHours`);   
    }

    public changeWorkingHours(companyId: string, employeeId: string, body: object) {
        return this.post(`companies/${companyId}/employees/${employeeId}/workingHours`, body);
    }
}
