import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { IProfile } from '../../../profile/models';
import { ProfileService } from '../../../profile/services';
import { IClient } from '../../models/client.model';
import { ClientBaseService } from '../../services';
import { ClientReservationComponent } from './../client-reservation/client-reservation.component';

@Component({
    selector: 'client-base',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './client-base.component.html',
    styleUrls: ['./client-base.component.scss'],
})
export class ClientBaseComponent implements OnInit {
    private profile: IProfile;
    public clients: IClient[];
    public selectedClient: IClient = null;

    constructor(
        private clientBaseService: ClientBaseService,
        private profileService: ProfileService,
        private cd: ChangeDetectorRef,
        private modalService: BsModalService) {

    }

    ngOnInit() {
        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            this.clientBaseService.getClients(this.profile.companyId).subscribe(clients => {
                this.clients = clients;

                this.cd.markForCheck();
            });
        });
    }

    selectClient(client: IClient) {
        this.selectedClient = client;
    }

    createAppointment(client: IClient) {
        const initialState = {
            client: client,
            companyId: this.profile.companyId,
          };

        this.modalService.show(ClientReservationComponent, { class: 'modal-dialog-centered', initialState });
    }
}
