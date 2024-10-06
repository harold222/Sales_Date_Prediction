import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { SalesService } from '../../core/services/sales/sales.service';
import { CreateOrderRequest, GetAllEmployeesResponse, GetAllProductsResponse, GetAllShippersResponse } from '../../core/models/Sales.model';
import { MatSelectModule } from '@angular/material/select';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDialogContent,
    MatDialogActions,
    MatInputModule, 
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    MatSelectModule,
    MatDatepickerModule
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './order-form.component.html',
  styleUrl: './order-form.component.scss'
})
export default class OrderFormComponent {

  readonly dialogRef = inject(MatDialogRef<OrderFormComponent>);
  readonly data = inject<{ id: string, name: string }>(MAT_DIALOG_DATA);

  private service = inject(SalesService);
  private builder = inject(FormBuilder);

  public form = this.builder.group({
    Empid: [, Validators.required],
    Shipperid: [, Validators.required],
    Shipname: [, [Validators.required, Validators.maxLength(40)]],
    Shipaddress: [, [Validators.required, Validators.maxLength(60)]],
    Shipcity: [, [Validators.required, Validators.maxLength(15)]],
    Shipcountry: [, [Validators.required, Validators.maxLength(15)]],
    Orderdate: [, [Validators.required]],
    Requireddate: [, [Validators.required]],
    Shippeddate: [, [Validators.required]],
    Freight: [, [Validators.required, Validators.min(0)]],
    ProductId: [, Validators.required],
    UnitPrice: [, [Validators.required, Validators.min(0)]],
    Quantity: [, [Validators.required, Validators.min(0)]],
    Discount: [, [Validators.required, Validators.min(0)]],
    CustomerId: [this.data.id]
  });

  public employees = signal<GetAllEmployeesResponse[]>([]);
  public shippers = signal<GetAllShippersResponse[]>([]);
  public products = signal<GetAllProductsResponse[]>([]);

  ngOnInit(): void {
    this.service.loadAllSelects().subscribe(resp => {
      this.employees.set(resp[0]);
      this.shippers.set(resp[1]);
      this.products.set(resp[2]);
    });
  }

  public onSubmit() {
    if (this.form.invalid)
      return;

    const form: any = { ...this.form.value };
    
    const request: CreateOrderRequest = {
      Empid: Number(form!.Empid),
      Shipperid: Number(form!.Shipperid),
      Shipname: form!.Shipname,
      Shipaddress: form!.Shipaddress,
      Shipcity: form!.Shipcity,
      Shipcountry: form!.Shipcountry,
      Orderdate: form!.Orderdate,
      Requireddate: form!.Requireddate,
      Shippeddate: form!.Shippeddate,
      Freight: Number(form!.Freight),
      ProductId: Number(form!.ProductId),
      UnitPrice: Number(form!.UnitPrice),
      Quantity: Number(form!.Quantity),
      Discount: Number(form!.Discount),
      CustomerId: Number(form!.CustomerId)
    }

    this.service.createOrder(request).subscribe(() => this.dialogRef.close(true));
  }
}
