import { Component } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";
import { ClientPaymentsService } from "../client-payments.service";
import { ClientPaymentComponent } from '../client-payment/client-payment.component';
import { PaidAppointmentDetails } from "src/app/shared/interfaces/company";

@Component({
  selector: "client-payments",
  templateUrl: "./client-payments.component.html",
  styleUrls: ["./client-payments.component.css"],
})
export class ClientPaymentsComponent {
  page: number = 1;
  pageSize: number = 5;
  collectionSize: number;
  paidAppointments: PaidAppointmentDetails[];
  pagedPaidAppointments: PaidAppointmentDetails[];

  constructor(
    private paymentService: ClientPaymentsService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.paymentService.paidAppointmentsDetails$.subscribe((result) => {
      this.paidAppointments = result;
      this.collectionSize = result.length;
      this.refreshAppointments();
    });
  }

  refreshAppointments() {
    this.pagedPaidAppointments = this.paidAppointments
      .slice(
        (this.page - 1) * this.pageSize,
        (this.page - 1) * this.pageSize + this.pageSize
      );
  }

  showAppointmentsDetails(appointment: PaidAppointmentDetails) {
    const initialState = {
        appointment
      };
    this.modalService.show(ClientPaymentComponent, { initialState });
  }
}
