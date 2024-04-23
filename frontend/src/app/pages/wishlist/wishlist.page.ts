import { Component, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/models/productDto';
import { WishlistService } from 'src/app/services/wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.page.html',
  styleUrls: ['./wishlist.page.scss'],
})
export class WishlistPage implements OnInit {
  products: ProductDto[] = [];
  constructor(private wishlistService: WishlistService) {}

  ngOnInit() {
    this.wishlistService.getProductInWishList().subscribe((products) => {
      this.products = products;
    });
  }

  // removeFromCart(cartItemId: string) {
  //   this.cartItemService.removeFromCart(cartItemId).subscribe(() =>
  //     this.cartItemService.getAllCartItems().subscribe((cartItems) => {
  //       this.cartItems = cartItems;
  //       this.calculateTotalPrice();
  //     })
  //   );
  // }

  removeFromWishlist(productId: string) {
    this.wishlistService.removeFromWislist(productId).subscribe(() =>
      this.wishlistService.getProductInWishList().subscribe((products) => {
        this.products = products;
      })
    );
  }
}
