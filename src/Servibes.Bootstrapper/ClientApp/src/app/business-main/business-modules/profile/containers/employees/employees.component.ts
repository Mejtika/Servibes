import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { IEmployee } from '../../models';
import { EmployeeService } from '../../services';
import { ProfileService } from '../../services/profile.service';

@Component({
    selector: 'employees',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './employees.component.html',
    styleUrls: ['./employees.component.scss'],
})
export class EmployeesComponent extends BaseForm implements OnInit{
    private companyId: string;
    employees: IEmployee[] = null;
    get formEmployees() { return this.form.controls.employees as FormArray; }

    constructor(private profileService: ProfileService,
        private employeeService: EmployeeService,
        private formBuilder: FormBuilder,
        private cd: ChangeDetectorRef,
        private toastr: ToastrService,
        private router: Router)
        {
            super();
        }

    ngOnInit() {
        this.profileService.getProfile().subscribe(profile => {
            this.companyId = profile.companyId;
            this.getEmployeesList();
        });
    }

    getEmployeesList() {
        this.employeeService.getAllEmployees(this.companyId).subscribe(employees => {
            this.employees = employees;
            this.cd.markForCheck();
        });
    }

    deleteEmployee(employeeId: string) {
        this.employeeService.removeEmployee(this.companyId, employeeId).subscribe(_ => {
            this.toastr.success("Employee removed successfully.");
            this.getEmployeesList();
        })
    }

}
