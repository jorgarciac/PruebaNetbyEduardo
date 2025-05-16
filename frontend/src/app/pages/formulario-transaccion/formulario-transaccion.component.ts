import { Component, inject  } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

import { TransaccionService } from '../../services/transaccion.service';
import { ProductoService } from '../../services/producto.service';

@Component({
  selector: 'app-formulario-transaccion',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    DropdownModule,
    InputTextModule,
    InputNumberModule,
    ButtonModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './formulario-transaccion.component.html',
  styleUrl: './formulario-transaccion.component.scss'
})
export class FormularioTransaccionComponent {

  private fb = inject(FormBuilder);
  private router = inject(Router);
  private transaccionService = inject(TransaccionService);
  private productoService = inject(ProductoService);
  private messageService = inject(MessageService);

  form: FormGroup = this.fb.group({
    tipo: ['', Validators.required], // Compra o Venta
    productoId: [null, Validators.required],
    cantidad: [null, [Validators.required, Validators.min(1)]],
    precioUnitario: [null, [Validators.required, Validators.min(0.01)]],
    detalle: ['']
  });

  productos: any[] = [];
  productoSeleccionado: any;

  ngOnInit(): void {
    this.productoService.obtenerProductos().subscribe({
      next: (resp) => {
        this.productos = resp;
      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo cargar productos' });
      }
    });

    this.form.get('productoId')?.valueChanges.subscribe(id => {
      this.productoSeleccionado = this.productos.find(p => p.id === id);
      this.form.get('precioUnitario')?.setValue(this.productoSeleccionado?.precio || null);
    });

    this.form.get('cantidad')?.valueChanges.subscribe(() => {
      this.verificarStock();
    });
  }

  verificarStock() {
    const tipo = this.form.get('tipo')?.value;
    const cantidad = this.form.get('cantidad')?.value;
    const stock = this.productoSeleccionado?.stock;

    if (tipo === 'Venta' && stock !== undefined && cantidad > stock) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Stock insuficiente',
        detail: `Solo hay ${stock} unidades disponibles`
      });
      this.form.get('cantidad')?.setValue(null);
    }
  }

 

  guardar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { tipo, productoId, cantidad, precioUnitario, detalle } = this.form.value;

    const transaccion = {
      fecha: new Date().toISOString(),
      tipo,
      productoId,
      cantidad,
      precioUnitario,
      detalle
    };

    this.transaccionService.crearTransaccion(transaccion).subscribe({
      complete: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Transacción registrada',
          detail: 'La operación fue exitosa',
          life: 3000
        });
        setTimeout(() => this.router.navigate(['/transacciones']), 1000);
      },
      error: (err) => {
        console.error('Error al registrar transacción:', err);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudo registrar la transacción',
          life: 3000
        });
      }
    });
  }


}
