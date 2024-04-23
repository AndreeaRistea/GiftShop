import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/enums/orderStatus';
import { OrdersHistoryDto } from 'src/app/models/ordersHistoryDto';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-orders-history',
  templateUrl: './orders-history.page.html',
  styleUrls: ['./orders-history.page.scss'],
})
export class OrdersHistoryPage implements OnInit {
  orders: OrdersHistoryDto[] = [];
  OrderStatus = OrderStatus;
  constructor(private orderService: OrderService, private router: Router) {}

  ngOnInit() {
    this.orderService.ordersHistory().subscribe((orders) => {
      this.orders = orders;
    });
  }
  orderDetails(orderId: string) {
    this.router.navigate([`order-details/${orderId}`]);
    // this.orderService.orderDetails(orderId).subscribe();
  }
}
