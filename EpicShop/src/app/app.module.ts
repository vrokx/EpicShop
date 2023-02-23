import { CommonModule, CurrencyPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {AllProductsComponent} from './components/all-products/all-products.component';
import { AllProductsService } from './services/all-products.service';
import { AddProductsComponent } from './components/add-products/add-products.component';
import { FormsModule } from '@angular/forms';
import { UpdateProductComponent } from './components/update-product/update-product.component';

@NgModule({
  declarations: [
    AppComponent,
    AllProductsComponent,
    AddProductsComponent,
    UpdateProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    FormsModule
  ],
  providers: [
    AllProductsService,
    CurrencyPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
