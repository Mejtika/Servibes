import { Component, Input } from "@angular/core";
import { CompanyDataService } from "src/app/data-service/company-data.service";
import { Company } from "src/app/shared/interfaces/company";

@Component({
  selector: "single-company",
  templateUrl: "./single-company.component.html",
  styleUrls: ["./single-company-component.css"],
})
export class SingleCompanyComponent {
  @Input() company: Company;
  public imageSrc: string;

  constructor(private companyDataService: CompanyDataService) {}

  ngOnInit() {
    this.companyDataService.getImage(this.company.coverPhotoId).subscribe(image => {
      this.imageSrc = "data:" + image.fileType + ";base64," + image.data;
    });
  }
}
