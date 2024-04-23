import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./index/index.module').then((m) => m.IndexPageModule),
  },
  {
    path: '',
    loadChildren: () =>
      import('./home/home.module').then((m) => m.HomePageModule),
  },
  {
    path: 'product/:categoryId/:name',
    loadChildren: () =>
      import('./pages/products/products.module').then(
        (m) => m.ProductsPageModule
      ),
  },
  // {
  //   path: 'pages',
  //   loadChildren: () => import('./pages/pages.module').then( m => m.PagesPageModule)
  // },
  {
    path: 'login',
    loadChildren: () =>
      import('./pages/login/login.module').then((m) => m.LoginPageModule),
  },
  {
    path: 'signup',
    loadChildren: () =>
      import('./pages/signup/signup.module').then((m) => m.SignupPageModule),
  },
  {
    path: 'products',
    loadChildren: () =>
      import('./pages/products/products.module').then(
        (m) => m.ProductsPageModule
      ),
  },
  {
    path: 'cart',
    loadChildren: () =>
      import('./pages/cart/cart.module').then((m) => m.CartPageModule),
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./pages/profile/profile.module').then((m) => m.ProfilePageModule),
  },
  {
    path: 'wishlist',
    loadChildren: () =>
      import('./pages/wishlist/wishlist.module').then(
        (m) => m.WishlistPageModule
      ),
  },
  {
    path: 'orders-history',
    loadChildren: () =>
      import('./pages/orders-history/orders-history.module').then(
        (m) => m.OrdersHistoryPageModule
      ),
  },
  {
    path: 'order-details/:orderId',
    loadChildren: () =>
      import('./pages/order-details/order-details.module').then(
        (m) => m.OrderDetailsPageModule
      ),
  },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
