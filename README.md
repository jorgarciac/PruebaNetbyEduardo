
# 🧠 Sistema de Gestión de Inventario - Prueba Técnica NETBY

Aplicación basada en microservicios para gestionar productos y transacciones de inventario. Desarrollada con Angular, .NET Core y SQL Server. Incluye validaciones, control de stock, filtros, componentes PrimeNG y comunicación entre servicios.

---

## 🧩 Estructura General

```
/PruebaNetbyEduardo
├── frontend/                     → Aplicación Angular
├── ServicioProductos.API/       → Microservicio de Productos (.NET Core)
├── ServicioTransacciones.API/   → Microservicio de Transacciones (.NET Core)
├── base_de_datos.sql            → Script de creación de tablas
└── README.md
```

---

## 🚀 Instalación y ejecución local

### 📌 Requisitos previos

- Node.js 18.x o superior
- Angular CLI (`npm install -g @angular/cli`)
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 actualizado

---

## 🌐 Frontend (Angular)

```bash
cd frontend
npm install
ng serve
```

URL: `http://localhost:4200`

### 📚 Dependencias y módulos PrimeNG

- primeng, primeicons
- `TableModule`, `ButtonModule`, `InputTextModule`, `InputNumberModule`
- `DropdownModule`, `ToastModule`, `DialogModule`, `ConfirmDialogModule`
- `ReactiveFormsModule`, `HttpClientModule`, `RouterModule`



---

## 📦 Instalación de dependencias PrimeNG utilizadas

Ejecuta el siguiente comando desde la carpeta `frontend` para instalar solo los módulos y librerías utilizadas en este proyecto:

```bash
npm install primeng primeicons @primeng/themes @angular/animations @angular/forms @angular/common
```

### Módulos PrimeNG utilizados en el frontend

| Componente              | Módulo PrimeNG                          |
|------------------------|------------------------------------------|
| Tabla de productos     | `TableModule`                            |
| Botones                | `ButtonModule`                           |
| Entradas de texto      | `InputTextModule`                        |
| Entradas numéricas     | `InputNumberModule`                      |
| Dropdown (select)      | `DropdownModule`                         |
| Toast (mensajes)       | `ToastModule`                            |
| Diálogo (modal)        | `DialogModule`                           |
| Confirmación eliminar  | `ConfirmDialogModule`                    |
| Filtros de columna     | (incluidos en `TableModule`)             |
| Navegación             | `RouterModule` (Angular core)            |
---

## 🧾 Microservicio: Servicio de Gestión de Productos

Este microservicio permite administrar productos y ajustar el stock.

### ✅ Funcionalidades

- Crear, editar, eliminar productos
- Consultar producto por ID
- Validar datos requeridos (nombre, precio, stock)
- Ajustar stock automáticamente por compras/ventas

### 🛠️ Tecnologías

- ASP.NET Core 8.0
- EF Core + SQL Server
- Swagger / OpenAPI
- Arquitectura en capas (Controller, Service, Repository)

### ⚙️ Ejecución local

```bash
cd ServicioProductos.API
dotnet restore
dotnet run
```

Editar `appsettings.json`:

```json
"ConnectionStrings": {
  "ConexionSQL": "Server=localhost;Database=Netby;User ID=TU_USUARIO;Password=TU_PASSWORD;Encrypt=False;TrustServerCertificate=True;"
}
```

---

## 🧾 Microservicio: Servicio de Gestión de Transacciones

Este microservicio registra compras y ventas de productos, ajustando el stock y validando disponibilidad.

### ✅ Funcionalidades

- Registrar compras/ventas
- Validar stock antes de ventas
- Ajustar stock desde ServicioProductos.API
- Rollback si el ajuste falla
- Consultar historial por producto

### 🛠️ Tecnologías

- ASP.NET Core 8.0 + EF Core + SQL Server
- Comunicación entre microservicios con HttpClient
- Swagger, arquitectura en capas

### ⚙️ Ejecución local

```bash
cd ServicioTransacciones.API
dotnet restore
dotnet run
```

Editar `appsettings.json`:

```json
"ConnectionStrings": {
  "ConexionSQL": "Server=localhost;Database=Netby;User ID=TU_USUARIO;Password=TU_PASSWORD;Encrypt=False;TrustServerCertificate=True;"
}
```

> ⚠️ Requiere que `ServicioProductos.API` esté corriendo

---

## 📄 Base de datos (SQL Server)

Script `base_de_datos.sql` incluye:

- Tabla `Productos`
- Tabla `Transacciones` con columna computada `PrecioTotal`
- FK entre `Transacciones.ProductoId` y `Productos.Id`

---

## ✅ Validaciones de Transacciones

### Frontend (Angular)

- Campos requeridos: `tipo`, `productoId`, `cantidad`, `precioUnitario`
- Validación de stock si `tipo = Venta`
- Longitud máxima: `detalle` (250)
- `precioTotal`: calculado en BD
- Fecha generada con `new Date().toISOString()`

### Backend (ASP.NET Core)

- Validación de modelo con `ModelState`
- Validación de stock (ventas)
- Rollback si el ajuste falla
- Respuestas claras con código HTTP

---

## 🙌 Autor

Desarrollado por **Eduardo García Castro** como parte de la evaluación técnica Fullstack para NETBY.


---

## ⚙️ Configuraciones importantes

### 🔁 Configurar microservicio de productos en ServicioTransacciones.API

En el archivo `Program.cs` del microservicio de transacciones, ubica y modifica la sección donde se registra el `HttpClient` para el microservicio de productos:

```csharp
builder.Services.AddHttpClient("Productos", client =>
{
    client.BaseAddress = new Uri("http://localhost:16937");
});
```

> Asegúrate de que este puerto coincida con el asignado al ejecutar `ServicioProductos.API`.

---

### 🌐 Configurar rutas del frontend a los endpoints correctos

Edita los archivos de servicios en el frontend para que apunten a los puertos correctos según se asignen al ejecutar los microservicios:

#### 📁 \src\app\services\producto.service.ts

```ts
private baseUrl = 'http://localhost:16937/api/productos';
```

#### 📁 \src\app\services\transaccion.service.ts

```ts
private baseUrl = 'http://localhost:5027/api/Transacciones';
```

> Reemplaza los valores con los puertos reales que se generen en tu entorno local (Visual Studio, launchSettings, etc).

