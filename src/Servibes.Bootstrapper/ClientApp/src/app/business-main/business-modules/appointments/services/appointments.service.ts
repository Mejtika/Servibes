import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { environment } from "src/environments/environment";
import { Appointment, CompanyTimeOff, TimeReservation } from "../models/appointments.model";
import { ProfileService } from "../../profile/services/profile.service";
import { mergeMap, switchMap } from "rxjs/operators";
import { IProfile } from '../../profile/models/profile.model';
import { formatDate } from "@angular/common";
import { IEmployee } from "../../profile/models";
import { TimeOff } from '../../profile/models/employee.model';

export interface CancelAppointmentRequest {
  cancellationReason: string;
}

@Injectable()
export class AppointmentsService {
  // private _deleteOperationSuccessfullEvent$: Subject<boolean> = new Subject<boolean>();
  // appointmentDeletedAction$ = this._deleteOperationSuccessfullEvent.asObservable();

  constructor(
    private http: HttpClient,
  ) {}

  getCompanyAppointments(
    date: Date
  ): Observable<Appointment[]> {
    const appointmentDate = formatDate(date, 'yyyy-MM-dd', 'en_US');
    return this.http.get<IProfile>(`${environment.backendEndpoint}companies/owner`)
      .pipe(
        switchMap(({companyId}) =>
          this.http.get<Appointment[]>(`${environment.backendEndpoint}companies/${companyId}/appointments?date=${appointmentDate}`))
      );
  }

  public getCompanyEmployees() : Observable<IEmployee[]> {
    return this.http.get<IProfile>(`${environment.backendEndpoint}companies/owner`)
    .pipe(
      switchMap(({companyId}) =>
        this.http.get<IEmployee[]>(`${environment.backendEndpoint}companies/${companyId}/employees`))
    );
}


  cancelAppointment(appointmentId: string, cancellationReason: string){
    const requestBody: CancelAppointmentRequest = {
      cancellationReason
    };
    return this.http.get<IProfile>(`${environment.backendEndpoint}companies/owner`)
    .pipe(
      switchMap(({companyId}) =>
        this.http.post(`${environment.backendEndpoint}companies/${companyId}/appointments/${appointmentId}/cancel`, requestBody))
    );
  }

  cancelTimeReservation(timeReservationId: string){
    return this.http.get<IProfile>(`${environment.backendEndpoint}companies/owner`)
    .pipe(
      switchMap(({companyId}) =>
        this.http.post(`${environment.backendEndpoint}companies/${companyId}/timeReservations/${timeReservationId}/cancel`, {}))
    );
  }


  getCompanyTimeReservations(
    date: Date
  ): Observable<TimeReservation[]> {
    const timeReservationDate = formatDate(date, 'yyyy-MM-dd', 'en_US');
    return this.http.get<IProfile>(`${environment.backendEndpoint}companies/owner`)
    .pipe(
      switchMap(({companyId}) =>
      this.http.get<TimeReservation[]>(`${environment.backendEndpoint}companies/${companyId}/timeReservations?date=${timeReservationDate}`))
    );
  }

  getCompanyTimeOffs(
    date: Date
  ): Observable<CompanyTimeOff[]> {
    const timeOffsDate = formatDate(date, 'yyyy-MM-dd', 'en_US');
    return this.http.get<IProfile>(`${environment.backendEndpoint}companies/owner`)
    .pipe(
      switchMap(({companyId}) =>
      this.http.get<CompanyTimeOff[]>(`${environment.backendEndpoint}companies/${companyId}/timeOffs?date=${timeOffsDate}`))
    );
  }
}
