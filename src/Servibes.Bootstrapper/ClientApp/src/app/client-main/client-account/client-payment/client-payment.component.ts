import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PaidAppointmentDetails } from 'src/app/shared/interfaces/company';

@Component({
  selector: 'app-client-payment',
  templateUrl: './client-payment.component.html',
  styleUrls: ['./client-payment.component.css']
})
export class ClientPaymentComponent {
  appointment: PaidAppointmentDetails;

  constructor(private bsModalRef: BsModalRef) { }

  public closeModal() {
    this.bsModalRef.hide();
  }
}
