import { Directive, Inject, Injector, OnInit } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  FormControlDirective,
  FormControlName,
  FormGroupDirective,
  NgControl,
  Validators,
} from '@angular/forms';
import { Subject, takeUntil, startWith, distinctUntilChanged, tap } from 'rxjs';

@Directive({
  selector: '[appControlValueAccessor]',
})
export class ControlValueAccessorDirective<T>
  implements ControlValueAccessor, OnInit
{
  control!: FormControl | undefined;
  isRequired = false;

  private isDisabled = false;
  private destroy$ = new Subject<void>();
  private onTouched!: () => T;

  constructor(@Inject(Injector) private injector: Injector) {}

  ngOnInit(): void {
    this.setFormControl();

    this.isRequired = this.control?.hasValidator(Validators.required) ?? false;
  }

  setFormControl() {
    try {
      const formControl = this.injector.get(NgControl);

      switch (formControl.constructor) {
        case FormControlName: {
          this.control = this.injector
            .get(FormGroupDirective)
            .getControl(formControl as FormControlName);
          break;
        }

        default: {
          const formControlDirective = formControl as FormControlDirective;
          this.control = formControlDirective.form as FormControl;
          break;
        }
      }
    } catch (error) {
      this.control = new FormControl();
    }
  }

  writeValue(value: T): void {
    if (this.control) {
      this.control.setValue(value);
    } else {
      this.control = new FormControl(value);
    }
  }

  registerOnChange(fn: (value: T | null) => T): void {
    this.control?.valueChanges
      .pipe(
        takeUntil(this.destroy$),
        startWith(this.control.value),
        distinctUntilChanged(),
        tap((value) => fn(value))
      )
      .subscribe(() => this.control?.markAsUntouched());
  }

  registerOnTouched(fn: () => T): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
