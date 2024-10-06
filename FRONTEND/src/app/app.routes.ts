import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    title: 'Sales Date Prediction View',
    loadComponent: () => import('./components/sales-date/sales-date.component')
  },
  {
    path: 'orders/:id',
    title: 'Orders View',
    loadComponent: () => import('./components/orders-view/orders-view.component')
  },
  {
    path: 'new-order',
    title: 'New Order Form',
    loadComponent: () => import('./components/order-form/order-form.component')
  }
];
