export interface Company {
  companyId: string;
  companyName: string;
  phoneNumber: string;
  address: Address;
  category: string;
  description: string;
  coverPhoto: string;

  openingHours: OpeningHours[];
  employees: Employee[];
  services: Service[];
}

export enum Category {
  All = "All",
  Fryzjer = "Fryzjer",
  Barber = "Barber",
  Masaz = "Masaz",
  Makeup = "Makeup"
}

export enum City {
  Warszawa = 'Warszawa',
  Wroclaw = 'Wroclaw',
  Bialystok = 'Bialystok',
  Lublin = 'Lublin',
  Krakow = 'Krakow',
}

export enum DayOfWeek {
  Monday = "Monday",
  Tuesday = "Tuesday",
  Wednesday = "Wednesday",
  Thursday = "Thursday",
  Friday = "Friday",
  Saturday = "Saturday",
  Sunday = "Sunday"
}

export interface OpeningHours {
  dayOfWeek: DayOfWeek;
  isAvailable: boolean;
  start: string;
  end: string;
}

export interface ServiceHours {
  time: string;
}

export interface Address {
  city: string;
  zipCode: string;
  street: string;
  flatNumber: string;
  streetNumber: string;
}


export interface Service {
  serviceId: string;
  serviceName: string;
  price: number;
  duration: number;
  description: string;
  color: string;
  text: string;
}


export interface PagedResults<T> {
  totalRecords: number;
  results: T;
}

export interface Employee {
  employeeId: string;
  firstName: string;
  lastName: string;
  text: string;
  color: string;
}

export interface Client {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface Appointment {
  employeeId: string;
  serviceId: string;
  client: Client;
  startDate: Date;
  endDate: Date;
}
