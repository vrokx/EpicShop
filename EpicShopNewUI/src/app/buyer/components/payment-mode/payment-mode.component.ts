import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/orders';
import { BuyerProductListService } from '../../services/buyer-product-list.service';
import { ViewCartComponent } from '../../components/view-cart/view-cart.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-mode',
  templateUrl: './payment-mode.component.html',
  styleUrls: ['./payment-mode.component.css']
})
export class PaymentModeComponent implements OnInit {
  public order: Order = {
    orderId: 0,
    orderDate: new Date(),
    modeOfPayment: '',
    orderStatus: '',
    amountPaid: 0
  };
  public grandTotal: number = 0;

  constructor(
    private orderService: BuyerProductListService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Access the grandTotal property from the ViewCartComponent
    this.grandTotal = this.orderService.grandTotal;
  }

  public placeOrder(mode: string): void {
    this.order.modeOfPayment = mode;
    this.order.orderStatus = "Confirmed";
  
    this.orderService.placeOrder(mode, this.grandTotal, this.order).subscribe(
      (result: Order) => {
        this.order = result;
        console.log('Order placed successfully:', this.order);
        this.router.navigate(['/orderDetails']);
      },
      (error: any) => {
        console.error('An error occurred while placing order:', error);
      }
    );
  }  
}
