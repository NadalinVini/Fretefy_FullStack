import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegiaoComponent } from './regiao.component';
import { RegiaoRoutingModule } from './regiao.routing';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RegiaoRoutingModule
  ],
  declarations: [RegiaoComponent],
  exports: [RegiaoComponent]
})
export class RegiaoModule { }
