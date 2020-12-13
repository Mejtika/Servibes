import { Component, ViewChild, ElementRef } from '@angular/core';
import { formatDate } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { CompanyDataService } from 'src/app/data-service/company-data.servce';
import { EmployeeDataService } from 'src/app/data-service/employee-data.service';
import { MockDataService } from 'src/app/data-service/mock-data.service';
import { ServicesDataService } from 'src/app/data-service/services-data.service';
import { AppointmentDataService } from 'src/app/data-service/appointment-data.service';

import { ICompany, IService, IEmployee, IServiceHours } from 'src/app/shared/interfaces/company';
import { forkJoin } from 'rxjs';
import { ModalComponent } from '../../shared/components/modal/modal.component';


@Component({
    selector: 'company-page',
    templateUrl: 'company-page.component.html',
    styleUrls: ['./company-page.component.css']
})
export class CompanyPageComponent {
    company: ICompany;
    public weekDays: string[] = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
     public selectedService: IService;

    // public selectedDate: Date = new Date();

    // public serviceEmployees: IEmployee[];
    // public selectedEmployee: IEmployee;

    // public serviceAvailableHours: IServiceHours[];

    @ViewChild("modal") modal: ModalComponent;

    constructor(
        private companyDataService: CompanyDataService,
        private servicesDataService: ServicesDataService,
        private employeeDataService: EmployeeDataService,
        private appointmentDataService: AppointmentDataService,
        private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        const companyId: string = this.activatedRoute.snapshot.paramMap.get('id');

        this.loadCompanyData(companyId);

        //this.selectedEmployee = null;
    }

    loadCompanyData(companyId: string) {
        let company = this.companyDataService.getCompanyById(companyId);
        let openingHours = this.companyDataService.getCompanyOpeningHours(companyId);
        let services = this.servicesDataService.getAllCompanyServices(companyId);
        let employees = this.employeeDataService.getAllCompanyEmployees(companyId);

        forkJoin([company, openingHours, services, employees]).subscribe(result => {
            result[0].openingHours = result[1];
            result[0].services = result[2];
            result[0].employees = result[3];

            this.company = result[0];
        });
  }

    public selectService(service: IService) {
      this.selectedService = service;

      // this.servicesDataService.getServiceEmployees(this.company.companyId, service.serviceId).subscribe(result => {
      //   console.log('companyId', this.company.companyId);
      //   console.log('serviceId', service.serviceId);

      //   console.log('serviceEmployees', result);

      //   this.serviceEmployees = result;
      // });
    }

    // public selectEmployee(employee: IEmployee) {
    //   this.selectedEmployee = employee;

    //   console.log('selectedDate', this.selectedDate);

    //   this.employeeDataService.getEmployeeDayAvailability(this.company.companyId, this.selectedEmployee.employeeId, formatDate(this.selectedDate, 'yyyy-MM-dd', 'en_US'), this.selectedService.duration).subscribe(result => {
    //     this.serviceAvailableHours = result;

    //     console.log('serviceAvailableHours', result);
    //   });
    // }

    // public createAppointment(hour: IServiceHours) {
    //     console.log('created appointment on ', hour);

    //   let appointment = this.createAppointmentObject(this.selectedEmployee, this.selectedService, this.selectedDate, hour);

    //   this.appointmentDataService.postAppointment(this.company.companyId, this.selectedEmployee.employeeId, appointment).subscribe(result => {
    //     console.log('Appointment created: ', appointment);
    //   });

    //   this.resetSelectedData();
    //   this.modal.closeModalOnEscape();
    // }

    // public resetSelectedData() {
    //     this.selectedDate = new Date();
    //     this.serviceEmployees = null;
    //     this.selectedEmployee = null;
    //     this.selectedService = null;
    //     this.serviceAvailableHours = null;
    // }

    // private createAppointmentObject(employee: IEmployee, service: IService, selectedDate: Date, selectedHour: IServiceHours) {
    //     return {
    //       employeeName: employee.firstName + " " + employee.lastName,
    //       serviceName: service.serviceName,
    //       servicePrice: service.price,
    //       serviceDuration: service.duration,
    //       start: formatDate(selectedDate, 'yyy-MM-dd', "en_US") + "T" + selectedHour.time
    //     };
    // }

}
