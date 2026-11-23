# Matriz de pruebas del MVP STOXX

**Versión:** 1.0  
**Fecha de preparación:** 20 de octubre de 2026  
**Responsable de QA/documentación:** Magdiel Siomn Grandidier Alegria

## Instrucciones

Ejecutar cada caso con datos ficticios. Antes de la ejecución, registrar la fecha y el entorno (local o Render). Guardar capturas en `QA/evidencias/` y nombrarlas con el ID del caso. No incluir contraseñas, tokens, datos personales ni secretos.

**Estado inicial:** pendiente de ejecución. Esta matriz no constituye evidencia de que las pruebas hayan pasado.

| ID | Prueba y pasos | Resultado esperado | Estado | Evidencia |
|---|---|---|---|---|
| TC-01 | Abrir la URL pública y navegar hasta la pantalla inicial. | La página carga sin error; si falla, registrar el mensaje y la hora. | Aprobado: cargó el formulario inicial tras el arranque en frío. | [E-06 — Render](../evidencias/E-06-render-acceso-2026-10-20.md) |
| TC-02 | En un entorno de prueba limpio, seguir el flujo de creación del primer administrador. | La cuenta se crea una sola vez y permite iniciar sesión. | Pendiente | `TC-02-admin.png` |
| TC-03 | Iniciar sesión con una cuenta válida. | Se accede a las secciones autorizadas para el rol. | Pendiente | `TC-03-login.png` |
| TC-04 | Intentar iniciar sesión con una contraseña incorrecta. | El acceso se rechaza sin revelar información sensible. | Pendiente | `TC-04-login-rechazado.png` |
| TC-05 | Crear un producto de prueba con cantidad inicial 10. | El producto aparece en el catálogo con stock 10. | Pendiente | `TC-05-producto.png` |
| TC-06 | Vender 2 unidades de un producto con stock 10. | La venta se registra y el stock queda en 8. | Pendiente | `TC-06-venta-stock.png` |
| TC-07 | Intentar vender más unidades que el stock disponible. | La operación se rechaza y el stock no cambia. | Pendiente | `TC-07-stock-insuficiente.png` |
| TC-08 | Abrir caja con monto inicial ficticio y luego cerrar registrando efectivo contado. | Se muestran efectivo esperado y diferencia de forma coherente con los movimientos. | Pendiente | `TC-08-caja.png` |
| TC-09 | Registrar una compra recibida de 5 unidades. | El stock aumenta en 5 unidades tras confirmar la recepción. | Pendiente | `TC-09-compra.png` |
| TC-10 | Consultar el resumen diario después de realizar ventas de prueba. | Los totales coinciden con el cálculo manual de esas ventas. | Pendiente | `TC-10-reporte.png` |
| TC-11 | Iniciar sesión con cada rol disponible e intentar acceder a operaciones de otros roles. | Las operaciones restringidas no pueden realizarse por un usuario sin permiso. | Pendiente | `TC-11-roles.png` |
| TC-12 | Crear un registro de prueba, reiniciar el servicio y volver a consultarlo. | El registro continúa disponible en el entorno con persistencia configurada. | Pendiente | `TC-12-persistencia.png` |
| TC-13 | Consultar una alerta con stock por debajo del mínimo configurado. | El producto se identifica como stock bajo según la regla configurada. | Pendiente | `TC-13-alerta-stock.png` |

## Ejecución local adicional del 20-10-2026

Se realizó un recorrido HTTP aislado con datos ficticios y una base SQLite temporal. Pasaron alta inicial de administrador (incluido el rechazo del segundo alta), 401 sin sesión, permisos muestreados de Cajero e Inventario, compra y actualización de existencias, venta en efectivo y QR, rechazo de sobreventa, cierre de caja, totales de reporte y recuperación de usuarios/productos/compras/ventas después de reiniciar el proceso local. El registro detallado está en [E-05](../evidencias/qa-runtime-2026-10-20.md).

Esta ejecución no se hizo contra Render y no cubre todos los pasos/datos específicos de TC-01–TC-13. Por eso se mantiene el resumen formal de casos como pendiente de registro, y no se afirma aprobación del cuestionario.

## Pruebas automatizadas

Se incorporó un proyecto de pruebas de integración en `tests/Stoxx.Web.Tests/` y un flujo de GitHub Actions en `.github/workflows/dotnet-ci.yml`. Las pruebas automatizadas cubren el endpoint de salud, la creación inicial de administrador de un solo uso, inicio de sesión, rechazo de usuarios no autenticados y restricción de gestión de usuarios para el rol cajero.

**Estado de ejecución:** el flujo del commit `f84e1d5` falló (3 de 5 pruebas) porque la configuración inicial de administrador respondió 409 en vez de 200. Se corrigió el aislamiento de la base temporal por fábrica de prueba. En el commit `32bed0c`, GitHub Actions compiló sin advertencias ni errores y aprobó las 5 pruebas automáticas. Ver [E-07 — CI](../evidencias/E-07-github-actions-2026-10-20.md). Las pruebas manuales de venta, compras, caja, reportes y persistencia en Render siguen requiriendo ejecución y evidencias reales.

## Registro de ejecución

Copiar este bloque por cada prueba ejecutada:

- **ID de caso:**
- **Fecha y hora:**
- **Responsable:**
- **Entorno:** local / Render
- **Versión o commit probado:**
- **Datos de prueba utilizados:**
- **Resultado obtenido:**
- **Estado:** aprobado / fallido / bloqueado
- **Evidencia (ruta dentro del repositorio):**
- **Incidencia encontrada y seguimiento:**

## Regla para declarar aprobado

Marcar una prueba como **aprobada** únicamente después de ejecutarla y comprobar el resultado esperado. Si no se puede ejecutar, usar **bloqueada** y explicar el motivo. Si el resultado no coincide, usar **fallida**, guardar evidencia y registrar la corrección pendiente.

## Resumen de resultados

| Indicador | Valor actual |
|---|---:|
| Casos definidos | 13 |
| Aprobados | 1 registrado (TC-01, Render) |
| Fallidos | 0 registrados |
| Bloqueados | 0 registrados |
| Pendientes de ejecutar | 12 |

Los valores cuentan únicamente los casos manuales TC-01–TC-13. El resultado automatizado de GitHub Actions se informa por separado arriba.
