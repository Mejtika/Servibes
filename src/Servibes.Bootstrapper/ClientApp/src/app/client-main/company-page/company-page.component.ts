import { Component, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";

import { CompanyDataService } from 'src/app/data-service/company-data.servce';
import { EmployeeDataService } from 'src/app/data-service/employee-data.service';
import { ServicesDataService } from 'src/app/data-service/services-data.service';

import { ICompany, IService, IEmployee, IServiceHours } from 'src/app/shared/interfaces/company';
import { forkJoin } from 'rxjs';
import { ModalComponent } from '../../shared/components/modal/modal.component';
import { ClientReservationComponent } from './client-reservation/client-reservation.component';
import { ConfirmModalComponent } from 'src/app/shared/components/confirm-modal/confirm-modal.component';


@Component({
    selector: 'company-page',
    templateUrl: 'company-page.component.html',
    styleUrls: ['./company-page.component.css']
})
export class CompanyPageComponent {
    company: ICompany;
    public weekDays: string[] = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
     public selectedService: IService;

     public selectedDate: Date = new Date();

     public serviceEmployees: IEmployee[];
     public selectedEmployee: IEmployee;

    public serviceAvailableHours: IServiceHours[];

    private modalRef: BsModalRef;

    @ViewChild("modal") modal: ModalComponent;

    constructor(
        private companyDataService: CompanyDataService,
        private servicesDataService: ServicesDataService,
        private employeeDataService: EmployeeDataService,
        private activatedRoute: ActivatedRoute,
        private modalService: BsModalService) {

    }

    ngOnInit() {
        const companyId: string = this.activatedRoute.snapshot.paramMap.get('id');

        this.loadCompanyData(companyId);
    }

    loadCompanyData(companyId: string) {
        let company = this.companyDataService.getCompanyById(companyId);
        let openingHours = this.companyDataService.getCompanyOpeningHours(companyId);
        let services = this.servicesDataService.getAllCompanyServices(companyId);
        let employees = this.employeeDataService.getAllCompanyEmployees(companyId);

        forkJoin([company, openingHours, services, employees]).subscribe(result => {
            result[0].openingHours = result[1];
            result[0].services = result[2];
            result[0].employees = result[3];

            this.company = result[0];
        });
  }

    public selectService(service: IService) {
      this.selectedService = service;

      const initialState = {
        service: service,
        company: this.company
      };

      console.log('initialState', initialState);

      this.modalRef = this.modalService.show(ClientReservationComponent, { initialState });

      this.modalRef.onHide.asObservable().subscribe(x => {
        this.modalService.show(ConfirmModalComponent);
      });
    }
}
