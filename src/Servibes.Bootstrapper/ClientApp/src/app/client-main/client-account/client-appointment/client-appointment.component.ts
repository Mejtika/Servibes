import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";
import { ClientAppointmentService } from "../client-appointment.service";
import { AppointmentDetails } from "../client-appointments/client-appointments.component";

@Component({
  selector: "client-appointment",
  templateUrl: "./client-appointment.component.html",
  styleUrls: ["./client-appointment.component.css"],
})
export class ClientAppointmentComponent {
  appointment: AppointmentDetails;
  isCollapsed: boolean = true;
  cancellationReason: string;

  constructor(
    private clientAppointmentService: ClientAppointmentService,
    private bsModalRef: BsModalRef,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
  }

  public closeModal() {
    this.bsModalRef.hide();
  }

  public cancel(cancellationForm: NgForm) {
    this.clientAppointmentService.cancelAppointment(
      this.appointment.appointmentId,
      cancellationForm.value.cancellationReason
    ).subscribe(_ => {
      this.closeModal();
      this.refreshPage();
      this.toastr.success("Appointment was cancelled successfully!");
    });
  }

  public canBeCanceled(): boolean {
    return this.appointment.status.toUpperCase() === "CONFIRMED";
  }

  refreshPage(){
    this.router.navigateByUrl('/client/account/appointments');
  }
}
