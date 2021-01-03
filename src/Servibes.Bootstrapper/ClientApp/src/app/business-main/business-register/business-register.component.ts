import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';

import { BaseForm } from 'src/app/shared/others/BaseForm';
import { TimeArray } from './../../shared/others/time-array';
import { ProfileService } from '../business-services/profile-service';
import { BusinessProfile } from '../models/BusinessProfile';
import { Router } from '@angular/router';
import { CategoriesService } from '../business-modules/profile/services';
import { ICategory } from '../business-modules/profile/models';

@Component({
    selector: 'business-register',
    templateUrl: './business-register.component.html',
    styleUrls: ['./business-register.component.css']
})
export class BusinessRegisterComponent extends BaseForm {
    //categories = ["Fryzjer", "Barber", "Masaz", "Makeup"];
    categories: ICategory[];
    public weekDays: string[] = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    public times: string[] = this.timeArray.generateHours(15);
    public servicetimes: string[] = this.timeArray.generateMinutes(15, 0, 240);
    private coverPhoto: File;

    public step: number = 1;
    public maxStep = 4;


  constructor(private formBuilder: FormBuilder,
    private timeArray: TimeArray,
    private profileService: ProfileService,
    private router: Router,
    private categoriesService: CategoriesService) {
        super();
    }

    get employees() { return this.form.controls.employees as FormArray; }
    get openingHours() { return this.form.controls.openingHours as FormArray; }
    get services() { return this.form.controls.services as FormArray; }
    get address() { return this.form.controls.address as FormGroup };

    ngOnInit() {
      this.categoriesService.getCategories().subscribe(categories => {
        this.categories = categories;
      });

        this.form = this.formBuilder.group({
            companyName: new FormControl('', Validators.required),
            phoneNumber: new FormControl('', [Validators.required, Validators.pattern("^[0-9]+(\.?[0-9]+)?$")]),
            category: new FormControl('', Validators.required),
            description: new FormControl('', Validators.required),
            address: this.formBuilder.group({
                city: new FormControl('', Validators.required),
                zipCode: new FormControl('', Validators.required),
                street: new FormControl('', Validators.required),
                streetNumber: new FormControl('', Validators.required),
                flatNumber: new FormControl('')
            }),
            coverPhotoId: new FormControl(''),
            employees: new FormArray([]),
            openingHours: new FormArray([]),
            services: new FormArray([])
        });

        //Adding initial form fields
        this.addEmployeeForm();
        this.addOpeningHoursForm();
        //this.addService();

        this.form.markAllAsTouched();
    }

    canGoToNextStep(): boolean {
      if(this.step == 1 && 
          !this.hasError('companyName') && 
          !this.hasError('phoneNumber') &&
          !this.hasError('category') &&
          !this.hasError('description') &&
          !this.hasError('city') &&
          !this.hasError('zipCode') &&
          !this.hasError('street') &&
          !this.hasError('streetNumber') &&
          !this.hasError('flatNumber') &&
          !this.hasError('coverPhoto'))
        return true;

      if(this.step == 2 && 
          !this.hasError('employees'))
        return true;

      if(this.step == 3 &&
          !this.hasError('openingHours'))
        return true;

      if(this.step == 4 &&
          !this.hasError('services'))
        return true;

      return false;
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

        for (let index = 1; index <= 6; index++) {
            this.openingHours.push(this.formBuilder.group({
                dayOfWeek: new FormControl(index),
                isAvailable: new FormControl(false),
              start: new FormControl('08:00', [this.conditionalValidator(() => this.openingHours.controls[index].get('isAvailable').value, Validators.required)]),
              end: new FormControl('16:00', [this.conditionalValidator(() => this.openingHours.controls[index].get('isAvailable').value, Validators.required)]),
            }));
        }

        this.openingHours.push(this.formBuilder.group({
          dayOfWeek: new FormControl(0),
          isAvailable: new FormControl(false),
          start: new FormControl('08:00', [this.conditionalValidator(() => this.openingHours.controls[0].get('isAvailable').value, Validators.required)]),
          end: new FormControl('16:00', [this.conditionalValidator(() => this.openingHours.controls[0].get('isAvailable').value, Validators.required)]),
        }));
    }

    addService() {
      let service = this.formBuilder.group({
        serviceName: new FormControl('', Validators.required),
        price: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
        duration: new FormControl('', Validators.required),
        performers: new FormArray([])
      });

      let performers = service.get('performers') as FormArray;

      this.addEmployeesToServiceForm(performers);

      this.services.markAllAsTouched();

      this.services.push(service);
    }

    addEmployeesToServiceForm(performers: FormArray) {

        (this.employees as FormArray).controls.forEach(emp => {
          console.log('employee: ', emp);

          performers.push(this.formBuilder.group({
            isActive: new FormControl(false),
            firstName: new FormControl(emp.get('firstName').value),
            lastName: new FormControl(emp.get('lastName').value)
          }));
        });
    }

    removeService(index: number) {
        this.services.removeAt(index);
    }

    changedCategory(e) {
        this.getControl('category').setValue(e.target.value);
    }

    openHoursChanged(e, index: number) {
        this.openingHours.controls[index].get('start').setValue(e.target.value);
    }

    closeHoursChanged(e, index: number) {
        this.openingHours.controls[index].get('end').setValue(e.target.value);
    }

    serviceTimeChanged (e, index: number) {
      this.services.controls[index].get('duration').setValue(+e.target.value);
    }

    onCoverPhotoChange(e) {
      this.coverPhoto = <File>e.target.files[0];

      const formData = new FormData();
      formData.append('formFile', this.coverPhoto);

      this.profileService.uploadImage(formData).subscribe(image => {
        //this.form.controls.coverPhotoId.setValue(imageId);
        this.form.controls['coverPhotoId'].setValue(image.imageId);
      });

      console.log('coverPhoto', this.coverPhoto);
    }

    onSubmit() {
      console.log(this.form.getRawValue());

      const formValue = Object.assign(
        this.form.value
      )

      console.log('formValue', formValue);

      this.profileService.addBusinessProfile(formValue).subscribe(profile => {
        console.log('Posted profile: ', profile);

        this.router.navigateByUrl('business/appointments');
      });
    }

    nextStep() {
      if(this.canGoToNextStep())
      {
        if(this.step == 3)
          this.addService();

        this.step++;
      }
    }
}
