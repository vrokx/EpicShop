import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BuyerProductListService } from '../../services/buyer-product-list.service';

@Component({
  selector: 'app-view-cart',
  templateUrl: './view-cart.component.html',
  styleUrls: ['./view-cart.component.css']
})
export class ViewCartComponent {
  cartItems: any = [];
  grandTotal: number = 0;

  constructor(private cartService: BuyerProductListService, private router: Router) { 
    this.getCart();
  }

  getCart(): void {
    this.cartService.getCartItems().subscribe(
      (data: any) => {
        this.cartItems = data;
        this.calculateGrandTotal();
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  calculateGrandTotal() {
    this.grandTotal = this.cartItems.reduce((total : any , item : any) => total + item.totalAmount, 0);
    this.cartService.grandTotal = this.grandTotal;
  }

  removeFromCart(cartId: number): void {
    this.cartService.removeCartItem(cartId).subscribe(
      (data: any) => {
        console.log(data);
        this.getCart();
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  goToBuyerProductList(): void {
    this.router.navigate(['/']);
  }

  checkout(): void {
    this.router.navigate(['wallet']);
  }
}
