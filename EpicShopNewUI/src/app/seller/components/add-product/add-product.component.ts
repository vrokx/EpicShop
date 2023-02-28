import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ProductListService } from '../../services/product-list.service';
import { Products } from 'src/app/models/products';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {

  product: Products = { productId: 0, productName: '', price: 0, image: '' };

  constructor(private productService: ProductListService, private router: Router) {}

  addProduct(): void {
    const newProduct: Products = { ...this.product };
    this.productService.addProduct(newProduct).subscribe(() => {
      this.router.navigate(['']);
    });
  }

  onCancel(): void {
    this.router.navigate(['']);
  }
}
