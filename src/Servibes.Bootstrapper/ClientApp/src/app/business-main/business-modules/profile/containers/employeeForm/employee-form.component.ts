import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { IEmployee, IProfile } from '../../models';
import { EmployeeService, ProfileService } from '../../services';

@Component({
    selector: 'employee-form',
    templateUrl: './employee-form.component.html',
    styleUrls: ['./employee-form.component.scss']
})
export class EmployeeFormComponent extends BaseForm implements OnInit {
    private profile: IProfile = null;
    private employee: IEmployee = null;

    constructor(
        private profileService: ProfileService,
        private employeeService: EmployeeService,
        private formBuilder: FormBuilder,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService) {
        super();
    }

    ngOnInit() {
        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            var employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
            console.log('employeeId: ', employeeId);
            if(employeeId != "")
            {
                this.employeeService.getSingleEmployee(this.profile.companyId, employeeId).subscribe(employee => {
                    this.employee = employee;
                    this.form.patchValue(employee);
                });
            }
            else {
                this.employee = {
                    employeeId: "",
                    firstName: "",
                    lastName: ""
                };
            }
        });

        this.form = this.formBuilder.group({
            employeeId: new FormControl(''),
            firstName: new FormControl('', Validators.required),
            lastName: new FormControl('', Validators.required)
        });
    }

    onSubmit() {
        const formValues = this.form.getRawValue();

        if(formValues.employeeId == "") {
            this.employeeService.addEmployee(this.profile.companyId, formValues).subscribe(() => {
                this.toastr.success("Employee successfully added.");
                this.navigateBack();
            });
            
        }
        else {
            this.employeeService.updateEmployee(this.profile.companyId, formValues).subscribe(() => {
                this.toastr.success("Employee successfully updated!");
                this.navigateBack();
            });
        }
    }

    navigateBack() {
        this.router.navigateByUrl('business/profile/employees');
    }

    gotoWorkingHours() {
        let route = `business/profile/employees/${this.employee.employeeId}/workinghours`;
        return this.router.navigateByUrl(route);
    }
    
}