import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Transaccion {
  id: string;
  fecha: Date;
  tipo: string;
  productoId: string;
  cantidad: number;
  precioUnitario: number;
  detalle: string;
}

@Injectable({
  providedIn: 'root'
})
export class TransaccionService {

  private baseUrl = 'http://localhost:5027/api/Transacciones';
  httpCliente = inject(HttpClient); // Ajusta el puerto si es necesario

  obtenerTransacciones(): Observable<Transaccion[]> {
    return this.httpCliente.get<any[]>(this.baseUrl);
  }

  crearTransaccion(transaccion: any): Observable<any> {
    return this.httpCliente.post(this.baseUrl, transaccion);
  } 

  
  
}
