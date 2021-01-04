import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { IProfile, IService } from '../../models';
import { ProfileService, ServicesService } from '../../services';

@Component({
    selector: 'services',
    // changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './services.component.html',
    styleUrls: ['./services.component.scss'],
})
export class ServicesComponent extends BaseForm implements OnInit {
    private profile: IProfile;
    public services: IService[];

    constructor(
        private profileService: ProfileService,
        private servicesService: ServicesService,
        private cd: ChangeDetectorRef,
        private toastr: ToastrService) {

        super();
    }
    
    ngOnInit() {
        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            this.getServicesList();
        });
    }

    getServicesList() {
        this.servicesService.getAllServices(this.profile.companyId).subscribe(services => {
            this.services = services;

            // this.cd.markForCheck();
        });
    }

    deleteService(serviceId: string) {
        this.servicesService.removeService(this.profile.companyId, serviceId).subscribe(() => {
            this.toastr.success("Service removed successfully.");

            this.getServicesList();
        });
    }
}
