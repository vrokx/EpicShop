import { Component } from '@angular/core';
import { Product } from 'src/app/models/product';
import { AllProductsService } from 'src/app/services/all-products.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.css']
})
export class AllProductsComponent {
  products: Product[] = [];
  editProductId: number = 0;

  constructor(private allProductsService: AllProductsService) {}

  getAllProducts(): void {
    this.allProductsService.getAllProducts().subscribe((products: Product[]) => {
      this.products = products;
    });
  }

  updateProduct(updatedProduct: Product): void {
    this.allProductsService.updateProduct(updatedProduct.productId as number, updatedProduct).subscribe(() => {
      this.getAllProducts(); // refresh the product list after updating
      this.editProductId = 0; // reset edit product ID
    });
  }

  editProduct(productId: number): void {
    this.editProductId = productId;
  }  

  cancelEdit(): void {
    this.editProductId = 0;
  }
}
