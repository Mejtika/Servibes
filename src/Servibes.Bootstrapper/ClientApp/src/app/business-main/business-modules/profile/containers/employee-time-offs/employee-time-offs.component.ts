import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AppointmentDataService } from 'src/app/data-service/appointment-data.service';
import { EmployeeDataService } from 'src/app/data-service/employee-data.service';
import { ServiceHours } from 'src/app/shared/interfaces/company';
import { TimeOff } from '../../models';

@Component({
  selector: 'app-employee-time-offs',
  templateUrl: './employee-time-offs.component.html',
  styleUrls: ['./employee-time-offs.component.scss']
})
export class EmployeeTimeOffsComponent implements OnInit {
  public employeeId: string;
  public companyId: string;
  public timeOffs: TimeOff[];
  public isCollapsed = true;
  public selectedDates: Date[];
  public minDate: Date;
  public maxDate: Date;
  public bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    public bsModalRef: BsModalRef,
    private employeeDataService: EmployeeDataService,
    private toastr: ToastrService) {
      this.selectedDates = null;
      this.minDate = new Date();
      this.maxDate = new Date();
      this.minDate.setDate(this.minDate.getDate());
      this.maxDate.setMonth(this.maxDate.getMonth() + 12);
      this.bsConfig = Object.assign({}, { containerClass: 'theme-dark-blue' });
  }

  ngOnInit() {
    this.employeeDataService.getEmployeeTimeOffs(this.employeeId, this.companyId).subscribe(result => this.timeOffs = result);
  }

  public giveTimeOff() {
    const [start, end] = this.selectedDates;
    this.employeeDataService.giveEmployeeTimeOff(this.employeeId, this.companyId,start, end).subscribe(_ => {
      this.closeModal();
      this.toastr.success("Time off created successfully!");
    });
  }

  public cancelTimeOff(timeOff: TimeOff) {
    console.log(timeOff.start);
    
    this.employeeDataService.cancelEmployeeTimeOff(this.employeeId, this.companyId, timeOff.start).subscribe(_ => {
      this.closeModal();
      this.toastr.success("Time off canceled successfully!");
    });
  }

  public closeModal() {
    this.bsModalRef.hide();
  }
}
