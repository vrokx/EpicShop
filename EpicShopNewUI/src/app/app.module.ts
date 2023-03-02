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
import { BuyerProductListedComponent } from './buyer/components/buyer-product-list/buyer-product-list.component';
import { ViewCartComponent } from './buyer/components/view-cart/view-cart.component';
import { WalletComponent } from './buyer/components/wallet/wallet.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarUserComponent } from './buyer/components/navbar-user/navbar-user.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    UpdateProductComponent,
    DeleteProductComponent,
    AddProductComponent,
    BuyerProductListedComponent,
    ViewCartComponent,
    WalletComponent,
    NavbarUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    ProductListService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
