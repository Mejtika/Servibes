import { FormGroup, Validators } from '@angular/forms';

export class BaseForm {
    form: FormGroup;

    getControl(name: string) {
        return this.form.get(name);
    }

    hasError(name: string) {
        var e = this.getControl(name);
        return e && (e.dirty || e.touched) && e.invalid;
    }

    conditionalValidator(predicate, validator: Validators) {
      return (formControl => {

        if (!formControl.parent) { return null; }
        if (predicate()) { return Validators.required(formControl); }
        
        return null;
      })
    }
}