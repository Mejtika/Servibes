import { Component, ViewChild } from '@angular/core';
import { DxSchedulerComponent } from 'devextreme-angular';

import { MockDataService } from 'src/app/data-service/mock-data.service';
import { IAppointment, IClient, ICompany, IEmployee, IService } from 'src/app/shared/interfaces/company';
import Query from 'devextreme/data/query';

@Component({
    selector: 'appointments',
    templateUrl: './appointments.component.html',
    styleUrls: ['./appointments.component.css']
})
export class CalendarComponent {
    @ViewChild(DxSchedulerComponent, { static: false }) scheduler: DxSchedulerComponent;

    currentDate: Date = new Date(2020, 9, 21);

    appointments: IAppointment[];
    services: IService[];
    employees: IEmployee[];

    constructor(mockDataService: MockDataService) {

        mockDataService.getAppointments().subscribe(result => {
          this.appointments = result;
          console.log(result);
        });
        
        mockDataService.getCompanyEmployeees("1").subscribe(result => {
          this.employees = result;
          console.log(result);
        });

        mockDataService.getCompanyServices("1").subscribe(result => {
          this.services = result;
          console.log(result);
        });
  }

  log(val) { console.log(val); }

    onSchedulerReady() {
        let now = new Date();
        now.setHours(now.getHours() - 1);   //subtract 1h to display current time in a center of scheduler

        this.scheduler.instance.scrollToTime(now.getHours(), now.getMinutes());
    }

    getDataObj(objData) {
        for(var i = 0; i < this.appointments.length; i++) {
            if(this.appointments[i].startDate.getTime() === objData.startDate.getTime() && this.appointments[i].employeeId === objData.employeeId)
                return this.appointments[i];
        }
    }

  getServiceById(id: string) {
    console.log(Query(this.services).filter(["id", "=", id]).toArray()[0]);
      return Query(this.services).filter(["id", "=", id]).toArray()[0];
    }
}
