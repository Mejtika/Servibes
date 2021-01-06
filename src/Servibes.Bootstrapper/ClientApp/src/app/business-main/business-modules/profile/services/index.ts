import { ProfileService } from "./profile.service";
import { EmployeeService } from "./employees.service";
import { CategoriesService } from "./categories.service";
import { ServicesService } from "./services.service";
import { ValidationService } from "./validation.service";
import { AppointmentDataService } from "src/app/data-service/appointment-data.service";
import { EmployeeDataService } from "src/app/data-service/employee-data.service";

export const services = [
  ProfileService,
  EmployeeService,
  EmployeeDataService,
  AppointmentDataService,
  CategoriesService,
  ServicesService,
  ValidationService,
];

export * from "./profile.service";
export * from "./employees.service";
export * from "./categories.service";
export * from "./services.service";
export * from "./validation.service";
