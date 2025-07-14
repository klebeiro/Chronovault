import { Component, forwardRef, inject, Input, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

import { ControlValueAccessorDirective } from '../../directives';
import { FormErrorServiceService } from '../../services';
import { SelectOption } from '../../types';

@Component({
  selector: 'app-select',
  templateUrl: './app-select.component.html',
  styleUrls: ['./app-select.component.css', '../../styles/form-field.css'],
  standalone: false,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AppSelectComponent),
      multi: true,
    },
  ],
})
export class AppSelectComponent<T>
  extends ControlValueAccessorDirective<T>
  implements OnInit
{
  private formErrorService: FormErrorServiceService = inject(
    FormErrorServiceService
  );

  @Input('selectId') selectId: string = '';
  @Input('label') label: string = '';
  @Input('selectPlaceholder') placeholder: string = '';
  @Input('options') options: SelectOption<T>[] = [];

  override ngOnInit(): void {
    super.ngOnInit();
  }

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
