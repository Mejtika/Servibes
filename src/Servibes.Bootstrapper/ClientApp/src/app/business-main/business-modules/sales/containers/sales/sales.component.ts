import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { AppointmentDto } from "../../models";
import {
  SalesService,
} from "../../services/sales.service";

@Component({
  selector: "sales",
  templateUrl: "./sales.component.html",
  styleUrls: ["./sales.component.scss"],
})
export class SalesComponent implements OnInit {
  public appointments: AppointmentDto[];
  public selectedAppointment: AppointmentDto = null;
  public isCollapsed: boolean = true;

  constructor(
    private salesService: SalesService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.salesService.getUnpaidAppointments().subscribe((result) => {
      this.appointments = result;
    });
  }

  selectClient(appointment: AppointmentDto) {
    this.selectedAppointment = appointment;
  }

  checkoutAppointment(appointment: AppointmentDto) {
    this.salesService
      .checkout(appointment.appointmentId, appointment.price)
      .subscribe(_ => 
        {
            this.toastr.success("Payment completed successfully!");
            var indexOfpaidAppointment = this.appointments.indexOf(appointment);
            this.appointments = this.appointments.splice(indexOfpaidAppointment, 1);
            this.selectedAppointment = null;
        });
  }
}
