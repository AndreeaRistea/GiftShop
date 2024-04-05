import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductDto } from 'src/app/models/productDto';
import { CartItemService } from 'src/app/services/cartItem.service';
import { ProductService } from 'src/app/services/product.service';
import { WishlistService } from 'src/app/services/wishlist.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.page.html',
  styleUrls: ['./products.page.scss'],
})
export class ProductsPage implements OnInit {
  products: ProductDto[] = [];
  categoryName: string = '';
  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private cartItemService: CartItemService,
    private wishlistService: WishlistService,
    private router: Router
  ) {}

  ngOnInit() {
    const categoryId = this.activatedRoute.snapshot.params['categoryId'];
    this.categoryName = this.activatedRoute.snapshot.params['name'];
    this.productService
      .getProductByCategoryId(categoryId)
      .subscribe((products) => {
        this.products = products;
      });
  }
  addToCart(productId: any) {
    console.log(productId);
    this.cartItemService.addCartItems(productId).subscribe();
    this.router.navigate(['cart']);
  }
  addToWishlist(productId: any) {
    console.log(productId);
    this.wishlistService.addToWishlist(productId).subscribe();
    this.router.navigate(['wishlist']);
  }
}
