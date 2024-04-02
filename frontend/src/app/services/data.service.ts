import { Injectable } from '@angular/core';

export interface ICategory {
  id: number;
  name: string;
  image: string;
}

export interface IProduct {
  id: number;
  name: string;
  price: number;
  category: string;
  image: string;
}

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor() {}

  getCategories() {
    let categories = [];

    let cat1: ICategory = {
      id: 1,
      name: 'Flowers',
      image: '../../assets/images/category/flowers.png',
    };
    let cat2: ICategory = {
      id: 2,
      name: 'Home Decors',
      image: '../../assets/images/category/homeDecors.png',
    };
    let cat3: ICategory = {
      id: 3,
      name: 'Jewelry',
      image: '../../assets/images/category/jewelry.png',
    };

    categories.push(cat1, cat2, cat3);

    return categories;
  }

  getProducts(categoryName: string) {
    let products = [];

    let prod1: IProduct = {
      id: 1,
      name: 'Clock',
      price: 55,
      category: 'Home decor',
      image: '../../assets/images/products/clock.png',
    };
    let prod2: IProduct = {
      id: 2,
      name: 'Candle Warmer Lamp',
      price: 34,
      category: 'Home decor',
      image: '../../assets/images/products/Candle.PNG',
    };
    let prod3: IProduct = {
      id: 3,
      name: 'Earrings',
      price: 400,
      category: 'Jewelry',
      image: '../../assets/images/products/earrings.PNG',
    };
    let prod4: IProduct = {
      id: 4,
      name: 'Bracelet',
      price: 900,
      category: 'Jewelry',
      image: '../../assets/images/products/bracelet.PNG',
    };
    products.push(prod1, prod2, prod3, prod4);

    //return products;
    return products.filter((product) => product.category === categoryName);
  }
}
