import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ProductDto } from '../models/productDto';
import { Observable } from 'rxjs';
import { CategoryDto } from '../models/categoryDto';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private httpService: HttpService) {}

  getProductByCategoryId(categoryId: string): Observable<ProductDto[]> {
    return this.httpService.get<ProductDto[]>(
      `Product/by-category-id/${categoryId}`
    );
  }
}
