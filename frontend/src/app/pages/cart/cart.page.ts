import { Component, OnInit } from '@angular/core';
import { CartItemDto } from 'src/app/models/cartItemDto';
import { CartItemService } from 'src/app/services/cartItem.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.page.html',
  styleUrls: ['./cart.page.scss'],
})
export class CartPage implements OnInit {
  cartItems: CartItemDto[] = [];
  constructor(private carItemService: CartItemService) {}

  ngOnInit() {}
}
