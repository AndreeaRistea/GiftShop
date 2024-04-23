import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderDetailsDto } from 'src/app/models/orderDetailsDto';
import { ProductDto } from 'src/app/models/productDto';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.page.html',
  styleUrls: ['./order-details.page.scss'],
})
export class OrderDetailsPage implements OnInit {
  orderDetail!: OrderDetailsDto;
  products: ProductDto[] = [];
  constructor(
    private orderService: OrderService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    const orderId = this.activatedRoute.snapshot.params['orderId'];
    this.orderService.orderDetails(orderId).subscribe((orderDetail) => {
      this.orderDetail = orderDetail;
      this.products = orderDetail.OrderItems.map((oi) => oi.Product!);
    });
  }
}
