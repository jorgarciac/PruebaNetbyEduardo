import { Component, inject  } from '@angular/core';
import { ProductoService } from '../../services/producto.service';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
//import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { RouterModule } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-productos',
  standalone: true,
  providers: [
    MessageService, 
    ConfirmationService
  ],
  imports: [
    CommonModule,
    FormsModule, 
    TableModule, 
    ButtonModule,
    InputTextModule,
    InputNumberModule,
    RouterModule, 
    ToastModule,  
    ConfirmDialogModule ,
    DialogModule
  ],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.scss'
})
export class ProductosComponent {
  apiService = inject(ProductoService);
  confirmationService = inject(ConfirmationService);
  messageService = inject(MessageService);

  productos: any[]= [];
  loading = false;
  mostrarModal: boolean = false;
  productoSeleccionado: any = null;

  ngOnInit(){
    this.apiService.obtenerProductos().subscribe({
      next: (resp)=>{
        console.log(resp);
        this.productos = [...resp];
      }, error: (err)=>{
        console.log(err);
        
      }
    });
  }

  confirmarEliminar(id: string) {
  this.confirmationService.confirm({
      message: '¿Estás seguro de eliminar este producto?',
      header: 'Confirmar eliminación',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Sí',
      rejectLabel: 'No',
      accept: () => {
        this.apiService.eliminarProducto(id).subscribe({
          next: () => {
            this.productos = this.productos.filter(p => p.id !== id);
            this.messageService.add({
              severity: 'success',
              summary: 'Eliminado',
              detail: 'Producto eliminado correctamente',
              life: 3000
            });
          },
          error: () => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'No se pudo eliminar el producto',
              life: 3000
            });
          }
        });
      }
    });
  }

  verDetalle(producto: any) {
  this.productoSeleccionado = producto;
  this.mostrarModal = true;
}

}
