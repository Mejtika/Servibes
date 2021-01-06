import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AppointmentDataService } from 'src/app/data-service/appointment-data.service';
import { EmployeeDataService } from 'src/app/data-service/employee-data.service';
import { ServiceHours, Service, Employee } from 'src/app/shared/interfaces/company';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-employee-time-reservations',
  templateUrl: './employee-time-reservations.component.html',
  styleUrls: ['./employee-time-reservations.component.scss']
})
export class EmployeeTimeReservationsComponent implements OnInit {
  public employeeId: string;
  public companyId: string;
  public step: number = 1;
  public maxStep: number = 4;
  public canMoveToNextStep: boolean;
  public selectedDate: Date;
  public selectedDuration: Date;
  public minDate: Date;
  public maxDate: Date;
  public bsConfig: Partial<BsDatepickerConfig>;
  public serviceAvailableHours: ServiceHours[];
  public selectedHour: ServiceHours;

  constructor(
    public bsModalRef: BsModalRef,
    private employeeDataService: EmployeeDataService,
    private appointmentDataService: AppointmentDataService,
    private toastr: ToastrService) {
      this.selectedDuration = new Date();
      this.selectedDuration.setHours(0,0,0,0);
      this.selectedDate = null;
      this.minDate = new Date();
      this.maxDate = new Date();
      this.minDate.setDate(this.minDate.getDate());
      this.maxDate.setMonth(this.maxDate.getMonth() + 2);
      this.bsConfig = Object.assign({}, { containerClass: 'theme-dark-blue' });
  }

  ngOnInit() {

  }

  private checkIfCanMoveToNextStep(): boolean {
 
    if(this.step == 1 && this.selectedDate != null)
      return true;

    console.log(this.step,  this.getTotalMinutes(this.selectedDuration));
    if (this.step == 2 && this.getTotalMinutes(this.selectedDuration) != 0 )
      return true;

    if (this.step == 3 && this.selectHour != null )
      return true;

    if(this.step == 4)
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

  onValueChange(value: Date): void {
    this.selectedDate = value;
    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  } 

  selectedDurationChanged(): void{
    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  public selectDuration() {
    this.employeeDataService.getEmployeeDayAvailability(
      this.companyId,
      this.employeeId, 
      formatDate(this.selectedDate, 'yyyy-MM-dd', 'en_US'), 
      this.getTotalMinutes(this.selectedDuration))
      .subscribe(result => {
      this.serviceAvailableHours = result;
    });
    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
    this.nextStep();
  }

  public selectHour(hour: ServiceHours) {
    this.selectedHour = hour;
    this.canMoveToNextStep = this.checkIfCanMoveToNextStep();
  }

  getTotalMinutes(date: Date): number{
    var minutesInHours = date.getHours() * 60;
    return date.getMinutes() + minutesInHours;
  }

  public createTimeReservation() {
      const request = {
        start: formatDate(this.selectedDate, 'yyy-MM-dd', "en_US") + "T" + this.selectedHour.time,
        duration: this.getTotalMinutes(this.selectedDuration)
      };    
      this.appointmentDataService.postTimeReservation(this.companyId, this.employeeId, request.start, request.duration).subscribe(() => {
        this.closeModal();
        this.toastr.success("Reservation created successfully!");
      });
  }

  public closeModal() {
    this.resetSelectedData();
    this.bsModalRef.hide();
  }

  public resetSelectedData() {
    this.selectedDate = null;
    this.selectedHour = null;
    this.selectedDuration = new Date();
    this.selectedDuration.setHours(0,0,0,0);
    this.serviceAvailableHours = null;
    this.step = 1;
  }
}
