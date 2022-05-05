import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhonesListComponent } from './phones-list/phones-list.component';
import { PhonesRoutingModule } from './phones-routing.module';

@NgModule({
  declarations: [
    PhonesListComponent
  ],
  imports: [
    CommonModule,
    PhonesRoutingModule
  ]
})
export class PhonesModule { }
