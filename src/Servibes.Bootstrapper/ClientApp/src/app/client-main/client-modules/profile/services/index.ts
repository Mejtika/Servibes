import { ProfileService } from './profile.service';
import { EmployeeService } from './employees.service';

export const services = [ProfileService, EmployeeService];

export * from './profile.service';
export * from './employees.service';