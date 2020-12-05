export interface ICompany {
  companyId: string;
  companyName: string;
  phoneNumber: string;
  address: IAddress;
  category: string;
  description: string;
  coverPhoto: string;

  openingHours: IOpeningHours[];
  employees: IEmployee[];
  services: IService[];
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

export interface IOpeningHours {
  dayOfWeek: DayOfWeek;
  start: string;
  end: string;
}

export interface IAddress {
  city: string;
  zipCode: string;
  street: string;
  flatNumber: string;
  streetNumber: string;
}


export interface IService {
  serviceId: string;
  serviceName: string;
  price: number;
  duration: number;
  description: string;
  color: string;
  text: string;
}


export interface IPagedResults<T> {
  totalRecords: number;
  results: T;
}

export interface IEmployee {
  employeeId: number;
  firstName: string;
  lastName: string;
  text: string;
  color: string;
}

export interface IClient {
  firstName: string;
  lastName: string;
}

export interface IAppointment {
  employeeId: string;
  serviceId: string;
  client: IClient;
  startDate: Date;
  endDate: Date;
}
