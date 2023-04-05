import { Component, OnInit } from '@angular/core';
import { Products } from 'src/app/models/products';
import { ProductListService } from '../../services/product-list.service';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent{
  
  token = localStorage.getItem("AccessToken") || '';
  decodeToken: any = jwt_decode(this.token);
  role = this.decodeToken.role;

  products: Products[] = [];
  editProductId: number = 0;

  constructor(private productService: ProductListService, private router: Router) {
    if (this.role !== 'seller') {
      console.log(this.role);
      // redirect the user to a different page if they don't have the required role
      this.router.navigate(['/']);
      return;
    }

    this.getAllProducts();
  }
  
  getAllProducts(): void {
    this.productService.getAllProducts().subscribe((products: Products[]) => {
      this.products = products;
      console.log(products);
    });
  }

  updateProduct(updatedProduct: Products): void {
    this.productService.updateProduct(updatedProduct.productId as number, updatedProduct).subscribe(() => {
      this.getAllProducts(); // refresh the product list after updating
      this.editProductId = 0; // reset edit product ID
    });
  }

  editProduct(product: Products): void {
    this.router.navigate(['/update-product', product.productId]);
  }

  deleteProduct(product: Products): void {
    this.router.navigate(['/delete-product', product.productId]);
  }

  addProduct(product: Products): void {
    this.router.navigate(['/add-product']);
  }

  cancelEdit(): void {
    this.editProductId = 0;
  }
}
