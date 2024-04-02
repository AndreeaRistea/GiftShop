import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from 'src/app/models/productDto';
import { CartItemService } from 'src/app/services/cartItem.service';
import { ProductService } from 'src/app/services/product.service';

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
    private cartItemService: CartItemService
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
  test(productId: any) {
    console.log(productId);
    this.cartItemService.addCartItems(productId).subscribe();
  }
}
