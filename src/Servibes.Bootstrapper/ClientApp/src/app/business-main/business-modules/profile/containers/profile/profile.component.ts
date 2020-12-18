import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { ICategory } from '../../models';
import { IProfile } from '../../models/profile.model';
import { ProfileService } from '../../services';
import { CategoriesService } from '../../services/categories.service';
import { ValidationService } from '../../services/validation.service';

@Component({
    selector: 'profile',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent extends BaseForm implements OnInit {
    profile: IProfile;
    categories: ICategory[];

    constructor(private profileService: ProfileService,
                private categoriesService: CategoriesService,
                private validationService: ValidationService,
                private formBuilder: FormBuilder) 
    {
        super();
    }

    ngOnInit() {
        let profile = this.profileService.getProfile();
        let categories = this.categoriesService.getCategories();

        forkJoin([profile, categories]).subscribe(result => {
            this.profile = result[0];
            this.categories = result[1];

            console.log('categories', this.categories);
            console.log('profile', this.profile);

            this.initForm();
        });
    }

    private initForm() {
        console.log('profile', this.profile);

        this.form = this.formBuilder.group({
            companyName: new FormControl(this.profile.companyName, this.validationService.companyNameValidator()),
            phoneNumber: new FormControl(this.profile.phoneNumber, this.validationService.phoneNumberValidator()),
            category: new FormControl(this.profile.category, this.validationService.categoryValidator() ),
            description: new FormControl(this.profile.description, this.validationService.descriptionValidator() ),
            coverPhoto: new FormControl(this.profile.coverPhoto, this.validationService.requiredVaidator() ),
            /*address: this.formBuilder.group({
                city: new FormControl(this.profile.address.city, this.validationService.cityValidator() ),
                zipcode: new FormControl(this.profile.address.zipcode, this.validationService.zipCodeValidator() ),
                street: new FormControl(this.profile.address.street),
                flatNumber: new FormControl(this.profile.address.flatNumber),
                streetNumber: new FormControl(this.profile.address.streetNumber)
            })*/
        });

        console.log('form', this.form.getRawValue());
    }

    onSubmit() {
        console.log(this.form.getRawValue());
  
        const formValue: IProfile = Object.assign(this.form.value);
  
        this.profileService.updateProfile(formValue).subscribe(profile => {
            console.log('Updated profile: ', profile);
        });
    }
}
