import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { combineLatest } from "rxjs";
import { map } from "rxjs/operators";
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { environment } from "src/environments/environment";
import { ClientAppointmentService } from "./client-appointment.service";
import { PaidAppointmentDetails } from "./client-payments/client-payments.component";

export interface PaidAppointmentsDto {
  appointmentId: string;
  price: number;
}

@Injectable()
export class ClientPaymentsService {
  constructor(
    private httpClient: HttpClient,
    private appointmentService: ClientAppointmentService
  ) {}

  paidAppointments$ = this.httpClient.get<PaidAppointmentsDto[]>(
    `${environment.backendEndpoint}account/sales/appointments`
  );

  paidAppointmentsDetails$ = combineLatest([
    this.appointmentService.appointmentWithCompany$,
    this.paidAppointments$
  ]).pipe(
    map(([appointmentsDetails, paidAppointments]) => {
      let paidAppointmentsDetails: PaidAppointmentDetails[] = [];
      paidAppointments.forEach(paidAppointment => {
        var appointment = appointmentsDetails.find(appointment => appointment.appointmentId == paidAppointment.appointmentId);
        let paidAppointmentDetails: PaidAppointmentDetails = {
          appointmentId: appointment.appointmentId,
          price: paidAppointment.price,
          company: appointment.company,
          employeeName: appointment.employeeName,
          serviceName: appointment.serviceName,
          start: appointment.start,
          end: appointment.end
        }
        console.log(paidAppointmentDetails);
        paidAppointmentsDetails.push(paidAppointmentDetails);
      });
      return paidAppointmentsDetails;
    })
  );
}
