import { Component, OnInit } from "@angular/core";
import { Category } from "src/app/shared/interfaces/company";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";

@Component({
  selector: "app-client-navbar",
  templateUrl: "./client-navbar.component.html",
  styleUrls: ["./client-navbar.component.css"],
})
export class ClientNavbarComponent implements OnInit {
  public categories$: Observable<Category[]>;
  constructor(private httpClient: HttpClient) {}

  ngOnInit() {
    this.categories$ = this.httpClient.get<Category[]>(`${environment.backendEndpoint}companies/categories`);
  }
}
