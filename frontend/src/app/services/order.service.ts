import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { OrdersHistoryDto } from '../models/ordersHistoryDto';
import { Observable } from 'rxjs';
import { OrderDetailsDto } from '../models/orderDetailsDto';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private httpService: HttpService) {}

  ordersHistory(): Observable<OrdersHistoryDto[]> {
    return this.httpService.get<OrdersHistoryDto[]>('Order/orders-history');
  }

  orderDetails(orderId: string): Observable<OrderDetailsDto> {
    return this.httpService.get<OrderDetailsDto>(
      `Order/order-details/${orderId}`
    );
  }
}
