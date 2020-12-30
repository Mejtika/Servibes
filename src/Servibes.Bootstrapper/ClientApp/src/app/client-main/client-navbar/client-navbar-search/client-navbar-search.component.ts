import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { CompanyDetails } from "src/app/shared/interfaces/company";
import { CompanyDataService } from "../../../data-service/company-data.service";
import { Router } from '@angular/router';

@Component({
  selector: "app-client-navbar-search",
  templateUrl: "./client-navbar-search.component.html",
  styleUrls: ["./client-navbar-search.component.css"],
})
export class ClientNavbarSearchComponent implements OnInit {
  companies$: Observable<CompanyDetails[]>;
  constructor(
    private companyService: CompanyDataService,
    private router: Router
    ) {}

  ngOnInit() {
    this.companies$ = this.companyService.companies$;
  }

  onChange($event) {
    return this.router.navigateByUrl(`client/companies/${$event.companyId}`);
  }
}
