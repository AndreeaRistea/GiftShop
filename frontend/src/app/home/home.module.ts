import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { HomePageRoutingModule } from './home-routing.module';

import { HomePage } from './home.page';
import { HomeRouter } from './home.router';
import { DataService } from '../services/data.service';
import { CategoryService } from '../services/category.service';
import { HttpService } from '../services/http.service';
import { UserService } from '../services/user.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    HomeRouter,
    HomePageRoutingModule,
    ReactiveFormsModule,
  ],
  exports: [ReactiveFormsModule],
  declarations: [HomePage],
  providers: [DataService, CategoryService, HttpService, UserService],
})
export class HomePageModule {}
