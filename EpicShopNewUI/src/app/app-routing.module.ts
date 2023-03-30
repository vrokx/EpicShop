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

const routes: Routes = [
  {path : '', component: BuyerProductListedComponent},
  {path : 'view-cart', component: ViewCartComponent},
  {path : 'wallet', component: WalletComponent},
  {path : 'paymentMode', component: PaymentModeComponent},
  {path : 'orderDetails', component: OrderDetailsComponent},
  {path: 'seller/product-list', component: ProductListComponent },
  {path : 'update-product/:id', component: UpdateProductComponent},
  {path : 'delete-product/:id', component: DeleteProductComponent},
  {path : 'add-product', component: AddProductComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
