# Arquitectura del sistema

**Estado:** describe la implementación presente en `main`; los cambios futuros deben actualizar este documento.

## Vista general

STOXX es una aplicación web ASP.NET Core en .NET 10. La interfaz está implementada con componentes Razor/Blazor y el servidor expone rutas HTTP bajo `/api`. Entity Framework Core con el proveedor SQLite persiste los datos.

```text
Navegador
   │ interfaz Razor/Blazor y solicitudes HTTP
   ▼
ASP.NET Core — src/Stoxx.Web/Program.cs
   ├── Autenticación por cookies y autorización por roles
   ├── Endpoints /api para ventas, caja, inventario, compras y reportes
   └── Entity Framework Core (AppDbContext)
          │
          ▼
       SQLite
```

## Componentes y responsabilidades

| Componente | Responsabilidad |
|---|---|
| `Components/` | Renderizar la aplicación web y la pantalla principal interactiva. |
| `Program.cs` | Configurar servicios, autenticación, autorización, persistencia y endpoints HTTP. |
| `Contracts/Requests.cs` | Definir los datos recibidos por las operaciones de la API. |
| `Data/Entities.cs` | Definir usuarios, productos, proveedores, ventas, compras y sesiones de caja. |
| `Data/AppDbContext.cs` | Exponer tablas y configurar índices, precisión monetaria y relaciones. |
| `tests/Stoxx.Web.Tests/` | Probar endpoints de salud, configuración inicial, autenticación y autorización. |
| `.github/workflows/dotnet-ci.yml` | Restaurar, compilar y ejecutar pruebas en pushes y pull requests hacia `main`. |

## Flujo de una solicitud

1. El navegador muestra la interfaz Razor/Blazor o solicita una ruta `/api`.
2. ASP.NET Core valida la cookie de sesión y aplica las políticas de autorización del endpoint.
3. El endpoint valida los datos, aplica las reglas del dominio y usa `AppDbContext`.
4. EF Core lee o actualiza SQLite y el endpoint devuelve una respuesta HTTP.

Las ventas se realizan dentro de una transacción: se verifica que exista una caja abierta, que los productos estén activos y que el stock alcance; luego se descuentan unidades y se guarda la venta con sus partidas.

## Autenticación y roles

La aplicación usa cookies HTTP-only con expiración deslizante de ocho horas. En producción requiere cookies seguras. Los roles existentes son `Admin`, `Cajero` e `Inventario`; las políticas específicas se aplican en los endpoints. La primera cuenta administradora se crea mediante configuración inicial de un solo uso cuando la base todavía no contiene usuarios.

## Persistencia y despliegue

La conexión se obtiene de `ConnectionStrings:DefaultConnection`; si no está definida, la aplicación usa `STOXX_DATA_DIR` o el directorio `data` bajo el directorio de contenido. SQLite guarda `stoxx.db` y las claves de protección de datos se guardan en `data/keys`. `render.yaml` configura la publicación Docker en Render y el volumen persistente.

Al iniciar, la aplicación ejecuta `EnsureCreated`; el repositorio no contiene migraciones de EF Core. Si el esquema cambia, el equipo debe acordar y documentar el procedimiento de actualización de bases existentes antes del despliegue.

## Decisiones y límites conocidos

- El backend y la interfaz se alojan en el mismo proceso ASP.NET Core.
- SQLite es la base de datos del MVP; no hay servicio de base de datos separado.
- El método de pago QR se registra como tipo de pago; no hay integración de validación bancaria.
- Las pruebas de integración se ejecutan en CI. Las pruebas manuales del despliegue están registradas por separado en `QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md`.
