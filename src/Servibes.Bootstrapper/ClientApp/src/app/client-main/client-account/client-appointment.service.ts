import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, tap } from "rxjs/operators";
import { combineLatest } from 'rxjs';
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { environment } from "src/environments/environment";
import { AppointmentDetails } from "src/app/shared/interfaces/company";

export interface CancelAppointmentRequest {
  cancellationReason: string;
}

@Injectable()
export class ClientAppointmentService {
  constructor(
    private httpClient: HttpClient,
    private companyService: CompanyDataService
  ) {}

  appointments$ = this.httpClient.get<AppointmentDetails[]>(
    `${environment.backendEndpoint}account/appointments`
  );

  appointmentWithCompany$ = combineLatest([
    this.appointments$,
    this.companyService.companies$,
  ]).pipe(
    map(([appointments, companies]) =>
      appointments.map(
        (appointment) =>
          ({
            ...appointment,
            company: companies.find((c) => appointment.companyId === c.companyId),
          } as AppointmentDetails)
      )
    )
  );

  cancelAppointment(appointmentId: string, cancellationReason: string) {
    const requestBody : CancelAppointmentRequest =  {
      cancellationReason
    }
    return this.httpClient.post(`${environment.backendEndpoint}account/appointments/${appointmentId}/cancel`, requestBody);
  }
}
