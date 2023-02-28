import { CommonModule, CurrencyPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {AllProductsComponent} from './components/all-products/all-products.component';
import { AllProductsService } from './services/all-products.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UpdateProductComponent } from './components/update-product/update-product.component';

@NgModule({
  declarations: [
    AppComponent,
    AllProductsComponent,
    UpdateProductComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([ // <-- add Router Module to imports array
      { path: '', component: AllProductsComponent },
    ])
  ],
  providers: [
    AllProductsService,
    CurrencyPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
