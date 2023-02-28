import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductListComponent } from './seller/components/product-list/product-list.component';
import { ProductListService } from './seller/services/product-list.service';
import { UpdateProductComponent } from './seller/components/update-product/update-product.component';
import { FormsModule } from '@angular/forms';
import { DeleteProductComponent } from './seller/components/delete-product/delete-product.component';
import { AddProductComponent } from './seller/components/add-product/add-product.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    UpdateProductComponent,
    DeleteProductComponent,
    AddProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    ProductListService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
