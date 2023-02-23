import { Component } from '@angular/core';
import { Product } from 'src/app/models/product';
import { AddProductsService } from 'src/app/services/add-products.service';

@Component({
  selector: 'app-add-products',
  templateUrl: './add-products.component.html',
  styleUrls: ['./add-products.component.css']
})
export class AddProductsComponent {
  product: Product = {
    productName: '',
    image: '',
    price: 0
  };

  constructor(private productService: AddProductsService) {}

  onSubmit(): void {
    this.productService.createProduct(this.product)
      .subscribe(
        product => console.log('Product created: ', product),
        error => console.error('Error creating product: ', error)
      );
}
}
