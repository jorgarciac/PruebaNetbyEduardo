# Servicio de Gestión de Transacciones - Prueba Técnica NETBY

Este microservicio permite registrar transacciones de **compra y venta** asociadas a productos, validando el stock disponible y ajustándolo automáticamente al confirmar la transacción. En caso de fallo en el ajuste de stock, la transacción se elimina para mantener la integridad del sistema.

Forma parte del sistema de microservicios del proyecto `PruebaNetbyEduardo`.

---

## 🛠️ Tecnologías utilizadas

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- HttpClient para comunicación entre microservicios
- Arquitectura en capas (Controlador, Servicio, Repositorio)

---

## 📦 Funcionalidades del microservicio

- Registrar transacciones (compra o venta)
- Validar stock antes de registrar ventas
- Ajustar automáticamente el stock en el microservicio de productos
- Rollback manual si el ajuste de stock falla
- Consultar historial de transacciones por ID

---

## 🔧 Requisitos

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 (actualizado para .NET 8)
- `ServicioProductos.API` corriendo en paralelo

---

## 🚀 Ejecución local

### 1. Configuración de conexión a base de datos

En `appsettings.json`:

```json
"ConnectionStrings": {
  "ConexionSQL": "Server=localhost;Database=Netby;User ID=COLOCA_USUARIO_;Password=COLOCA_PASSWORD;Encrypt=False;TrustServerCertificate=True;"
}
