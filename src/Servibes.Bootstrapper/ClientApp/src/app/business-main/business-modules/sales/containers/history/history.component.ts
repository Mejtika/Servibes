import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { SalesService } from "../../services";
import { AppointmentDto } from "../../models/sales.model";

@Component({
  selector: "history",
  templateUrl: "./history.component.html",
  styleUrls: ["./history.component.scss"],
})
export class HistoryComponent implements OnInit {
  appointments: AppointmentDto[];
  constructor(private salesService: SalesService) {}
  ngOnInit() {
    this.salesService
      .getAppointmentsHistory()
      .subscribe(result => this.appointments = result);
  }
}
