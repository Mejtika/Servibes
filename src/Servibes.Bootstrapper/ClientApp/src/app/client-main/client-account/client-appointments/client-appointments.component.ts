import { Component } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";
import { IAddress } from "src/app/shared/interfaces/company";
import { ClientAppointmentService } from "../client-appointment.service";
import { ClientAppointmentComponent } from "../client-appointment/client-appointment.component";

export interface CompanyDetails {
  companyId: string;
  companyName: string;
  phoneNumber: string;
  address: IAddress;
}

export interface AppointmentDetails {
  ordinalNumber?: number;
  appointmentId: string;
  companyId: string;
  company?: CompanyDetails;
  status: string;
  employeeId: string;
  employeeName: string;
  serviceName: string;
  servicePrice: number;
  start: string;
  end: string;
  cancellationReason: string;
}

@Component({
  selector: "client-appointments",
  templateUrl: "./client-appointments.component.html",
  styleUrls: ["./client-appointments.component.css"],
})
export class ClientAppointmentsComponent {
  page: number = 1;
  pageSize: number = 5;
  appointments: AppointmentDetails[];
  pagedAppointments: AppointmentDetails[];
  collectionSize: number;

  constructor(
    private clientAppointmentService: ClientAppointmentService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.clientAppointmentService.appointmentWithCompany$.subscribe(
      (result) => {
        this.appointments = result;
        this.collectionSize = result.length;
        this.refreshAppointments();
      }
    );
  }

  refreshAppointments() {
    this.pagedAppointments = this.appointments
      .map((appointment, i) => ({ ordinalNumber: i + 1, ...appointment }))
      .slice(
        (this.page - 1) * this.pageSize,
        (this.page - 1) * this.pageSize + this.pageSize
      );
  }

  showAppointmentsDetails(appointment: AppointmentDetails) {
    const initialState = {
        appointment
      };
    this.modalService.show(ClientAppointmentComponent, { initialState });
  }
}
