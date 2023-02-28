import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './seller/components/add-product/add-product.component';
import { DeleteProductComponent } from './seller/components/delete-product/delete-product.component';
import { ProductListComponent } from './seller/components/product-list/product-list.component';
import { UpdateProductComponent } from './seller/components/update-product/update-product.component';

const routes: Routes = [
  { path: '', component: ProductListComponent },
  {path : 'update-product/:id', component: UpdateProductComponent},
  {path : 'delete-product/:id', component: DeleteProductComponent},
  {path : 'add-product', component: AddProductComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
