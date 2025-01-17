import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { CartItemDto } from '../models/cartItemDto';

@Injectable({
  providedIn: 'root',
})
export class CartItemService {
  constructor(private httpService: HttpService) {}

  getAllCartItems(): Observable<CartItemDto[]> {
    return this.httpService.get<CartItemDto[]>('CartItem/all');
  }

  addCartItems(productId: string, quantity = 1): Observable<CartItemDto> {
    return this.httpService.post('CartItem', {
      productid: productId,
      quantity: quantity,
    });
  }
}
