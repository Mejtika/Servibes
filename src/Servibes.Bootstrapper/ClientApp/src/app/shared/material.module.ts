import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    MatProgressSpinnerModule
  ],
  providers: [],
  bootstrap: [],
  exports: [
    MatProgressSpinnerModule
  ]
})
export class MaterialModule {
    
}