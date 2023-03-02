import { Component, OnInit } from '@angular/core';
import { CartModel } from 'src/app/models/cart-model';
import { Products } from 'src/app/models/products';
import { BuyerProductListService } from '../../services/buyer-product-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'buyer-product-listed',
  templateUrl: './buyer-product-list.component.html',
  styleUrls: ['./buyer-product-list.component.css']
})
export class BuyerProductListedComponent {
  products: Products[] = [];
  productId: number = 0;
  qty: number = 0;
  cartItem: CartModel = new CartModel();

  constructor(private cartService: BuyerProductListService, private router: Router) {
    this.getAllProducts();
   }

  getAllProducts(): void {
    this.cartService.getAllProducts().subscribe((products: Products[]) => {
      this.products = products;
      console.log(products);
    });
  }

  onAddToCart(productId: number, qty: number, cartItem: CartModel): void {
    this.cartService.addToCart(productId, qty, cartItem)
      .subscribe(() => {
        this.router.navigate(['/view-cart']);
      }, error => {
        alert('Error adding product to cart');
        console.error(error);
      });
  }
}
