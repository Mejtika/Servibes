import { ChangeDetectorRef, Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { TimeArray } from 'src/app/shared/others/time-array';
import { IHoursRange, IProfile } from '../../models';
import { EmployeeService, ProfileService } from '../../services';

@Component({
    selector: 'employee-working-hours',
    templateUrl: './employee-working-hours.component.html',
    styleUrls: ['./employee-working-hours.component.scss']
})
export class EmployeeWorkingHoursComponent extends BaseForm {
    private profile: IProfile;
    public workingHours: IHoursRange[];
    private employeeId: string;
    public weekDays: string[] = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    public times: string[] = this.timeArray.generate(15);

    get workingHoursForm() { return this.form.controls.workingHours as FormArray; }

    constructor(
        private profileService: ProfileService,
        private employeeService: EmployeeService,
        private activatedRoute: ActivatedRoute,
        private cd: ChangeDetectorRef,
        private formBuilder: FormBuilder,
        private toastr: ToastrService,
        private timeArray: TimeArray
    ) {
        super();
        
    }

    ngOnInit() {
        this.form = this.formBuilder.group({
            employeeId: new FormControl(''),
            workingHours: new FormArray([]),
        });

        this.getData();
    }

    getData() {
        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

            this.form.controls.employeeId.setValue(this.employeeId);

            this.employeeService.getWorkingHours(this.profile.companyId, this.employeeId).subscribe(workingHours => {
                this.workingHours = workingHours;

                this.cd.markForCheck();

                if(this.workingHoursForm.controls.length != 7)
                    this.addWorkingHoursForm();
            });
        });
    }

    addWorkingHoursForm() {
        for (let index = 1; index <= 6; index++) {
            this.workingHoursForm.push(this.formBuilder.group({
                dayOfWeek: new FormControl(index),
                isAvailable: new FormControl(this.workingHours[index].isAvailable),
              start: new FormControl(this.workingHours[index].start),
              end: new FormControl(this.workingHours[index].end),
            }));
        }

        this.workingHoursForm.push(this.formBuilder.group({
          dayOfWeek: new FormControl(0),
          isAvailable: new FormControl(this.workingHours[0].isAvailable),
          start: new FormControl(this.workingHours[0].start),
          end: new FormControl(this.workingHours[0].end),
        }));
    }

    startHoursChanged(e, index: number) {
        this.workingHoursForm.controls[index].get('start').setValue(e.target.value);
    }

    endHoursChanged(e, index: number) {
        this.workingHoursForm.controls[index].get('end').setValue(e.target.value);
    }

    onSubmit() {
        const formValues = this.form.getRawValue();

        console.log('Form values: ', formValues);

        const body = {
            workingHours: formValues.workingHours
        }

        this.employeeService.changeWorkingHours(this.profile.companyId, this.employeeId, body).subscribe(result => {
            this.toastr.success("Working hours changed successfully.");
        });
    }
}