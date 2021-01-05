import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { AppointmentDto, CheckoutRequest } from "../models";

@Injectable()
export class SalesService {
  constructor(private httpClient: HttpClient) {}

  getUnpaidAppointments(): Observable<AppointmentDto[]> {
    return this.httpClient.get<AppointmentDto[]>(
      `${environment.backendEndpoint}sales/appointments`
    );
  }

  getAppointmentsHistory(): Observable<AppointmentDto[]> {
    return this.httpClient.get<AppointmentDto[]>(
      `${environment.backendEndpoint}sales/appointments/history`
    );
  }

  checkout(appointmentId: string, price: number) {
    const requestBody: CheckoutRequest = {
      price,
    };
    return this.httpClient.post(
      `${environment.backendEndpoint}sales/appointments/${appointmentId}/checkout`,
      requestBody
    );
  }
}
