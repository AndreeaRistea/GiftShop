import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private httpService: HttpService) {}

  checkout(): Observable<any> {
    return this.httpService.post('Cart', {});
  }
}
