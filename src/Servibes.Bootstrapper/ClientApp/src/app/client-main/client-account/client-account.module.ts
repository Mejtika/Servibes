import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { ClientAccountComponent } from "./client-account.component";
import { ClientAppointmentsComponent } from "./client-appointments/client-appointments.component";
import { ClientReviewsComponent } from "./client-reviews/client-reviews.component";
import { ClientAppointmentComponent } from "./client-appointment/client-appointment.component";
import { ClientPaymentsComponent } from "./client-payments/client-payments.component";
import { ClientFavoritesComponent } from "./client-favorites/client-favorites.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ClientAppointmentService } from "./client-appointment.service";
import { ModalModule } from "ngx-bootstrap/modal";
import { ClientReviewsService } from "./client-reviews.service";
import { ClientPaymentsService } from "./client-payments.service";
import { ClientPaymentComponent } from "./client-payment/client-payment.component";

const routes: Routes = [
  {
    path: "",
    component: ClientAccountComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "appointments" },
      { path: "appointments", component: ClientAppointmentsComponent },
      { path: "appointments/:id", component: ClientAppointmentComponent },
      { path: "favorites", component: ClientFavoritesComponent },
      { path: "reviews", component: ClientReviewsComponent },
      { path: "payments", component: ClientPaymentsComponent },
    ],
  },
];

@NgModule({
  declarations: [
    ClientAppointmentsComponent,
    ClientAppointmentComponent,
    ClientFavoritesComponent,
    ClientReviewsComponent,
    ClientPaymentsComponent,
    ClientPaymentComponent,
    ClientAccountComponent
  ],
  imports: [
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forChild(routes),
    ModalModule.forChild()
  ],
  providers: [
    ClientAppointmentService,
    ClientReviewsService,
    ClientPaymentsService
  ],
})
export class ClientAccountModule {}
