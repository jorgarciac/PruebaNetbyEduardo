import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { TransaccionService } from '../../services/transaccion.service';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-transacciones',
  standalone: true,
  imports: [CommonModule, TableModule,ButtonModule,RouterModule],
  templateUrl: './transacciones.component.html',
  styleUrl: './transacciones.component.scss'
})
export class TransaccionesComponent {

  apiService = inject(TransaccionService);

  transacciones: any[]= [];
  loading = true;
  
  ngOnInit(){
    this.apiService.obtenerTransacciones().subscribe({
      next: (data) => {
        console.log(data);
        this.transacciones = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al obtener transacciones', err);
        this.loading = false;
      }
    });
  }

}
