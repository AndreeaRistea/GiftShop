import { OrderStatus } from '../enums/orderStatus';
import { CartItemDto } from './cartItemDto';

export class OrderDetailsDto {
  OrderDate: string = '';
  OrderItems!: CartItemDto[];
  OrderStatus!: OrderStatus;
  TotalAmount: number = 0;
}
