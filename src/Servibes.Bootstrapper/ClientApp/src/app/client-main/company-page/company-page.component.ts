import { Component, ViewChild, ElementRef } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { BsModalService } from "ngx-bootstrap/modal";
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { EmployeeDataService } from "src/app/data-service/employee-data.service";
import { ServicesDataService } from "src/app/data-service/services-data.service";
import {
  ICompany,
  IService,
  IEmployee,
  IServiceHours,
} from "src/app/shared/interfaces/company";
import { forkJoin } from "rxjs";
import { ClientReservationComponent } from "./client-reservation/client-reservation.component";
import { ClientFavoritesService } from "../client-account/client-favorites.service";
import { ToastrService } from "ngx-toastr";
import { ClientReviewsService, ReviewSummaryDto } from '../client-account/client-reviews.service';
import { ReviewDetails } from '../client-account/client-reviews/client-reviews.component';

@Component({
  selector: "company-page",
  templateUrl: "company-page.component.html",
  styleUrls: ["./company-page.component.css"],
})
export class CompanyPageComponent {
  company: ICompany;
  public weekDays: string[] = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];
  public selectedService: IService;

  public selectedDate: Date = new Date();

  public serviceEmployees: IEmployee[];
  public selectedEmployee: IEmployee;

  public serviceAvailableHours: IServiceHours[];

  public isFavoriteCompany: boolean;

  public reviewsSummary: ReviewSummaryDto;
  public reviews: ReviewDetails[];

  constructor(
    private companyDataService: CompanyDataService,
    private servicesDataService: ServicesDataService,
    private employeeDataService: EmployeeDataService,
    private favoritesService: ClientFavoritesService,
    private reviewsService: ClientReviewsService,
    private activatedRoute: ActivatedRoute,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
  }

  ngOnInit() {
    const companyId: string = this.activatedRoute.snapshot.paramMap.get("id");
    this.loadCompanyData(companyId);

    this.favoritesService.favoritesCompanies$.subscribe((companies) => {
      this.isFavoriteCompany = companies
      .find((c) => c.companyId == this.company.companyId) !== undefined;
    });
  }

  addToFavorites() {
    this.favoritesService
      .addToFavorites(this.company.companyId)
      .subscribe((_) => {
        this.toastr.success("Company was successfully added to favorites!");
        this.refreshPage();
      });
  }

  deleteFromFavorites() {
    this.favoritesService
      .deleteFromFavorites(this.company.companyId)
      .subscribe((_) => {
        this.toastr.success("Company was successfully removed from favorites!");
        this.refreshPage();
      });
  }

  loadCompanyData(companyId: string) {
    let company = this.companyDataService.getCompanyById(companyId);
    let openingHours = this.companyDataService.getCompanyOpeningHours(
      companyId
    );
    let services = this.servicesDataService.getAllCompanyServices(companyId);
    let employees = this.employeeDataService.getAllCompanyEmployees(companyId);

    forkJoin([company, openingHours, services, employees]).subscribe(
      (result) => {
        result[0].openingHours = result[1];
        result[0].services = result[2];
        result[0].employees = result[3];

        this.company = result[0];

        this.reviewsService.getReviewsSummary(this.company.companyId)
        .subscribe(result => this.reviewsSummary = result);

        this.reviewsService.getCompanyReview(this.company.companyId)
        .subscribe(result => this.reviews = result)
      }
    );
    
  }

  public selectService(service: IService) {
    this.selectedService = service;

    const initialState = {
      service: service,
      company: this.company,
    };

    console.log("initialState", initialState);

    this.modalService.show(ClientReservationComponent, { initialState });
  }
  refreshPage() {
    this.router.navigateByUrl(`/client/companies/${this.company.companyId}`);
  }
}
