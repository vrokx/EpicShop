import { Component} from '@angular/core';
import { Product } from 'src/app/models/product';
import { AllProductsService } from 'src/app/services/all-products.service';

@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.css']
})
export class AllProductsComponent{
  products: Product[] = [];

  constructor(private allProductsService: AllProductsService) {
    this.allProductsService.getProducts()
      .subscribe({
        next: (products) => {
          this.products = products;
          console.log(products);
        },
        error: (response) => {
          console.log(response);
        }
      });
  }
}