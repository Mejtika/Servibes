import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from '@angular/forms';
import { Category, City } from '../../shared/interfaces/company';

@Component({
  selector: "app-client-navbar",
  templateUrl: "./client-navbar.component.html",
  styleUrls: ["./client-navbar.component.css"],
})
export class ClientNavbarComponent implements OnInit {
  public categories: Category[];
  public cities: City[];
  public searchForm: FormGroup;
  public closeOnBodyClick: boolean = false;
  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.categories =  Object.keys(Category).map((key) => Category[key]);
    this.cities =  Object.keys(City).map((key) => City[key]);
    this.searchForm = this.fb.group({
      searchWhat: "",
      searchWhere: "",
      searchWhen: new Date()
    });
  }

  selectWhat(category: Category): void{
    this.searchForm.patchValue({
      searchWhat: category.toString()
    });
  }

  selectWhere(city: City): void{
    this.searchForm.patchValue({
      searchWhere: city.toString()
    });
  }

  saveForm(): void {
    console.log(JSON.stringify(this.searchForm.value));
  }
}

