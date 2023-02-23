import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductsComponent } from './components/add-products/add-products.component';
import { AllProductsComponent } from './components/all-products/all-products.component';
import { UpdateProductComponent } from './components/update-product/update-product.component';

const routes: Routes = [
  {path: 'DisplayAllProducts', component: AllProductsComponent},
  {path: 'AddProducts', component: AddProductsComponent},
  {path: 'UpdateProducts', component: UpdateProductComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
