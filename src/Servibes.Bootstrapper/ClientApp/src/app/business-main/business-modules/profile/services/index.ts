import { ProfileService } from './profile.service';
import { EmployeeService } from './employees.service';
import { CategoriesService } from './categories.service';
import { ServicesService } from './services.service';
import { ValidationService } from './validation.service';

export const services = [ProfileService, EmployeeService, CategoriesService, ServicesService, ValidationService];

export * from './profile.service';
export * from './employees.service';
export * from './categories.service';
export * from './services.service';
export * from './validation.service';