import { inject ,Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Producto {
  id: string;
  nombre: string;
  categoria: string;
  precio: number;
  stock: number;
  imagenUrl: string;
  descripcion: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private baseUrl = 'http://localhost:16937/api/productos';
  httpCliente = inject(HttpClient); // Ajusta el puerto si es necesario

  obtenerProductos(): Observable<Producto[]> {    
    return this.httpCliente.get<Producto[]>(this.baseUrl);
  }

  crearProducto(producto: any): Observable<any> {
    return this.httpCliente.post(this.baseUrl, producto);
  }

  obtenerProductoId(id: string): Observable<any> {
    return this.httpCliente.get(`${this.baseUrl}/${id}`);
  }

  actualizarProducto(id: string, producto: any): Observable<any> {
    return this.httpCliente.put(`${this.baseUrl}/${id}`, producto);
  }

  eliminarProducto(id: string): Observable<any> {
    return this.httpCliente.delete(`${this.baseUrl}/${id}`);
  }
}
