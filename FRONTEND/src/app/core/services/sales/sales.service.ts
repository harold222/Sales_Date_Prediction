import { Injectable } from '@angular/core';
import { GeneralService } from '../general.service';
import { CreateOrderRequest, CreateOrderResponse, GetAllEmployeesResponse, GetAllProductsResponse, GetAllShippersResponse, GetOrdersResponse, NextOrderResponse } from '../../models/Sales.model';
import { zip } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesService extends GeneralService {

  nextOrders(filter = '') {
    return this.http.get<NextOrderResponse[]>(`${this.base}Services/NextOrder?filter=${filter}`);
  }

  getOrders(id: string) {
    return this.http.get<GetOrdersResponse[]>(`${this.base}Services/GetOrderByClient/${id}`);
  }

  getAllEmployees() {
    return this.http.get<GetAllEmployeesResponse[]>(`${this.base}Services/GetEmployees`);
  }

  getAllShippers() {
    return this.http.get<GetAllShippersResponse[]>(`${this.base}Services/GetShippers`);
  }

  getAllProducts() {
    return this.http.get<GetAllProductsResponse[]>(`${this.base}Services/GetProducts`);
  }

  loadAllSelects() {
    return zip(
      this.getAllEmployees(),
      this.getAllShippers(),
      this.getAllProducts()
    )
  }

  createOrder(request: CreateOrderRequest) {
    return this.http.post<CreateOrderResponse>(`${this.base}Services/CreateOrder`, request);
  }
}
