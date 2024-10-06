import { CommonModule } from '@angular/common';
import { Component, effect, inject, input, Input, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SalesService } from '../../core/services/sales/sales.service';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-orders-view',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule, 
    MatSortModule, 
    MatPaginatorModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDialogContent,
    MatDialogActions
  ],
  templateUrl: './orders-view.component.html',
  styleUrl: './orders-view.component.scss'
})
export default class OrdersViewComponent {

  readonly dialogRef = inject(MatDialogRef<OrdersViewComponent>);
  readonly data = inject<{ id: string, name: string }>(MAT_DIALOG_DATA);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns: string[] = ["id", "fechaRequerida", "fechaCompra", "nombre", "direccion", "ciudad"];
  private service = inject(SalesService);

  public dataSource = new MatTableDataSource<any>([]);

  constructor() {
    effect(
      async (onCleanup) => {
        const currentId = this.data.id;

        if (currentId)
          this.service.getOrders(currentId).subscribe(data => this.dataSource.data = data);

        onCleanup(() => { this.dataSource.data = [] });
      }
    );
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
