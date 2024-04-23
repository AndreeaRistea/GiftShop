import { OrderStatus } from '../enums/orderStatus';

export class OrdersHistoryDto {
  Id: string = '';
  OrderDate: string = '';
  OrderStatus!: OrderStatus;
  TotalAmount: number = 0;
}
