import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import {
  AppInputComponent,
  AppLoadingIndicatorComponent,
  AppSelectComponent,
} from './components';
import { ClickStopPropagationDirective } from './directives';
import { CartService } from './services';

@NgModule({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMaskDirective,
    MatProgressSpinnerModule,
    ClickStopPropagationDirective,
    CommonModule
  ],
  declarations: [
    AppInputComponent,
    AppSelectComponent,
    AppLoadingIndicatorComponent,
  ],
  exports: [
    AppInputComponent,
    AppSelectComponent,
    ClickStopPropagationDirective,
    AppLoadingIndicatorComponent
  ],
  providers: [provideNgxMask()],
})
export class SharedModule {}
