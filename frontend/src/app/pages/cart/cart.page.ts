import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartItemDto } from 'src/app/models/cartItemDto';
import { CartService } from 'src/app/services/cart.service';
import { CartItemService } from 'src/app/services/cartItem.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.page.html',
  styleUrls: ['./cart.page.scss'],
})
export class CartPage implements OnInit {
  cartItems: CartItemDto[] = [];
  totalPrice: number = 0;
  constructor(
    private cartItemService: CartItemService,
    private cartService: CartService
  ) {}

  ngOnInit() {
    this.cartItemService.getAllCartItems().subscribe((cartItems) => {
      this.cartItems = cartItems;
      this.calculateTotalPrice();
    });
  }

  calculateTotalPrice() {
    this.totalPrice = this.cartItems.reduce((total, item) => {
      return total + (item.Product?.Price || 0) * item.Quantity;
    }, 0);
  }

  updateQuantity(cartItemId: string, newQuantity: number) {
    this.cartItemService.updateQuantity(cartItemId, newQuantity).subscribe(() =>
      this.cartItemService.getAllCartItems().subscribe((cartItems) => {
        this.cartItems = cartItems;
        this.calculateTotalPrice();
      })
    );
  }

  removeFromCart(cartItemId: string) {
    this.cartItemService.removeFromCart(cartItemId).subscribe(() =>
      this.cartItemService.getAllCartItems().subscribe((cartItems) => {
        this.cartItems = cartItems;
        this.calculateTotalPrice();
      })
    );
  }

  checkout() {
    this.cartService.checkout().subscribe(() =>
      this.cartItemService.getAllCartItems().subscribe((cartItems) => {
        this.cartItems = cartItems;
        this.calculateTotalPrice();
      })
    );
  }
}
