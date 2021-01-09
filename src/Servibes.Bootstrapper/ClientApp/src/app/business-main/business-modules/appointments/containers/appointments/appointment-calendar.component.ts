import { Component, ElementRef, TemplateRef, ViewChild } from "@angular/core";
import { CalendarEvent, CalendarEventAction } from "angular-calendar";
import { addHours, startOfDay } from "date-fns";
import { User } from "./day-view-scheduler.component";
import { AppointmentsService } from "../../services/appointments.service";
import { combineLatest, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { ToastrService } from 'ngx-toastr';
import { NgForm } from "@angular/forms";

export const colors: any = {
  red: {
    primary: "#ad2121",
    secondary: "#FAE3E3",
  },
  blue: {
    primary: "#1e90ff",
    secondary: "#D1E8FF",
  },
  yellow: {
    primary: "#e3bc08",
    secondary: "#FDF1BA",
  },
};

export enum ReservationType{
  Appointment,
  TimeReservation
}

@Component({
  selector: "appointment-calendar",
  templateUrl: "./appointment-calendar.component.html",
  styleUrls: ["./appointment-calendar.component.css"]
})
export class AppointmentCalendarComponent {
  private employees$: Observable<User[]>;
  employees: User[];
  appointments$: Observable<CalendarEvent[]>;
  viewDate = new Date();
  @ViewChild('eventModal') modalTemplate: TemplateRef<any>;
  modalRef: BsModalRef;
  public selectedReservation: CalendarEvent;
  public cancellationReason: string = "";

  ngOnInit() {
    this.employees$ = this.appointmentService.getCompanyEmployees().pipe(
      map((result) => {
        const employees: User[] = [];
        result.forEach((x) => {
          employees.push({
            id: x.employeeId,
            name: `${x.firstName} ${x.lastName}`,
            color: colors.blue,
          });
        });
        return employees;
      })
    );
    this.fetchReservations();
  }

  constructor(
    private appointmentService: AppointmentsService,
    private modalService: BsModalService,
    private toastr: ToastrService) {}


    handleClick(reservation: CalendarEvent): void {
      this.selectedReservation = reservation;    
      this.modalRef = this.modalService.show(this.modalTemplate, { class: 'modal-dialog-centered' });
    }
  
    cancelAppointment(cancellationForm: NgForm){
      this.appointmentService.cancelAppointment(this.selectedReservation.id as string, cancellationForm.value.cancellationReason)
      .subscribe(_ => {
        this.fetchReservations();
        this.modalRef.hide();
        this.toastr.success("Appointment was successfully cancelled!");
      });
    }
  
    cancelTimeReservation(){
      this.appointmentService.cancelTimeReservation(this.selectedReservation.id as string)
      .subscribe(_ => {
        this.fetchReservations();
        this.modalRef.hide();
        this.toastr.success("Time reservation was successfully cancelled!");
      });
    }

  fetchReservations() {
    this.appointments$ = combineLatest([
      this.employees$,
      this.appointmentService.getCompanyAppointments(this.viewDate),
      this.appointmentService.getCompanyTimeReservations(this.viewDate),
      this.appointmentService.getCompanyTimeOffs(this.viewDate)
    ]).pipe(
      map(([employees, appointments, timeReservations, timeOffs]) => {
        this.employees = employees;
        const events: CalendarEvent[] = [];
        appointments.forEach((x) => {
          const employee = employees.find((e) => e.id == x.employeeId);
          const event: CalendarEvent = {
            id: x.appointmentId,
            title: `${this.formatDate(x.start)}- ${this.formatDate(x.end)} ${x.serviceName}`,
            color: employee.color,
            start: new Date(x.start),
            end: new Date(x.end),
            meta: {
              user: employee,
              status: x.status,
              type: ReservationType.Appointment,
              client: x.reserveeName,
              service: { name: x.serviceName, price: x.servicePrice }
            },
          };
          if(event.meta.status !== "Canceled")
          {
            events.push(event);
          }
        });
        timeReservations.forEach((x) => {
          const employee = employees.find((e) => e.id == x.employeeId);
          const event: CalendarEvent = {
            id: x.timeReservationId,
            title: `${this.formatDate(x.start)}- ${this.formatDate(x.end)} Time reservation`,
            color: employee.color,
            start: new Date(x.start),
            end: new Date(x.end),
            meta: {
              user: employee,
              status: x.status,
              type: ReservationType.TimeReservation
            },
          };
          if(event.meta.status !== "Canceled")
          {
            events.push(event);
          }
        });
        timeOffs.forEach(x => {
          const employee = employees.find((e) => e.id == x.employeeId);
          const allDayEvent: CalendarEvent = {
            title: `TimeOff`,
            color: employee.color,
            start: new Date(this.viewDate),
            allDay: true,
            meta: {
              user: employee,
              duration: {start: x.start, end: x.end }
            },
          }
          events.push(allDayEvent);
        });
        console.log(events);
        return events;
      })
    );
  }

  isAppointment(type: ReservationType){
    return type === ReservationType.Appointment;
  }
  canBeCancelled(){
    return this.selectedReservation.start > new Date();
  }

  formatDate(date: Date) {
    return new Date(date)
      .toLocaleTimeString(
        "en-US", 
      {
        hour: "2-digit",
        minute: "2-digit",
      })
      .replace("AM", "")
      .replace("PM", "");
  }
}
