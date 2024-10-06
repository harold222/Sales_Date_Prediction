import { Component, inject, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { SalesService } from '../../core/services/sales/sales.service';
import { NextOrderResponse } from '../../core/models/Sales.model';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, delay, distinctUntilChanged, filter, Subject, takeUntil } from 'rxjs';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import OrdersViewComponent from '../orders-view/orders-view.component';
import OrderFormComponent from '../order-form/order-form.component';

@Component({
  selector: 'app-sales-date',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule, 
    MatSortModule, 
    MatPaginatorModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatIconModule,
    ReactiveFormsModule
  ],
  templateUrl: './sales-date.component.html',
  styleUrl: './sales-date.component.scss',
  encapsulation: ViewEncapsulation.None
})
export default class SalesDateComponent {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns: string[] = ['nombre', 'ultimaCompra', 'prediccion'];
  private service = inject(SalesService);
  readonly dialog = inject(MatDialog);

  public value = new FormControl('');
  private destroy$ = new Subject<void>();
  public dataSource = new MatTableDataSource<any>([]);

  ngOnInit(): void {
    this.getData("");

    this.value.valueChanges
    .pipe(
      distinctUntilChanged(),
      debounceTime(500),
      takeUntil(this.destroy$)
    )
    .subscribe((value) => {
      this.getData(value ?? "");
    });
  }

  getData(filter = "") {
    this.service.nextOrders(filter).subscribe((data: NextOrderResponse[]) => this.dataSource.data = data);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  openDialog(id: number, name: string, isCreate = false): void {
    if (!isCreate) {
      this.dialog.open(OrdersViewComponent, {
        data: { id, name },
        maxHeight: '90vh',
        maxWidth: '90vw',
        disableClose: true,
        hasBackdrop: true,
      });
    } else {
      const dialogRef = this.dialog.open(OrderFormComponent, {
        data: { id, name },
        maxHeight: '90vh',
        maxWidth: '90vw',
        disableClose: true,
        hasBackdrop: true,
      });

      dialogRef.afterClosed().pipe(filter(x => x)).subscribe(result => this.value.setValue(""));
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
