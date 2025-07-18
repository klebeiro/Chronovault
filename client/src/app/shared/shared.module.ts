import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';

import { AppInputComponent, AppSelectComponent, PageLoaderComponent } from './components';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ClickStopPropagationDirective } from './directives';

@NgModule({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMaskDirective,
    MatProgressSpinnerModule,
    ClickStopPropagationDirective
  ],
  declarations: [
    AppInputComponent,
    AppSelectComponent,
    PageLoaderComponent,
  ],
  exports: [AppInputComponent, AppSelectComponent, PageLoaderComponent, ClickStopPropagationDirective],
  providers: [
    provideNgxMask()
  ]
})
export class SharedModule {}
