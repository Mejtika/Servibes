import { Component, Input, ViewChild } from '@angular/core';
import { formatDate } from '@angular/common';

import { Client, Company, Employee, Service, ServiceHours } from './../../../../../shared/interfaces/company';

import { ServicesDataService } from './../../../../../data-service/services-data.service';
import { EmployeeDataService } from '../../../../../data-service/employee-data.service';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppointmentDataService } from './../../../../../data-service/appointment-data.service';
import { ToastrService } from 'ngx-toastr';
import { ClientDataService } from '../../../../../data-service/client-data.service';
import { mergeMap } from 'rxjs/operators';
import { IClient } from '../../models/client.model';
import { IProfile } from '../../../profile/models';

@Component({
  selector: 'client-reservation',
  templateUrl: './client-reservation.component.html',
  styleUrls: ['./client-reservation.component.css']
})
export class ClientReservationComponent {
  companyId: string;
  service: Service;
  client: IClient;

  public step: number = 1;
  public maxStep: number = 5;
  public canMoveToNextStep: boolean;

  public selectedDate: Date = new Date();

  public serviceEmployees: Employee[];
  public selectedEmployee: Employee;

  public serviceAvailableHours: ServiceHours[];
  public selectedHour: ServiceHours;

  public services: Service[];

  constructor(
    public bsModalRef: BsModalRef,
    private servicesDataService: ServicesDataService,
    private employeeDataService: EmployeeDataService,
    private appointmentDataService: AppointmentDataService,
    private toastr: ToastrService) {
  }

  ngOnInit() {
    this.servicesDataService.getAllCompanyServices(this.companyId).subscribe(services => {
      this.services = services;
    });
  }

  private checkIfCanMoveToNextStep(): boolean {
    if(this.step == 1 && this.service != null)
      return true;

    if (this.step == 2 && this.selectedDate != null)
      return true;

    if (this.step == 3 && this.selectedEmployee != null)
      return true;

    if (this.step == 4 && this.selectedHour != null)
      return true;

    if(this.step == 5)
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

    this.employeeDataService.getEmployeeDayAvailability(this.companyId, this.selectedEmployee.employeeId, formatDate(this.selectedDate, 'yyyy-MM-dd', 'en_US'), this.service.duration).subscribe(result => {
      this.serviceAvailableHours = result;

      console.log('serviceAvailableHours', result);
    });

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  public selectService(service: Service) {
    this.service = service;

    this.servicesDataService.getServiceEmployees(this.companyId, this.service.serviceId).subscribe(result => {
      this.serviceEmployees = result;
    });

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();

  }

  public selectHour(hour: ServiceHours) {
    this.selectedHour = hour;

    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  public createReservation() {
      const appointmentObject = {
        reserveeId: this.client.clientId,
        serviceId: this.service.serviceId,
        start: formatDate(this.selectedDate, 'yyy-MM-dd', "en_US") + "T" + this.selectedHour.time
      };

      this.appointmentDataService.postAppointment(this.companyId, this.selectedEmployee.employeeId, appointmentObject).subscribe(result => {
        console.log('created appointment with data', appointmentObject);
        this.closeModal();
        this.toastr.success("Reservation created successfuly!");
      });
  }

  public closeModal() {
    this.resetSelectedData();
    this.bsModalRef.hide();
  }

  public resetSelectedData() {
    this.service = null;
    this.selectedDate = new Date();
    this.selectedHour = null;
    this.serviceEmployees = null;
    this.selectedEmployee = null;
    this.serviceAvailableHours = null;
    this.step = 1;
  }
}
