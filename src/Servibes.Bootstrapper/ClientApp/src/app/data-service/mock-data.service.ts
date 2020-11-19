import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';

import { ICompany, Category, DayOfTheWeek, IAppointment, IService, IEmployee } from '../shared/interfaces/company';

//import 'rxjs/add/observable/of';

@Injectable()
export class MockDataService {
   private _companies: ICompany[] = [
     {
       id: 1,
         companyDetails: { 
             phone: '123456789',
            address: {
                street: 'Warszawska',
                city: 'Lublin',
                localNumber: '5'
            },
            companyName: 'Norman Barber & Fryzjer',
            description: 'to nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.'
        },
       category: Category.Barber,
       services: [
         { id: 1, color:  "#cb6bb2", text: "Strzyżenie męskie", name: "Strzyżenie męskie", price: 420, description: "Strzyżenie męskie, dokładnie tak jak tego chcesz", duration: "15min"},
         { id: 2, color:  "#cb6bb2", text: "Strzyżenie damskie", name: "Strzyżenie damskie", price: 550, description: "Przycinanie końcówek, albo na łyso", duration: "15min"},
         { id: 3, color:  "#cb6bb2", text: "Farbowanie włosów", name: "Farbowanie włosów", price: 40, description: "Tylko na różowo", duration: "15min"},
         { id: 4, color:  "#cb6bb2", text: "Textowa usługa", name: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: "15min"},
         { id: 5, color:  "#cb6bb2", text: "Textowa usługa", name: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: "15min"},
         { id: 6, color:  "#cb6bb2", text: "Textowa usługa", name: "Testowa usługa", price: 99.99, description: "Skończyły się pomysły :(", duration: "15min"},
        ],
       employees: [
        { id: 1, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
        { id: 2, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
        { id: 3, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
       ],
       coverImg: "assets/recommended/1.jpeg",
       dayAvailability: [
           { dayOfTheWeek: DayOfTheWeek.Monday, startTime: 28800, endTime: 57600} //8:00 <-> 16:00
       ]
     },

     {
      id: 2,
        companyDetails: { 
            phone: '123456789',
           address: {
               street: 'Wólczańska',
               city: 'Łódź',
               localNumber: '67'
           },
           companyName: 'Fizjostrefa',
           description: 'to nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.'
       },
      category: Category.Masaz,
      services: [
        { id: 5, color:  "#cb6bb2", text: "Masaż leczniczy", name: "Masaż leczniczy", price: 420, description: "Masu masu masu", duration: "15min"},
        { id: 6, color:  "#cb6bb2", text: "Masaż relaksacyjny", name: "Masaż relaksacyjny", price: 20, description: "Masowanko :)", duration: "15min"},
      ],
      employees: [
        { id: 4, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
        { id: 5, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
        { id: 6, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
      ],
      coverImg: "assets/recommended/2.PNG",
      dayAvailability: [
          { dayOfTheWeek: DayOfTheWeek.Monday, startTime: 800, endTime: 1600}
      ]
    },

    {
      id: 3,
        companyDetails: { 
            phone: '123456789',
           address: {
               street: 'Wyszogrodzka',
               city: 'Warszawa',
               localNumber: '4'
           },
           companyName: 'Rzęsownia',
           description: 'to nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.'
       },
      category: Category.Makeup,
      services: [
        { id: 7, color:  "#cb6bb2", text: "Założenie rzęs Black Magic Volume", name: "Założenie rzęs Black Magic Volume", price: 30, description: "Założymy i będzie gites", duration: "15min" },
        { id: 8, color:  "#cb6bb2", text: "Przedłużanie rzęs", name: "Przedłużanie rzęs", price: 20, description: "Rzęsy aż do nieba!", duration: "15min"},
        { id: 9, color:  "#cb6bb2", text: "Uzupełnianie rzęs", name: "Uzupełnianie rzęs", price: 0, description: "Jak brakuje to dokleimy", duration: "15min"},
      ],
      employees: [
        { id: 7, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
        { id: 8, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
        { id: 9, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
      ],
      coverImg: "assets/recommended/3.PNG",
      dayAvailability: [
          { dayOfTheWeek: DayOfTheWeek.Monday, startTime: 800, endTime: 1600}
      ]
    },

    {
      id: 4,
        companyDetails: { 
            phone: '123456789',
           address: {
               street: 'Nowogrodzka',
               city: 'Warszawa',
               localNumber: '42'
           },
           companyName: 'Atelier Śródmieście',
           description: 'to nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.'
       },
      category: Category.Fryzjer,
      services: [
        { id: 10, color:  "#cb6bb2", text: "Strzyżenie męskie", name: "Strzyżenie męskie", price: 420, description: "Strzyżenie męskie, dokładnie tak jak tego chcesz", duration: "15min"},
        { id: 11, color:  "#cb6bb2", text: "Strzyżenie damskie", name: "Strzyżenie damskie", price: 550, description: "Przycinanie końcówek, albo na łyso", duration: "15min"},
        { id: 12, color:  "#cb6bb2", text: "Farbowanie włosów", name: "Farbowanie włosów", price: 40, description: "Tylko na różowo", duration: "15min"},
        { id: 13, color:  "#cb6bb2", text: "Usługa 4", name: "Usługa 4", price: 99.99, description: "Skończyły się pomysły :(", duration: "15min"},
      ],
      employees: [
        { id: 10, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
        { id: 11, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
        { id: 12, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
      ],
      coverImg: "assets/recommended/4.jpg",
      dayAvailability: [
          { dayOfTheWeek: DayOfTheWeek.Monday, startTime: 800, endTime: 1600}
      ]
    },

    {
      id: 5,
        companyDetails: { 
            phone: '123456789',
           address: {
               street: 'Pokorna',
               city: 'Warszawa',
               localNumber: '2'
           },
           companyName: 'Niepokorni Ona & On',
           description: 'to nowe miejsce na mapie Wrocławia. Męski salon fryzjerski - BlackBeard połączony jest z niespotykanymi w naszym kraju markami alkoholi oraz cygarami. BlackBeard ma w swoim asortymencie wysokiej jakości rumy, giny, wina, szampany oraz whisky. Salon BlackBeard to klimatyczny salon z luksusowymi alkoholami, miejsce, w którym można znakomicie spędzić czas w przyjaznej atmosferze i niezwykłym wnętrzu, a także Barber Shop. To właśnie w lokalu mieszczącym się przy ul. Tęczowej 57 we Wrocławiu podczas specjalnych i kameralnych degustacji dowiemy się praktycznie wszystkiego o historii rumu, tym jak produkuje się najznakomitsze alkohole na świecie, a także skosztujemy unikatowych i limitowanych oraz specjalnych edycji rumu, whisky czy koniaku.'
       },
      category: Category.Fryzjer,
      services: [
        { id: 14, color:  "#cb6bb2", text: "Strzyżenie męskie", name: "Strzyżenie męskie", price: 420, description: "Strzyżenie męskie, dokładnie tak jak tego chcesz", duration: "15min"},
        { id: 15, color:  "#cb6bb2", text: "Strzyżenie damskie", name: "Strzyżenie damskie", price: 550, description: "Przycinanie końcówek, albo na łyso", duration: "15min"},
        { id: 16, color:  "#cb6bb2", text: "Farbowanie włosów", name: "Farbowanie włosów", price: 40, description: "Tylko na różowo", duration: "15min"},
        { id: 17, color:  "#cb6bb2", text: "Usługa 4", name: "Usługa 4", price: 99.99, description: "Skończyły się pomysły :(", duration: "15min"},
      ],
      employees: [
        { id: 13, firstName: "Mateuszek", lastName: "Mateuszkowy", text: "Mateuszek Mateuszkowy", color: "#187bcd" },
        { id: 14, firstName: "Arkadiuszek", lastName: "Arkadiuszkowy", text: "Arkadiuszek Arkadiuszkowy", color: "#07da63" },
        { id: 15, firstName: "Krasnoludek", lastName: "Robotaju", text: "Krasnoludek Robotaju", color: "#ed2939" },
      ],
      coverImg: "assets/recommended/5.jpeg",
      dayAvailability: [
          { dayOfTheWeek: DayOfTheWeek.Monday, startTime: 800, endTime: 1600}
      ]
    }
   ];

   private _appointments: IAppointment[] = [
     { 
        employeeId: 1,
        serviceId: 2,
        client: { firstName: 'Anna', lastName: 'Nowak' },
        startDate: new Date(2020, 9, 21, 18, 10),
        endDate: new Date(2020, 9, 21, 19, 10)
    },
    { 
        employeeId: 1,
        serviceId: 3,
        client: { firstName: 'Anna', lastName: 'Nowak' },
        startDate: new Date(2020, 9, 21, 19, 10),
        endDate: new Date(2020, 9, 21, 20, 10)
    },
    { 
        employeeId: 1,
        serviceId: 2,
        client: { firstName: 'Anna', lastName: 'Nowak' },
        startDate: new Date(2020, 9, 21, 20, 10),
        endDate: new Date(2020, 9, 21, 21, 10)
    },

    { 
      employeeId: 2,
      serviceId: 1,
      client: { firstName: 'Anna', lastName: 'Nowak' },
      startDate: new Date(2020, 9, 21, 21, 10),
      endDate: new Date(2020, 9, 21, 22, 10)
    },
    {
      employeeId: 3,
      serviceId: 3,
      client: { firstName: 'Ziomek', lastName: 'Ziomkowski' },
      startDate: new Date(2020, 9, 21, 10, 30),
      endDate: new Date(2020, 9, 21, 13, 30)
    },
    {
      employeeId: 2,
      serviceId: 3,
      client: { firstName: 'Kajetan', lastName: 'Kajetanowicz' },
      startDate: new Date(2020, 9, 21, 11, 30),
      endDate: new Date(2020, 9, 21, 15, 0)
    },
    {
      employeeId: 1,
      serviceId: 1,
      client: { firstName: 'Geralt', lastName: 'of Rivia' },
      startDate: new Date(2020, 9, 21, 9, 0),
      endDate: new Date(2020, 9, 21, 13, 30)
    },
    {
      employeeId: 1,
      serviceId: 2,
      client: { firstName: 'Anna', lastName: 'Kowalska' },
      startDate: new Date(2020, 9, 21, 13, 40),
      endDate: new Date(2020, 9, 21, 17, 0)
    }
  ];

  constructor() {}

  getCompany(id: number): Observable<ICompany> {
    const company = this._companies.filter(c => c.id == id);

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

  public getCompanyServices(id: number): Observable<IService[]> {
    return of(this._companies.filter(c => c.id == id)[0].services);
  }

  public getCompanyEmployeees(id: number): Observable<IEmployee[]> {
    return of(this._companies.filter(c => c.id == id)[0].employees); 
  }
}
