import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseForm } from 'src/app/shared/others/BaseForm';
import { TimeArray } from 'src/app/shared/others/time-array';
import { IEmployee, IProfile, IService } from '../../models';
import { EmployeeService, ProfileService, ServicesService } from '../../services';

@Component({
    selector: 'service-form',
    templateUrl: './service-form.component.html',
    styleUrls: ['./service-form.component.scss']
})
export class ServiceFormComponent extends BaseForm implements OnInit {
    private profile: IProfile;
    public service: IService;
    public employees: IEmployee[];
    public performers: IEmployee[];
    public servicetimes: string[] = this.timeArray.generateMinutes(15, 0, 240);

    get employeesForm() { return this.form.controls.employees as FormArray; }

    constructor(
        private profileService: ProfileService,
        private servicesService: ServicesService,
        private employeesService: EmployeeService,
        private activatedRoute: ActivatedRoute,
        private toastr: ToastrService,
        private router: Router,
        private formBuilder: FormBuilder,
        private cd: ChangeDetectorRef,
        private timeArray: TimeArray
    ) {
        super();
    }

    ngOnInit() {
        this.initForm();

        this.profileService.getProfile().subscribe(profile => {
            this.profile = profile;

            let serviceId = this.activatedRoute.snapshot.paramMap.get('serviceId');
            console.log('serviceId', serviceId);
            if(serviceId != "") {
                this.servicesService.getSingleService(this.profile.companyId, serviceId).subscribe(service => {
                    this.service = service;

                    console.log('service', service);

                    this.form.patchValue(service);

                    this.employeesService.getAllEmployees(this.profile.companyId).subscribe(employees => {
                        this.employees = employees;
    
                        this.servicesService.getServiceEmployees(this.profile.companyId, this.service.serviceId).subscribe(performers => {
                            this.performers = performers;
    
                            console.log('performers', performers);
    
                            employees.forEach(e => {
                                let isActive = performers.some(p => p.employeeId === e.employeeId);
    
                                this.addEmployeeToServiceForm(e, isActive);
                            });
        
                            this.cd.markForCheck();
                        });                    
                    });
                });

                
            }
            else {
                this.service = {
                    serviceId: "",
                    serviceName: "",
                    price: 0,
                    duration: 0,
                    description: "",
                };

                this.employeesService.getAllEmployees(this.profile.companyId).subscribe(employees => {
                    this.employees = employees;

                    employees.forEach(e => {
                        this.addEmployeeToServiceForm(e, false);
                    });
                });
            }

            this.cd.markForCheck();
        });
    }

    initForm() {
        this.form = this.formBuilder.group({
            serviceId: new FormControl(''),
            serviceName: new FormControl('', Validators.required),
            price: new FormControl('', Validators.required),
            duration: new FormControl('', Validators.required),
            description: new FormControl('', Validators.required),
            performers: new FormArray([])
        });
    }

    private addEmployeeToServiceForm(employee: IEmployee, isActive: boolean) {
          (this.form.get('performers') as FormArray).push(this.formBuilder.group({
            isActive: new FormControl(isActive),
            firstName: new FormControl(employee.firstName),
            lastName: new FormControl(employee.lastName)
          }));
    }

    serviceTimeChanged (e, index: number) {
        this.form.controls.duration.setValue(+e.target.value);
      }

    onSubmit() {
        var formValues = this.form.getRawValue();

        if(this.service.serviceId != "") {
            this.servicesService.updateService(this.profile.companyId, formValues).subscribe(() => {
                this.toastr.success("Service successfully updated.");

                this.navigateBack();
            })
        }
        else {
            this.servicesService.addService(this.profile.companyId, formValues).subscribe(() => {
                this.toastr.success("Service successfully added.");

                this.navigateBack();
            })
        }
    }

    navigateBack() {
        this.router.navigateByUrl('business/profile/services');
    }
}