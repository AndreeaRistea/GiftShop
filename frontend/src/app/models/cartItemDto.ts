import { ProductDto } from './productDto';

export class CartItemDto {
  Id: string = '';
  Quantity: number = 0;
  ProductId: string = '';
  Product?: ProductDto;
}
