import { ClientBaseService } from './client-base.service';
import { ProfileService } from './../../profile/services/profile.service';
import { ServicesDataService } from './../../../../data-service/services-data.service';
import { EmployeeDataService } from './../.././../../data-service/employee-data.service';
import { AppointmentDataService } from './../../../../data-service/appointment-data.service';
import { ClientDataService } from './../../../../data-service/client-data.service';

export const services = [
    ClientBaseService, 
    ProfileService, 
    ServicesDataService, 
    EmployeeDataService, 
    AppointmentDataService,
    ClientDataService];

export * from './client-base.service';
