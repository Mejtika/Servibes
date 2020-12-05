import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';

import { ICompany, Category, DayOfWeek, IAppointment, IService, IEmployee } from '../shared/interfaces/company';

//import 'rxjs/add/observable/of';

@Injectable()
export class MockDataService {
   private _companies: ICompany[] = [
     {
       companyId: "1",
       phoneNumber: '123456789',
       address: {
         city: "Warszawa",
         zipCode: "00-100",
         street: "Lubelska",
         flatNumber: "10",
         streetNumber: "11",
       },
       companyName: 'Norman Barber & Fryzjer',
       description: 'to nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.',
       category: Category.Barber,
       services: [
         { serviceId: "1", color:  "#cb6bb2", text: "Strzyżenie męskie", serviceName: "Strzyżenie męskie", price: 420, description: "Strzyżenie męskie, dokładnie tak jak tego chcesz", duration: 15},
         { serviceId: "2", color:  "#cb6bb2", text: "Strzyżenie damskie", serviceName: "Strzyżenie damskie", price: 550, description: "Przycinanie końcówek, albo na łyso", duration: 15},
         { serviceId: "3", color:  "#cb6bb2", text: "Farbowanie włosów", serviceName: "Farbowanie włosów", price: 40, description: "Tylko na różowo", duration: 15},
         { serviceId: "4", color:  "#cb6bb2", text: "Textowa usługa", serviceName: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: 15},
         { serviceId: "5", color:  "#cb6bb2", text: "Textowa usługa", serviceName: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: 15},
         { serviceId: "6", color:  "#cb6bb2", text: "Textowa usługa", serviceName: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: 15},
        ],
       employees: [
        { employeeId: 1, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
        { employeeId: 2, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
        { employeeId: 3, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
       ],
       coverPhoto: "assets/recommended/1.jpeg",
       openingHours: [
         { dayOfWeek: DayOfWeek.Monday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Tuesday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Wednesday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Thursday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Friday, start: "08:00", end: "16:00" }
       ]
     },
     {
       companyId: "2",
       phoneNumber: '123456789',
       address: {
         city: "Warszawa",
         zipCode: "00-100",
         street: "Lubelska",
         flatNumber: "10",
         streetNumber: "11",
       },
       companyName: 'Testowa firma',
       description: 'To nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.',
       category: Category.Barber,
       services: [
         { serviceId: "1", color: "#cb6bb2", text: "Strzyżenie męskie", serviceName: "Strzyżenie męskie", price: 420, description: "Strzyżenie męskie, dokładnie tak jak tego chcesz", duration: 15 },
         { serviceId: "2", color: "#cb6bb2", text: "Strzyżenie damskie", serviceName: "Strzyżenie damskie", price: 550, description: "Przycinanie końcówek, albo na łyso", duration: 15 },
         { serviceId: "3", color: "#cb6bb2", text: "Farbowanie włosów", serviceName: "Farbowanie włosów", price: 40, description: "Tylko na różowo", duration: 15 },
         { serviceId: "4", color: "#cb6bb2", text: "Textowa usługa", serviceName: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: 15 },
         { serviceId: "5", color: "#cb6bb2", text: "Textowa usługa", serviceName: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: 15 },
         { serviceId: "6", color: "#cb6bb2", text: "Textowa usługa", serviceName: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: 15 },
       ],
       employees: [
         { employeeId: 1, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
         { employeeId: 2, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
         { employeeId: 3, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
       ],
       coverPhoto: "assets/recommended/2.jpeg",
       openingHours: [
         { dayOfWeek: DayOfWeek.Monday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Tuesday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Wednesday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Thursday, start: "08:00", end: "16:00" },
         { dayOfWeek: DayOfWeek.Friday, start: "08:00", end: "16:00" }
       ]
     },
   ];

   private _appointments: IAppointment[] = [
     { 
        employeeId: "1",
        serviceId: "2",
        client: { firstName: 'Anna', lastName: 'Nowak' },
        startDate: new Date(2020, 9, 21, 18, 10),
        endDate: new Date(2020, 9, 21, 19, 10)
    },
    { 
        employeeId: "1",
        serviceId: "3",
        client: { firstName: 'Anna', lastName: 'Nowak' },
        startDate: new Date(2020, 9, 21, 19, 10),
        endDate: new Date(2020, 9, 21, 20, 10)
    },
    { 
        employeeId: "1",
        serviceId: "2",
        client: { firstName: 'Anna', lastName: 'Nowak' },
        startDate: new Date(2020, 9, 21, 20, 10),
        endDate: new Date(2020, 9, 21, 21, 10)
    },

    { 
      employeeId: "2",
      serviceId: "1",
      client: { firstName: 'Anna', lastName: 'Nowak' },
      startDate: new Date(2020, 9, 21, 21, 10),
      endDate: new Date(2020, 9, 21, 22, 10)
    },
    {
      employeeId: "3",
      serviceId: "3",
      client: { firstName: 'Ziomek', lastName: 'Ziomkowski' },
      startDate: new Date(2020, 9, 21, 10, 30),
      endDate: new Date(2020, 9, 21, 13, 30)
    },
    {
      employeeId: "2",
      serviceId: "3",
      client: { firstName: 'Kajetan', lastName: 'Kajetanowicz' },
      startDate: new Date(2020, 9, 21, 11, 30),
      endDate: new Date(2020, 9, 21, 15, 0)
    },
    {
      employeeId: "1",
      serviceId: "1",
      client: { firstName: 'Geralt', lastName: 'of Rivia' },
      startDate: new Date(2020, 9, 21, 9, 0),
      endDate: new Date(2020, 9, 21, 13, 30)
    },
    {
      employeeId: "1",
      serviceId: "2",
      client: { firstName: 'Anna', lastName: 'Kowalska' },
      startDate: new Date(2020, 9, 21, 13, 40),
      endDate: new Date(2020, 9, 21, 17, 0)
    }
  ];

  constructor() {}

  getCompany(id: string): Observable<ICompany> {
    const company = this._companies.filter(c => c.companyId == id);

    return of(company[0]);
  }

  public getCompanies(category: Category = Category.All) : Observable<ICompany[]> {
    if(category == Category.All)
      return of(this._companies);

    return of(this._companies.filter(c => c.category == category));
  }

  public getCategories(): string[] {
    return Object.keys(Category).map((key) => Category[key]);
  }

  public getAppointments(): Observable<IAppointment[]> {
    return of(this._appointments);
  }

  public getCompanyServices(id: string): Observable<IService[]> {
    return of(this._companies.filter(c => c.companyId == id)[0].services);
  }

  public getCompanyEmployeees(id: string): Observable<IEmployee[]> {
    return of(this._companies.filter(c => c.companyId == id)[0].employees); 
  }
}
