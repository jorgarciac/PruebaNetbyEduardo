# Servicio de Gestión de Productos - Prueba Técnica NETBY

Este microservicio permite administrar productos dentro de un sistema de inventario, y proporciona operaciones de lectura, escritura y ajuste de stock. También sirve como proveedor de información para el microservicio de transacciones.

Forma parte del sistema de microservicios del proyecto `PruebaNetbyEduardo`.

---

## 🛠️ Tecnologías utilizadas

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- Arquitectura en capas: Controlador, Servicio, Repositorio
- Validaciones con DataAnnotations

---

## 📦 Funcionalidades del microservicio

- Crear, editar y eliminar productos
- Listar productos y consultar por ID
- Validar campos requeridos (nombre, precio, stock)
- Exponer stock disponible por producto
- Ajustar automáticamente el stock (compra/venta) desde otros microservicios

---

## 🔧 Requisitos

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 actualizado
- Otros microservicios deben conocer la URL base para consumirlo

---

## 🚀 Ejecución local

### 1. Configurar conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "ConexionSQL": "Server=localhost;Database=Netby;User ID=COLOCA_USUARIO_;Password=COLOCA_PASSWORD;Encrypt=False;TrustServerCertificate=True;"
}
