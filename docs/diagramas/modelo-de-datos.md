# Diagrama del modelo de datos

Este diagrama resume las relaciones configuradas en `AppDbContext`. Los campos entre paréntesis indican referencias; no es un esquema de migraciones.

```mermaid
erDiagram
    APP_USER {
        int Id PK
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
    }
    SUPPLIER {
        int Id PK
        string Name
        string Email
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
        int Quantity
        decimal UnitPrice
        decimal UnitCost
    }
    PURCHASE {
        int Id PK
        int SupplierId FK
        int ProductId FK
        int Quantity
        decimal UnitCost
        decimal TotalCost
    }
    CASH_SESSION ||--o{ SALE : registra
    SALE ||--|{ SALE_ITEM : contiene
    PRODUCT ||--o{ SALE_ITEM : vendido
    PRODUCT ||--o{ PURCHASE : recibido
    SUPPLIER ||--o{ PURCHASE : suministra
```

Consulta la explicación de [entidades y restricciones](../modelo-de-datos.md).
