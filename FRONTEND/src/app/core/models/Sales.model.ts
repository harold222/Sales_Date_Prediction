export interface NextOrderResponse {
  id: number,
  nombre: string,
  ultimaCompra: string,
  prediccion: string
}

export interface GetOrdersResponse {
  id: number,
  fechaRequerida: string,
  fechaCompra: string,
  nombre: string,
  direccion: string,
  ciudad: string,
}

export interface GetAllEmployeesResponse {
  id: number,
  nombreCompleto: string
}

export interface GetAllShippersResponse {
  id: number,
  nombreEmpresa: string
}

export interface GetAllProductsResponse {
  id: number,
  nombre: string
}

export interface CreateOrderRequest {
  Empid: number,
  Shipperid: number,
  Shipname: string,
  Shipaddress: string,
  Shipcity: string,
  Orderdate: string,
  Requireddate: string,
  Shippeddate: string,
  Freight: number,
  Shipcountry: string,
  ProductId: number,
  UnitPrice: number,
  Quantity: number,
  Discount: number,
  CustomerId: number
}

export interface CreateOrderResponse {
  id: number
}