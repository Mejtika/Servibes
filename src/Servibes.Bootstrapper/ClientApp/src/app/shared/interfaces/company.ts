export interface ICompany {
  id: number;
  coverImg: string;
  category: string;
  companyDetails: ICompanyDetails;
  dayAvailability: IDayAvailability[];
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

export enum DayOfTheWeek {
  Monday = "Monday",
  Tuesday = "Tuesday",
  Wednesday = "Wednesday",
  Thursday = "Thursday",
  Friday = "Friday",
  Saturday = "Saturday",
  Sunday = "Sunday"
}

export interface IDayAvailability{
  dayOfTheWeek: DayOfTheWeek;
  startTime: number;
  endTime: number;
}

export interface IAddress{
  street: string;
  city: string;
  localNumber: string;
}

export interface ICompanyDetails{
  phone: string;
  address: IAddress;
  companyName:string;
  description: string;
}


export interface IService {
  id: number;
  name: string;
  price: number;
  description: string;
  duration: string;
  color: string;
  text: string;
}


export interface IPagedResults<T> {
  totalRecords: number;
  results: T;
}

export interface IEmployee {
  id: number;
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
  employeeId: number;
  serviceId: number;
  client: IClient;
  startDate: Date;
  endDate: Date;
}