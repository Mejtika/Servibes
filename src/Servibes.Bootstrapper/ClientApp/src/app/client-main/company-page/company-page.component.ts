import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { BsModalService } from "ngx-bootstrap/modal";
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { EmployeeDataService } from "src/app/data-service/employee-data.service";
import { ServicesDataService } from "src/app/data-service/services-data.service";
import {
  Company,
  Service,
  Employee,
  ServiceHours,
  ReviewDetails,
} from "src/app/shared/interfaces/company";
import { forkJoin } from "rxjs";
import { ClientReservationComponent } from "./client-reservation/client-reservation.component";
import { ClientFavoritesService } from "../client-account/client-favorites.service";
import { ToastrService } from "ngx-toastr";
import {
  ClientReviewsService,
  ReviewSummaryDto,
} from "../client-account/client-reviews.service";
import { IImage } from "src/app/business-main/business-modules/profile/models";

@Component({
  selector: "company-page",
  templateUrl: "company-page.component.html",
  styleUrls: ["./company-page.component.css"],
})
export class CompanyPageComponent {
  company: Company;
  public weekDays: string[] = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];
  public selectedService: Service;
  public selectedDate: Date = new Date();
  public serviceEmployees: Employee[];
  public selectedEmployee: Employee;
  public serviceAvailableHours: ServiceHours[];
  public isFavoriteCompany: boolean;
  public reviewsSummary: ReviewSummaryDto;
  public reviews: ReviewDetails[];
  public imageSrc: string;

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

    this.favoritesService
    .favoritesCompanies$
    .subscribe(companies => 
      this.isFavoriteCompany = companies
      .find(company => company.companyId == this.company.companyId) !== undefined
    );
    
    this.reviewsService
    .getCompanyReviews(companyId)
    .subscribe(result => this.reviews = result);

    this.reviewsService
      .getReviewsSummary(companyId)
      .subscribe(result => this.reviewsSummary = result);
  }

  addToFavorites() {
    this.favoritesService
      .addToFavorites(this.company.companyId)
      .subscribe(_ => {
        this.toastr.success("Company was successfully added to favorites!");
        this.refreshPage();
      });
  }

  deleteFromFavorites() {
    this.favoritesService
      .deleteFromFavorites(this.company.companyId)
      .subscribe(_ => {
        this.toastr.success("Company was successfully removed from favorites!");
        this.refreshPage();
      });
  }

  loadCompanyData(companyId: string) {
    let company$ = this.companyDataService.getCompanyById(companyId);
    let openingHours$ = this.companyDataService.getCompanyOpeningHours(companyId);
    let services$ = this.servicesDataService.getAllCompanyServices(companyId);
    let employees$ = this.employeeDataService.getAllCompanyEmployees(companyId);

    forkJoin([company$, openingHours$, services$, employees$]).subscribe(
      ([company, openingHours, services, employees]) => {
        company.openingHours = openingHours;
        company.services = services;
        company.employees = employees;
        this.company = company;

        this.companyDataService.getImage(this.company.coverPhotoId).subscribe(image => {
          this.imageSrc = "data:" + image.fileType + ";base64," + image.data;
        });
      }
    );
  }

  public selectService(service: Service) {
    this.selectedService = service;
    const initialState = {
      service: service,
      company: this.company,
    };
    this.modalService.show(ClientReservationComponent, { initialState });
  }

  refreshPage() {
    this.router.navigateByUrl(`/client/companies/${this.company.companyId}`);
  }
}
