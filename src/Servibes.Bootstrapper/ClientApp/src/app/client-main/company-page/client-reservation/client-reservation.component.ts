import { Component, Input, ViewChild } from '@angular/core';
import { formatDate } from '@angular/common';

import { Client, Company, Employee, Service, ServiceHours } from '../../../shared/interfaces/company';

import { ServicesDataService } from '../../../data-service/services-data.service';
import { EmployeeDataService } from '../../../data-service/employee-data.service';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppointmentDataService } from '../../../data-service/appointment-data.service';
import { ToastrService } from 'ngx-toastr';
import { ClientDataService } from '../../..//data-service/client-data.service';
import { mergeMap } from 'rxjs/operators';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'client-reservation',
  templateUrl: './client-reservation.component.html',
  styleUrls: ['./client-reservation.component.css']
})
export class ClientReservationComponent {
  company: Company;
  service: Service;

  public step: number = 1;
  public maxStep: number = 4;
  public canMoveToNextStep: boolean;

  public selectedDate: Date;
  public minDate: Date;
  public maxDate: Date;
  public bsConfig: Partial<BsDatepickerConfig>;

  public serviceEmployees: Employee[];
  public selectedEmployee: Employee;

  public serviceAvailableHours: ServiceHours[];
  public selectedHour: ServiceHours;

  constructor(
    public bsModalRef: BsModalRef,
    private servicesDataService: ServicesDataService,
    private employeeDataService: EmployeeDataService,
    private appointmentDataService: AppointmentDataService,
    private clientDataService: ClientDataService,
    private toastr: ToastrService) {
      this.selectedDate = new Date();
      this.minDate = new Date();
      this.maxDate = new Date();
      this.minDate.setDate(this.minDate.getDate());
      this.maxDate.setMonth(this.maxDate.getMonth() + 2);
      this.bsConfig = Object.assign({}, { containerClass: 'theme-dark-blue' });
  }

  ngOnInit() {
    this.servicesDataService.getServiceEmployees(this.company.companyId, this.service.serviceId).subscribe(result => {
      this.serviceEmployees = result;
    });

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  private checkIfCanMoveToNextStep(): boolean {
    if (this.step == 1 && this.selectedDate != null)
      return true;

    if (this.step == 2 && this.selectedEmployee != null)
      return true;

    if (this.step == 3 && this.selectedHour != null)
      return true;

    return false;
  }

  public nextStep() {
    if (this.step < this.maxStep)
      this.step++;

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  public prevStep() {
    if (this.step > 1)
      this.step--;

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  public selectEmployee(employee: Employee) {
    this.selectedEmployee = employee;

    console.log('selectedDate', this.selectedDate);

    this.employeeDataService.getEmployeeDayAvailability(this.company.companyId, this.selectedEmployee.employeeId, formatDate(this.selectedDate, 'yyyy-MM-dd', 'en_US'), this.service.duration).subscribe(result => {
      this.serviceAvailableHours = result;

      console.log('serviceAvailableHours', result);
    });

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }


  onValueChange(value: Date): void {
    this.selectedDate = value;
  } 

  public selectHour(hour: ServiceHours) {
    this.selectedHour = hour;

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  public createReservation() {
    this.clientDataService.getClientData().subscribe(result => {

      console.log('me', result);

      const appointmentObject = {
        reserveeId: result.id,
        serviceId: this.service.serviceId,
        start: formatDate(this.selectedDate, 'yyy-MM-dd', "en_US") + "T" + this.selectedHour.time
      };

      this.appointmentDataService.postAppointment(this.company.companyId, this.selectedEmployee.employeeId, appointmentObject).subscribe(result => {
        console.log('created appointment with data', appointmentObject);
        this.closeModal();
        this.toastr.success("Reservation created successfuly!");
      })
    });
  }

  public closeModal() {
    this.resetSelectedData();
    this.bsModalRef.hide();
  }

  public resetSelectedData() {
    this.selectedDate = new Date();
    this.selectedHour = null;
    this.serviceEmployees = null;
    this.selectedEmployee = null;
    this.serviceAvailableHours = null;
    this.step = 1;
  }
}
