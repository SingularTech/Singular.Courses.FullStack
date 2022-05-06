import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PhonesFormComponent } from './phones-form/phones-form.component';
import { PhonesListComponent } from './phones-list/phones-list.component';

const routes: Routes = [
  {
    path: '',
    component: PhonesListComponent
  },
  {
    path: 'list',
    component: PhonesListComponent
  },
  {
    path: 'new',
    component: PhonesFormComponent
  },
  {
    path: 'edit/:id',
    component: PhonesFormComponent
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class PhonesRoutingModule {

}
