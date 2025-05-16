import { Component, inject  } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { ProductoService } from '../../services/producto.service';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-formulario-producto',
  standalone: true,
  providers: [MessageService],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    InputTextModule,
    InputNumberModule,
    ButtonModule,
    ToastModule
  ],
  templateUrl: './formulario-producto.component.html',
  styleUrl: './formulario-producto.component.scss'
})
export class FormularioProductoComponent {

  private fb = inject(FormBuilder);
  private productoService = inject(ProductoService);
  private router = inject(Router);
  private messageService = inject(MessageService);
  private routes = inject(ActivatedRoute);
  productoId: string | null = null;

  form: FormGroup = this.fb.group({
    nombre: ['', Validators.required],
    descripcion: [''],
    categoria: ['', Validators.required],
    imagenUrl: [''],
    precio: [null, [Validators.required, Validators.min(0)]],
    stock: [null, [Validators.required, Validators.min(0)]]
  });

  ngOnInit(): void {
    this.productoId = this.routes.snapshot.paramMap.get('id');

    if (this.productoId) {
      this.productoService.obtenerProductoId(this.productoId).subscribe({
        next: (producto) => this.form.patchValue(producto),
        error: (err) => console.error('No se pudo cargar el producto', err)
      });
    }
  }

  guardar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const producto = this.form.value;

    const obs = this.productoId
      ? this.productoService.actualizarProducto(this.productoId, producto)
      : this.productoService.crearProducto(producto);

    obs.subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: this.productoId ? 'Producto actualizado' : 'Producto creado',
          detail: 'Operación realizada con éxito.',
          life: 3000
        });
        setTimeout(() => this.router.navigate(['/productos']), 3000);
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: this.productoId ? 'No se pudo actualizar el producto' : 'No se pudo crear el producto',
          life: 3000
        });
      }
    });
  }


}
