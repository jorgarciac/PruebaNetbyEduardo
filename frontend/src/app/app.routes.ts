import { Routes } from '@angular/router';
import { ProductosComponent } from './pages/productos/productos.component';
import { FormularioProductoComponent } from './pages/formulario-producto/formulario-producto.component';
import { TransaccionesComponent } from './pages/transacciones/transacciones.component';
import { FormularioTransaccionComponent } from './pages/formulario-transaccion/formulario-transaccion.component';


export const routes: Routes = [
    { path: 'productos',    component: ProductosComponent },
    { path: 'productos/crear', component: FormularioProductoComponent },
    { path: 'productos/editar/:id', component: FormularioProductoComponent },
    { path: 'transacciones', component: TransaccionesComponent },
    { path: 'transacciones/crear', component: FormularioTransaccionComponent },
    { path: '',        redirectTo: 'productos',        pathMatch: 'full'    }
];
