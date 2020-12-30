import { Component, OnInit } from "@angular/core";
import { Category } from "src/app/shared/interfaces/company";

@Component({
  selector: "app-client-navbar",
  templateUrl: "./client-navbar.component.html",
  styleUrls: ["./client-navbar.component.css"],
})
export class ClientNavbarComponent implements OnInit {
  public categories: Category[];
  constructor() {}

  ngOnInit() {
    this.categories = Object.keys(Category).map((key) => Category[key]);
  }
}
