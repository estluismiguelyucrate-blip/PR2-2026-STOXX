# Requisitos del sistema

Estos requisitos describen comportamientos presentes en el código del MVP. Su aprobación como requisitos del equipo y su validación mediante pruebas deben registrarse por separado en la [matriz QA](../QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md). Los criterios de aceptación complementarios están en [objetivos y alcance](proyecto/ALCANCE_OBJETIVOS_CRITERIOS.md).

## Requisitos funcionales

| ID | Requisito | Verificación propuesta |
|---|---|---|
| RF-01 | El sistema debe permitir crear una única cuenta administradora inicial cuando no existan usuarios. | Ejecutar configuración inicial una vez y comprobar que el segundo intento se rechace. |
| RF-02 | El sistema debe autenticar usuarios y asignar los roles Administrador, Cajero o Inventario. | Iniciar sesión con cada rol y consultar estado de autenticación. |
| RF-03 | El administrador debe poder crear usuarios y consultar sus nombres y roles. | Probar operaciones permitidas y denegadas por rol. |
| RF-04 | Administrador e Inventario deben poder registrar, editar, consultar y desactivar productos. | Crear producto y verificar catálogo, código único y baja lógica. |
| RF-05 | Administrador e Inventario deben poder registrar proveedores y compras recibidas. | Registrar compra y comprobar el aumento del stock. |
| RF-06 | Administrador y Cajero deben poder abrir y cerrar caja. | Comprobar efectivo esperado y diferencia con el monto contado. |
| RF-07 | Administrador y Cajero deben registrar ventas en efectivo o QR. | Registrar venta válida con cada método de pago. |
| RF-08 | Una venta confirmada debe descontar el stock y conservar sus partidas y precios históricos. | Comparar stock antes/después y consultar el detalle de venta. |
| RF-09 | El sistema debe rechazar ventas sin caja abierta, de productos inactivos o con stock insuficiente. | Ejecutar casos negativos y comprobar que no haya cambios parciales. |
| RF-10 | El sistema debe mostrar alertas de stock bajo y un resumen/reporte diario de ventas. | Comparar totales y umbrales con un conjunto conocido de datos ficticios. |
| RF-11 | El sistema debe exponer un endpoint de salud para comprobar disponibilidad. | Consultar `/api/health` y esperar estado HTTP 200. |

## Requisitos no funcionales

| ID | Requisito | Verificación propuesta |
|---|---|---|
| RNF-01 | La aplicación debe poder compilarse y ejecutarse con .NET SDK 10 en Windows, macOS o Linux compatible. | Restaurar y ejecutar las instrucciones del README en un entorno limpio. |
| RNF-02 | Los datos operativos y las claves de protección deben persistir en el directorio de datos configurado. | Reiniciar el proceso y verificar registros de prueba; en Render, verificar el volumen persistente. |
| RNF-03 | Las rutas protegidas deben exigir autenticación y respetar los roles configurados. | Ejecutar pruebas automatizadas y manuales para sesiones ausentes y roles diferentes. |
| RNF-04 | Las contraseñas no deben almacenarse en texto plano y las cookies de sesión deben usar atributos seguros. | Inspección de configuración y pruebas de autenticación; verificar HTTPS en producción. |
| RNF-05 | Los cálculos monetarios deben conservar hasta dos decimales. | Verificar operaciones de ventas, compras, caja y reportes con valores decimales. |
| RNF-06 | La aplicación debe manejar errores inesperados con respuesta genérica y registro en servidor. | Provocar un error controlado en un entorno local y revisar respuesta y logs. |
| RNF-07 | El flujo de CI debe compilar el proyecto y ejecutar las pruebas automatizadas en cambios a `main`. | Revisar el resultado de GitHub Actions en el commit correspondiente. |
| RNF-08 | La documentación debe permitir preparar el entorno sin credenciales privadas ni datos reales. | Seguir el README desde un clon limpio usando solo datos ficticios. |

## Fuera del MVP

No se incluyen integración bancaria para pagos QR, facturación fiscal electrónica, aplicación móvil nativa, contabilidad externa ni operación multiempresa/multisucursal. El equipo puede proponer cambios mediante issue y actualizar este documento si aprueba una ampliación del alcance.
