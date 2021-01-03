import { ChangeDetectorRef, Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { TimeArray } from 'src/app/shared/others/time-array';
import { IHoursRange, IProfile } from '../../models';
import { ProfileService } from '../../services/profile.service';

@Component({
    selector: 'opening-hours',
    templateUrl: './opening-hours.component.html',
    styleUrls: ['./opening-hours.component.scss']
})
export class OpeningHoursComponent extends BaseForm {
    private profile: IProfile;
    public openingHours: IHoursRange[];
    public weekDays: string[] = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    public times: string[] = this.timeArray.generate(15);

    get openingHoursForm() { return this.form.controls.openingHours as FormArray; }

    constructor(
        private profileService: ProfileService,
        private timeArray: TimeArray,
        private formBuilder: FormBuilder,
        private toastr: ToastrService,
        private cd: ChangeDetectorRef) {
            super();
    }

    ngOnInit() {
        this.form = this.formBuilder.group({
            openingHours: new FormArray([]),
        });

        this.getData();
    }

    getData() {
        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            this.profileService.getOpeningHours(this.profile.companyId).subscribe(openingHours => {
                this.openingHours = openingHours;

                this.cd.markForCheck();

                if(this.openingHoursForm.controls.length != 7)
                    this.addOpeningHoursForm();
            });
        });
    }

    addOpeningHoursForm() {
        for (let index = 1; index <= 6; index++) {
            this.openingHoursForm.push(this.formBuilder.group({
                dayOfWeek: new FormControl(index),
                isAvailable: new FormControl(this.openingHours[index].isAvailable),
              start: new FormControl(this.openingHours[index].start),
              end: new FormControl(this.openingHours[index].end),
            }));
        }

        this.openingHoursForm.push(this.formBuilder.group({
          dayOfWeek: new FormControl(0),
          isAvailable: new FormControl(this.openingHours[0].isAvailable),
          start: new FormControl(this.openingHours[0].start),
          end: new FormControl(this.openingHours[0].end),
        }));
    }

    openHoursChanged(e, index: number) {
        this.openingHoursForm.controls[index].get('start').setValue(e.target.value);
    }

    closeHoursChanged(e, index: number) {
        this.openingHoursForm.controls[index].get('end').setValue(e.target.value);
    }

    onSubmit() {
        const formValues = this.form.getRawValue();

        console.log('Form values: ', formValues);

        const body = {
            openingHours: formValues.openingHours,
            adjustEmployeeWorkingHours: true
        }

        this.profileService.changeOpeningHours(this.profile.companyId, body).subscribe(result => {


            this.toastr.success("Opening hours changed successfully.");
        });
    }
}