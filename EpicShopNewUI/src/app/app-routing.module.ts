import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './seller/components/add-product/add-product.component';
import { DeleteProductComponent } from './seller/components/delete-product/delete-product.component';
import { ProductListComponent } from './seller/components/product-list/product-list.component';
import { UpdateProductComponent } from './seller/components/update-product/update-product.component';
import { BuyerProductListedComponent } from './buyer/components/buyer-product-list/buyer-product-list.component';
import { ViewCartComponent } from './buyer/components/view-cart/view-cart.component';
import { WalletComponent } from './buyer/components/wallet/wallet.component';
import { PaymentModeComponent } from './buyer/components/payment-mode/payment-mode.component';
import { OrderDetailsComponent } from './buyer/components/order-details/order-details.component';
import { RegistrationComponent } from './auth/components/registration/registration.component';
import { LoginComponent } from './auth/components/login/login.component';
import { AuthGuard } from './Auth/services/auth.guard';
import { NavbarUserComponent } from './buyer/components/navbar-user/navbar-user.component';

const routes: Routes = [
  {path : '', component: BuyerProductListedComponent},
  {path : 'view-cart', component: ViewCartComponent,canActivate:[AuthGuard]},
  {path : 'wallet', component: WalletComponent,canActivate:[AuthGuard]},
  {path : 'paymentMode', component: PaymentModeComponent,canActivate:[AuthGuard]},
  {path : 'orderDetails', component: OrderDetailsComponent,canActivate:[AuthGuard]},
  {path: 'seller/product-list', component: ProductListComponent,canActivate:[AuthGuard] },
  {path : 'update-product/:id', component: UpdateProductComponent,canActivate:[AuthGuard]},
  {path : 'delete-product/:id', component: DeleteProductComponent,canActivate:[AuthGuard]},
  {path : 'add-product', component: AddProductComponent,canActivate:[AuthGuard]},
  {path : 'registration', component: RegistrationComponent},
  {path : 'login', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
