import { Component, OnInit } from '@angular/core';
import { DataService, ICategory } from '../services/data.service';
// import { ProductPage } from '../pages/product/product.page';
import { CategoryService } from '../services/category.service';
import { CategoryDto } from '../models/categoryDto';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  //public categories: ICategory[] = [];
  public categories?: CategoryDto[];
  constructor(
    private data: DataService,
    private categoryService: CategoryService,
    private userService: UserService
  ) {}

  ngOnInit() {
    //this.categories = this.data.getCategories();
    //this.
    //console.log(this.userService.getUserDetails());
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }
}
