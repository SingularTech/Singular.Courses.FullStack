import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhonesListComponent } from './phones-list/phones-list.component';
import { PhonesRoutingModule } from './phones-routing.module';
import { PhonesService } from './services/phones.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PhonesFormComponent } from './phones-form/phones-form.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    PhonesListComponent,
    PhonesFormComponent
  ],
  imports: [
    CommonModule,
    PhonesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers: [
    PhonesService
  ]
})
export class PhonesModule { }
