import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/orders';
import { BuyerProductListService } from '../../services/buyer-product-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  order!: Order;
  emailSent = false;
  errorMessage = '';

  constructor(private orderService: BuyerProductListService,private router : Router) {}

  ngOnInit() {
    this.getOrderDetails();
  }

  getOrderDetails() {
    this.orderService.showOrder().subscribe((result: Order) => {
      this.order = result;
      console.log('Order details:', this.order);
    }, (error: any) => {
      console.error('An error occurred while getting order details:', error);
    });
  }
  
  sendEmail(userId: number) {
    this.orderService.sendEmail(userId)
      .subscribe(
        (response) => console.log(response),
        (error) => console.log(error)
      );
      this.router.navigate(['/']);
  }  
}

