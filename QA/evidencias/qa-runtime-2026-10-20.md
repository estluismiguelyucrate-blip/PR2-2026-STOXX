# E-05 — Ejecución funcional local

**Fecha:** 20-10-2026. **Entorno:** ASP.NET Core Development, HTTP loopback, base SQLite nueva en carpeta temporal fuera del repositorio. La base existente no se abrió ni modificó. Se usaron usuarios, producto, proveedor y contraseña temporales; no se guarda la contraseña ni el contenido de cookies.

## Resultados observados

| Comprobación | Resultado real | Estado |
|---|---:|---|
| `/api/health` | 200 | Aprobado |
| Alta del primer administrador | 200 | Aprobado |
| Alta inicial repetida | 409 | Aprobado: bloqueada tras inicialización |
| `/api/users` sin sesión | 401 | Aprobado |
| Alta de cuentas de Cajero e Inventario por Admin | 201 | Aprobado |
| Crear producto y proveedor por Admin | 201 | Aprobado |
| Registrar compra de 2 unidades a costo Bs 6 | 201; stock subió de 5 a 7 | Aprobado |
| Operación de venta como Inventario | 403 | Aprobado: denegada |
| Abrir caja con Bs 100 | 201 | Aprobado |
| Venta efectivo de 1 unidad a Bs 12,50 | 201; stock 7 a 6 | Aprobado |
| Intentar vender 7 unidades con 6 disponibles | 409 | Aprobado: rechazada |
| Venta QR de 2 unidades a Bs 12,50 | 201; stock final 4 | Aprobado |
| Cierre con Bs 112,50 contados | 200; diferencia Bs 0 | Aprobado |
| Reporte diario | 2 ventas; Bs 37,50 total; efectivo Bs 12,50; QR Bs 25; ganancia bruta Bs 19,50 | Aprobado: coincide con cálculos |
| Cajero: consultar usuarios, crear producto o registrar compra | 403 en las tres rutas | Aprobado: denegadas |
| Cajero: consultar ventas | 200 | Aprobado: permitido |
| Reinicio del proceso; recuperar productos, compras y ventas | Datos presentes; stock 4, ventas 2 y compra 1 | Aprobado en SQLite temporal local |

La compilación `dotnet build src/Stoxx.Web/Stoxx.Web.csproj --no-restore` finalizó con **0 advertencias y 0 errores**.

## Límites de esta evidencia

Esta sesión acredita rutas HTTP en una instancia local y base desechable. No comprueba el despliegue real de Render, HTTPS, almacenamiento persistente del servicio publicado, copias/restauración, carga de 100 usuarios ni presentación móvil. El alta de primer administrador se probó después de crear explícitamente una base vacía: como la ruta es pública hasta crear la primera cuenta, el administrador designado debe completarla antes de compartir la URL pública.
