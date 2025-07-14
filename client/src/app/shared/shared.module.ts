import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { AppInputComponent, AppSelectComponent } from './components';

@NgModule({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  declarations: [
    AppInputComponent,
    AppSelectComponent,
  ],
  exports: [AppInputComponent, AppSelectComponent],
})
export class SharedModule {}
