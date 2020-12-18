import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ValidatorFn, Validators } from '@angular/forms';

@Injectable()
export class ValidationService {
    constructor() {
    }

    public requiredVaidator() : Validators {
        return Validators.required;
    }

    public companyNameValidator() : ValidatorFn[] {
        return [
            Validators.pattern("^[.@&]?[a-zA-Z0-9 ]+[ !.@&()]?[ a-zA-Z0-9!()]+/"), 
            Validators.required
        ];
    }

    public phoneNumberValidator() : ValidatorFn[] {
        return [
            Validators.pattern("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"),
            Validators.required
        ];
    }

    public categoryValidator() : ValidatorFn[] {
        return [
            Validators.required
        ];
    }

    public descriptionValidator() : ValidatorFn[] {
        return [
            Validators.pattern("^(.|\s)*[a-zA-Z]+(.|\s)*$"),
            Validators.required
        ]
    }

    public cityValidator() : ValidatorFn[] {
        return [
            Validators.pattern("^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$"),
            Validators.required
        ];
    }

    public zipCodeValidator() : ValidatorFn[] {
        return [
            Validators.pattern("^[0-9]{2}-[0-9]{3}*$"),
            Validators.required
        ];
    }
}
