import { Component } from '@angular/core';
import { Product } from 'src/app/models/product';
import { updateProductService } from 'src/app/services/update-product.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-update',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent {

  productId: number = 0;
  product: Product = new Product();

  constructor(private productService: updateProductService) { }

  updateProduct(): void {
    this.productService.updateProduct(this.productId, this.product)
      .subscribe(() => this.goBack());
  }
  
}
