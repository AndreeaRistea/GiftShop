import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { ProductDto } from '../models/productDto';

@Injectable({
  providedIn: 'root',
})
export class WishlistService {
  constructor(private httpService: HttpService) {}

  getProductInWishList(): Observable<ProductDto[]> {
    return this.httpService.get<ProductDto[]>('Wishlist');
  }

  addToWishlist(productId: string): Observable<ProductDto> {
    return this.httpService.post('Wishlist', { productid: productId });
  }

  removeFromWislist(productId: string): Observable<any> {
    return this.httpService.delete(`Wishlist/${productId}`, productId);
  }
}
