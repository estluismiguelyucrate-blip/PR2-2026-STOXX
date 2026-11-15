# Modelo de datos

El esquema refleja las entidades de `src/Stoxx.Web/Data/Entities.cs` y las reglas configuradas en `AppDbContext.cs`. Los importes se configuran con precisión de dos decimales. El diagrama muestra asociaciones observadas en el código.

## Entidades

| Entidad | Propósito | Campos relevantes |
|---|---|---|
| `AppUser` | Cuenta de acceso y rol. | Nombre, usuario único, hash de contraseña, rol y fecha de creación. |
| `Product` | Catálogo y existencias. | Código único, nombre, categoría, costo, precio, stock, mínimo y estado activo. |
| `Supplier` | Proveedor. | Nombre, contacto, teléfono, correo, notas y estado activo. |
| `Sale` | Cabecera de una venta. | Fecha UTC, forma de pago, total, cajero y sesión de caja. |
| `SaleItem` | Producto y cantidad incluidos en la venta. | Producto, nombre histórico, cantidad, precio y costo unitarios. |
| `Purchase` | Recepción de compra de un producto. | Proveedor, producto, nombres históricos, cantidad, costo, total y usuario que registró. |
| `CashSession` | Apertura y cierre de caja. | Fechas, montos de apertura/cierre y responsables. |

## Integridad observada

- El nombre de usuario y el código de producto tienen índices únicos.
- Los detalles de venta y las compras mantienen referencias a sus productos; las ventas también refieren a una sesión de caja y las compras a un proveedor.
- Las relaciones históricas restringen la eliminación física de productos, sesiones o proveedores referidos.
- No se encontraron archivos de migración; el código inicializa el esquema con `EnsureCreated`.

Consulta el [diagrama entidad-relación](diagramas/modelo-de-datos.md). Este documento no sustituye una inspección de la base de datos de producción.
