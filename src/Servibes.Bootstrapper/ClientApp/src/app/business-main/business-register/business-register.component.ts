import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';

import { BaseForm } from 'src/app/shared/others/BaseForm';
import { TimeArray } from './../../shared/others/time-array';
import { ProfileService } from '../business-services/profile-service';
import { BusinessProfile } from '../models/BusinessProfile';
import { cwd } from 'process';

@Component({
    selector: 'business-register',
    templateUrl: './business-register.component.html',
    styleUrls: ['./business-register.component.css']
})
export class BusinessRegisterComponent extends BaseForm {
    categories = ["Fryzjer", "Barber", "Masaz", "Makeup"];
    public weekDays: string[] = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    public times: string[] = this.timeArray.generate(15);


  constructor(private formBuilder: FormBuilder,
    private timeArray: TimeArray,
    private profileService: ProfileService) {
        super();
    }

    get employees() { return this.form.controls.employees as FormArray; }
    get openingHours() { return this.form.controls.openingHours as FormArray; }
    get services() { return this.form.controls.services as FormArray; }

    ngOnInit() {
        this.form = this.formBuilder.group({
            businessName: new FormControl('', Validators.required),
            employees: new FormArray([]),
            businessPhoneNumber: new FormControl('', Validators.required),
            category: new FormControl('', Validators.required),
            location: new FormControl('', Validators.required),
            coverPhoto: new FormControl(''),
            openingHours: new FormArray([]),
            services: new FormArray([])
        });

        //Adding initial form fields
        this.addEmployeeForm();
        this.addOpeningHoursForm();
        this.addService();
    }

    addEmployeeForm() {
        this.employees.push(this.formBuilder.group({
            firstName: new FormControl('', Validators.required),
            lastName: new FormControl('', Validators.required)
        }));

        console.log('Added employee to form');
    }

    removeEmployeeForm(index: number) {
        this.employees.removeAt(index);
    }

    addOpeningHoursForm() {
        //ToDo: Conditional validations not working properly

        for (let index = 0; index < 7; index++) {
            this.openingHours.push(this.formBuilder.group({
                dayOfWeek: new FormControl(this.weekDays[index]),
                isActive: new FormControl(false),
                openHour: new FormControl('08:00', [this.conditionalValidator(() => this.openingHours.controls[index].get('isActive').value, Validators.required)]),
                closeHour: new FormControl('16:00', [this.conditionalValidator(() => this.openingHours.controls[index].get('isActive').value, Validators.required)]),
            }));
        }
    }

    addService() {
        this.services.push(this.formBuilder.group({
            serviceName: new FormControl(''),
            price: new FormControl(''),
            description: new FormControl(''),
            duration: new FormControl('')
        }))
    }

    removeService(index: number) {
        this.services.removeAt(index);
    }

    changedCategory(e) {
        this.getControl('category').setValue(e.target.value);
    }

    openHoursChanged(e, index: number) {
        this.openingHours.controls[index].get('open').setValue(e.target.value);
    }

    closeHoursChanged(e, index: number) {
        this.openingHours.controls[index].get('close').setValue(e.target.value);
    }

    onSubmit() {
      console.log(this.form.getRawValue());
      let formData = this.form.getRawValue();

      const formValue: BusinessProfile = Object.assign(
        this.form.value
      )

      this.profileService.addBusinessProfile(formValue).subscribe(profile => {
        console.log('Posted profile: ', profile);
      });
    }
}
