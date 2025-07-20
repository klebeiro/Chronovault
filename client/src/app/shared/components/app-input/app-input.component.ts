import { Component, forwardRef, inject, Input, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';

import { ControlValueAccessorDirective } from '../../directives';
import { FormErrorServiceService } from '../../services';

type InputType = 'text' | 'number' | 'email' | 'password';
@Component({
  selector: 'app-input',
  templateUrl: './app-input.component.html',
  styleUrls: ['./app-input.component.css', '../../styles/form-field.css'],
  standalone: false,
  providers: [
    FormErrorServiceService,
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AppInputComponent),
      multi: true,
    },
  ],
})
export class AppInputComponent<T>
  extends ControlValueAccessorDirective<T>
  implements OnInit
{
  private formErrorService: FormErrorServiceService = inject(
    FormErrorServiceService
  );

  @Input('inputId') inputId: string = '';
  @Input('label') label: string = '';
  @Input('inputType') inputType: InputType = 'text';
  @Input('inputPlaceholder') inputPlaceholder: string = '';
  @Input('mask') mask: string = '';
  @Input('prefix') prefix: string = '';
  @Input('readonly') readonly: boolean = false;

  getHasError(): boolean {
    if (!this.control) {
      return false;
    }

    return this.control.touched && this.control.invalid;
  }

  getErrorMessage(): string | null {
    if (!this.control) {
      return null;
    }

    return this.formErrorService.getErrorMessage(this.label, this.control);
  }
}
