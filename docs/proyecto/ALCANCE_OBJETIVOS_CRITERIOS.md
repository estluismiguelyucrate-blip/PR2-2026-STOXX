# Definición del proyecto STOXX

**Proyecto:** PR2-2026-STOXX  
**Sistema:** Gestión de ventas e inventario para pequeños comercios  
**Estado del documento:** propuesta de alcance para revisión y aprobación del equipo  
**Última actualización:** 20 de octubre de 2026

## 1. Problema

En pequeños comercios, registrar las ventas y las existencias por separado puede provocar diferencias de inventario, faltantes difíciles de detectar y errores al cuadrar la caja. STOXX busca centralizar estos registros en una aplicación web.

## 2. Objetivo general

Implementar y validar una aplicación web que permita registrar ventas, actualizar el inventario y consultar los movimientos principales de caja de un pequeño comercio desde un solo sistema.

## 3. Objetivos específicos y métricas

1. Registrar productos con nombre, costo, precio, cantidad y nivel mínimo de existencias.
2. Registrar ventas en efectivo o QR y comprobar que el stock se actualice correctamente en cada venta confirmada.
3. Impedir una venta cuando la cantidad solicitada supere las existencias disponibles.
4. Permitir abrir y cerrar caja, mostrando el efectivo esperado y la diferencia con el efectivo contado.
5. Registrar proveedores y compras recibidas, actualizando las existencias correspondientes.
6. Mostrar un resumen diario que permita revisar ventas, formas de pago y alertas de stock bajo.
7. Validar el acceso inicial, la autenticación y los permisos de administrador, cajero e inventario.

## 4. Alcance del MVP

### Incluido

- Configuración inicial de la cuenta administradora.
- Inicio de sesión y roles existentes.
- Registro y consulta de productos.
- Registro de ventas en efectivo o QR y actualización de inventario.
- Control de stock insuficiente.
- Apertura y cierre de caja.
- Registro de proveedores y compras recibidas.
- Alertas de stock mínimo y resumen diario.
- Persistencia de datos con SQLite y configuración de despliegue en Render.

### Fuera del alcance actual

- Integración real con una pasarela bancaria o validación automática de pagos QR.
- Facturación fiscal electrónica.
- Aplicación móvil nativa.
- Integraciones con sistemas contables externos.
- Multiempresa, múltiples sucursales o sincronización entre tiendas.
- Nuevos módulos que no sean necesarios para validar el MVP.

**Nota sobre QR:** registrar una venta como pagada por QR no equivale a confirmar el pago directamente con un banco. Esa integración queda fuera del MVP.

## 5. Criterios de aceptación verificables

El MVP se considerará listo para revisión cuando el equipo haya ejecutado y documentado los siguientes criterios:

| ID | Criterio | Evidencia requerida |
|---|---|---|
| CA-01 | La aplicación abre desde la URL publicada o se documenta claramente la incidencia del servicio. | Captura con fecha y URL, sin datos sensibles. |
| CA-02 | Es posible crear la cuenta administradora inicial siguiendo el flujo previsto. | Captura del resultado; nunca mostrar la contraseña. |
| CA-03 | Un usuario autenticado puede registrar y consultar un producto. | Capturas antes/después o registro de prueba. |
| CA-04 | Una venta válida actualiza el stock según la cantidad vendida. | Stock antes, venta y stock después. |
| CA-05 | Una venta superior al stock disponible se rechaza sin descontar unidades. | Mensaje del sistema y stock sin cambios. |
| CA-06 | La apertura y el cierre de caja muestran los importes esperados. | Datos de prueba y resultado calculado. |
| CA-07 | Una compra recibida aumenta las existencias del producto. | Cantidad antes y después de recibir la compra. |
| CA-08 | El sistema conserva los datos después de reiniciar la aplicación o el servicio. | Mismo registro visible antes y después del reinicio. |
| CA-09 | Los permisos por rol impiden las operaciones no autorizadas. | Matriz de permisos y capturas de intentos permitidos/denegados. |
| CA-10 | El resumen diario y las alertas coinciden con los movimientos de prueba. | Cálculo manual comparado con el resultado del sistema. |

Estos criterios son objetivos de aceptación, no una afirmación de que ya estén aprobados. El estado de cada uno debe registrarse en la matriz de pruebas.

## 6. Restricciones y supuestos

- La aplicación está desarrollada en C# con ASP.NET Core/Blazor Server y SQLite.
- El catálogo de productos empieza vacío; el equipo debe introducir datos ficticios para las pruebas.
- La URL pública depende de la disponibilidad y configuración del servicio de Render.
- La persistencia en producción depende de que el volumen de datos esté correctamente montado y configurado.
- Las pruebas deben realizarse con información ficticia, sin contraseñas reales ni datos personales.

## 7. Definición de terminado

El MVP estará preparado para evaluación cuando:
- Los archivos de documentación estén accesibles desde el repositorio.
- Los criterios CA-01 a CA-10 tengan resultado registrado: aprobado, fallido o bloqueado.
- Cada prueba ejecutada tenga fecha, responsable y evidencia identificada.
- Los defectos críticos estén corregidos o tengan una limitación y un plan de acción documentados.
- El equipo revise y apruebe el alcance, las responsabilidades y el cronograma.
