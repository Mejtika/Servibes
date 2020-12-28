import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PaidAppointmentDetails } from '../client-payments/client-payments.component';

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
