# Modelo de datos

El esquema vigente está definido por las entidades de `src/Stoxx.Web/Data/Entities.cs` y la configuración de `AppDbContext.cs`. La aplicación crea el esquema con EF Core `EnsureCreated`.

```mermaid
erDiagram
    CASH_SESSION ||--o{ SALE : contiene
    SALE ||--|{ SALE_ITEM : incluye
    PRODUCT ||--o{ SALE_ITEM : vendido_como
    SUPPLIER ||--o{ PURCHASE : abastece
    PRODUCT ||--o{ PURCHASE : recibido_como
    APP_USER {
        int Id PK
        string Name
        string UserName UK
        string PasswordHash
        string Role
    }
    PRODUCT {
        int Id PK
        string Code UK
        string Name
        decimal CostPrice
        decimal UnitPrice
        int Stock
        int MinimumStock
        bool Active
    }
    SUPPLIER {
        int Id PK
        string Name
        string ContactName
        string Phone
        string Email
        bool Active
    }
    CASH_SESSION {
        int Id PK
        datetime OpenedAtUtc
        datetime ClosedAtUtc
        decimal OpeningAmount
        decimal ClosingAmount
    }
    SALE {
        int Id PK
        int CashSessionId FK
        datetime CreatedAtUtc
        string PaymentMethod
        decimal Total
    }
    SALE_ITEM {
        int Id PK
        int SaleId FK
        int ProductId FK
        string ProductName
        int Quantity
        decimal UnitPrice
        decimal UnitCost
    }
    PURCHASE {
        int Id PK
        int SupplierId FK
        int ProductId FK
        string SupplierName
        string ProductName
        int Quantity
        decimal UnitCost
        decimal TotalCost
    }
```

## Entidades y relaciones

- **AppUser:** credenciales, nombre y rol. El nombre de usuario tiene índice único; la contraseña se guarda como hash.
- **Product:** código único, categoría, precios, stock, mínimo y estado activo.
- **Supplier:** datos de contacto y estado activo.
- **CashSession:** apertura, cierre, montos y responsables de caja.
- **Sale:** fecha, método de pago, total, cajero y sesión de caja.
- **SaleItem:** producto y cantidad vendidos, con nombre, costo y precio copiados para conservar el detalle histórico.
- **Purchase:** proveedor, producto, cantidades y costos; conserva sus nombres como instantánea del momento de registro.

Cada venta pertenece a una sesión de caja y tiene partidas. Cada partida referencia un producto. Cada compra referencia un proveedor y un producto. Los registros de venta y compra retienen nombres y precios/costos históricos aunque cambien los datos actuales del catálogo.

## Reglas de persistencia observables

- Los códigos de producto y nombres de usuario son únicos.
- Los campos monetarios se configuran con precisión de 12 dígitos y 2 decimales.
- No se permite eliminar físicamente productos ni proveedores desde la API; se marcan inactivos.
- Las relaciones de partidas de venta, compras y sesiones de caja usan borrado restringido para proteger el historial.
- No hay relación de clave foránea entre `AppUser` y ventas/compras; el responsable se conserva como texto.
