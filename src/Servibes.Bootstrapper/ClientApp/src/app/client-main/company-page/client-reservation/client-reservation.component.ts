import { Component, Input, ViewChild } from '@angular/core';
import { formatDate } from '@angular/common';

import { ICompany, IEmployee, IService, IServiceHours } from '../../../shared/interfaces/company';
import { ModalComponent } from '../../../shared/components/modal/modal.component';

import { ServicesDataService } from '../../../data-service/services-data.service';
import { EmployeeDataService } from '../../../data-service/employee-data.service';

@Component({
  selector: 'client-reservation',
  templateUrl: './client-reservation.component.html',
  styleUrls: ['./client-reservation.component.css']
})
export class ClientReservationComponent {
  @Input() company: ICompany;
  @Input() service: IService;

  public selectedDate: Date = new Date();

  public serviceEmployees: IEmployee[];
  public selectedEmployee: IEmployee;

  public selectedService: IService;
  public serviceAvailableHours: IServiceHours[];

  constructor(
    private servicesDataService: ServicesDataService,
    private employeeDataService: EmployeeDataService) {

  }

  ngOnInit() {
    this.servicesDataService.getServiceEmployees(this.company.companyId, this.service.serviceId).subscribe(result => {
      console.log('companyId', this.company.companyId);
      console.log('serviceId', this.service.serviceId);

      console.log('serviceEmployees', result);

      this.serviceEmployees = result;
    });
  }

  public selectEmployee(employee: IEmployee) {
    this.selectedEmployee = employee;

    console.log('selectedDate', this.selectedDate);

    this.employeeDataService.getEmployeeDayAvailability(this.company.companyId, this.selectedEmployee.employeeId, formatDate(this.selectedDate, 'yyyy-MM-dd', 'en_US'), this.selectedService.duration).subscribe(result => {
      this.serviceAvailableHours = result;

      console.log('serviceAvailableHours', result);
    });
  }

  public createAppointment(hour: IServiceHours) {
      console.log('created appointment on ', hour);

    this.resetSelectedData();
    //this.bookingModal.closeModalOnEscape();
  }

  public resetSelectedData() {
      this.selectedDate = new Date();
      this.serviceEmployees = null;
      this.selectedEmployee = null;
      this.selectedService = null;
      this.serviceAvailableHours = null;
  }
}
