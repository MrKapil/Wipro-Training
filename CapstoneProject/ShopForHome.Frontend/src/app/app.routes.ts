import { Routes } from '@angular/router';
import { ProductListComponent } from '@features/catalog/product-list/product-list';
import { ProductDetailComponent } from '@features/catalog/product-detail/product-detail';
import { LoginComponent } from '@features/auth/login/login';
import { RegisterComponent } from '@features/auth/register/register';
import { CartComponent } from '@features/cart/cart';
import { WishlistComponent } from '@features/wishlist/wishlist';
import { CheckoutComponent } from '@features/orders/checkout/checkout';
import { DashboardComponent } from '@features/admin/dashboard/dashboard';
import { UsersComponent } from '@features/admin/users/users';
import { authGuard } from '@core/guards/auth-guard';
import { adminGuard } from '@core/guards/admin-guard';
import { OrderHistoryComponent } from '@features/orders/order-history/order-history';
import { CouponsComponent } from '@features/admin/coupons/coupons';
import { ProductsComponent } from '@features/admin/products/products';
import { AdminReportsComponent } from '@features/admin/reports/reports';
import { StockAlertComponent } from '@features/admin/stock/stock-alert/stock-alert';
import { BulkUploadComponent } from '@features/admin/bulkupload/bulkupload';
import { Home } from '@features/home/home/home';

export const APP_ROUTES: Routes = [
  //{ path: '', component: ProductListComponent },
    //{ path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home, canActivate: [authGuard] },
  { path: 'products', component: ProductListComponent },
  { path: 'products/:id', component: ProductDetailComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cart', component: CartComponent, canActivate: [authGuard] },
  { path: 'wishlist', component: WishlistComponent, canActivate: [authGuard] },
  { path: 'checkout', component: CheckoutComponent, canActivate: [authGuard] },
  { path: 'orders/history', component: OrderHistoryComponent, canActivate: [authGuard] },


  // admin routes
 {
    path: 'admin',
    canActivate: [adminGuard],
    children: [
      { path: '', component: DashboardComponent },
      { path: 'users', component: UsersComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'stock', component: StockAlertComponent },
      { path: 'reports', component: AdminReportsComponent },
      { path: 'coupons', component: CouponsComponent },
      { path: 'bulk-upload', component: BulkUploadComponent }
    ]
  },
  
  // 404 - Not Found
  { path: '**', redirectTo: '/login' }
];
