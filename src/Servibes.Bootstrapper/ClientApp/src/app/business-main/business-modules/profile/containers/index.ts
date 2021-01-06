import { ProfileComponent } from "./profile/profile.component";
import { EmployeesComponent } from "./employees/employees.component";
import { ServicesComponent } from "./services/services.component";
import { EmployeeFormComponent } from "./employee-form/employee-form.component";
import { ServiceFormComponent } from "./serviceForm/service-form.component";
import { OpeningHoursComponent } from "./opening-hours/opening-hours.component";
import { EmployeeWorkingHoursComponent } from "./employee-working-hours/employee-working-hours.component";
import { EmployeeTimeReservationsComponent } from './employee-time-reservations/employee-time-reservations.component';
import { EmployeeTimeOffsComponent } from './employee-time-offs/employee-time-offs.component';

export const containers = [
  ProfileComponent,
  EmployeesComponent,
  ServicesComponent,
  EmployeeFormComponent,
  ServiceFormComponent,
  OpeningHoursComponent,
  EmployeeWorkingHoursComponent,
  EmployeeTimeReservationsComponent,
  EmployeeTimeOffsComponent
];

export * from "./profile/profile.component";
export * from "./employees/employees.component";
export * from "./services/services.component";
export * from "./employee-form/employee-form.component";
export * from "./serviceForm/service-form.component";
export * from "./opening-hours/opening-hours.component";
export * from "./employee-working-hours/employee-working-hours.component";
export * from "./employee-time-reservations/employee-time-reservations.component";
