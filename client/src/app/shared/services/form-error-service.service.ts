import { Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormErrorServiceService {
  constructor() { }

  public getErrorMessage(formControLabel: string, control: FormControl): string {
    if (control.hasError('required')) {
      return `O campo ${formControLabel.toLowerCase()} é obrigatório.`;
    }

    return '';
  }
}
