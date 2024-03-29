import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { ICategory } from '../../models';
import { IImage, IProfile } from '../../models/profile.model';
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
    profile: IProfile = null;
    categories: ICategory[] = null;
    get address() { return this.form.controls.address };
    imageSrc: string = null;
    coverPhoto: File;

    constructor(private profileService: ProfileService,
                private categoriesService: CategoriesService,
                private validationService: ValidationService,
                private formBuilder: FormBuilder,
                private cd: ChangeDetectorRef,
                private toastr: ToastrService)
    {
        super();
    }

    ngOnInit() {
        this.initForm();

        this.getData();
    }

    private getData() {
        let profile = this.profileService.getProfile();
        let categories = this.categoriesService.getCategories();

        forkJoin([profile, categories]).subscribe(result => {
            this.profile = result[0];
            this.categories = result[1];

            this.form.patchValue(result[0]);
            this.cd.markForCheck();

            this.profileService.getImage(this.profile.coverPhotoId).subscribe(image => {
                this.imageSrc = "data:" + image.fileType + ";base64," + image.data;

                this.cd.markForCheck();
            });
        });
    }

    private initForm() {
        this.form = this.formBuilder.group({
            companyName: new FormControl('', this.validationService.companyNameValidator()),
            phoneNumber: new FormControl('', this.validationService.phoneNumberValidator()),
            category: new FormControl('', this.validationService.categoryValidator() ),
            description: new FormControl('', this.validationService.descriptionValidator() ),
            coverPhotoId: new FormControl(''),
            address: this.formBuilder.group({
                city: new FormControl('', this.validationService.cityValidator() ),
                zipCode: new FormControl('', this.validationService.zipCodeValidator() ),
                street: new FormControl(''),
                flatNumber: new FormControl(''),
                streetNumber: new FormControl('')
            })
        });
    }

    changedCategory(e) {
        this.getControl('category').setValue(e.target.value);
    }

    onCoverPhotoChange(e) {
        this.coverPhoto = <File>e.target.files[0];
  
        const formData = new FormData();
        formData.append('formFile', this.coverPhoto);
  
        this.profileService.uploadImage(formData).subscribe(image => {
          this.form.controls['coverPhotoId'].setValue(image.imageId);
        });
  
        console.log('coverPhoto', this.coverPhoto);
      }

    onSubmit() {
        console.log(this.form.getRawValue());

        const formValue: IProfile = Object.assign(this.form.value);
        formValue.companyId = this.profile.companyId;

        this.profileService.updateProfile(formValue).subscribe(profile => {
            console.log('Updated profile: ', profile);

            this.toastr.success("Changes saved successfully!");

            this.getData();
        });
    }
}
