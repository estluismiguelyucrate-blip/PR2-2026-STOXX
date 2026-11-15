# Requisitos del MVP

Los requisitos describen el comportamiento esperado para la aplicación web actual. Su verificación se realiza con la [matriz de pruebas](../QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md); los estados de esa matriz son la fuente del resultado real.

## Requisitos funcionales

| ID | Requisito | Verificación relacionada |
|---|---|---|
| RF-01 | Permitir crear una única cuenta administradora en una base vacía y autenticar usuarios con roles. | TC-02–TC-04, TC-11 |
| RF-02 | Permitir crear, consultar, modificar y desactivar productos con código, precio, costo y existencias. | TC-05 |
| RF-03 | Registrar ventas en efectivo o QR, descontar existencias y rechazar cantidades superiores al stock disponible. | TC-06–TC-07 |
| RF-04 | Permitir abrir y cerrar caja y mostrar el efectivo esperado y la diferencia frente al conteo. | TC-08 |
| RF-05 | Registrar proveedores y compras recibidas, actualizando las existencias. | TC-09 |
| RF-06 | Mostrar resumen diario y alertas para productos bajo el stock mínimo. | TC-10, TC-13 |
| RF-07 | Restringir operaciones según el rol autenticado. | TC-03, TC-11 |
| RF-08 | Conservar los datos al reiniciar el proceso cuando el directorio de datos sea persistente. | TC-12 |

## Requisitos no funcionales

- **RNF-01 — Entorno reproducible:** un desarrollador con .NET SDK 10 debe poder restaurar, compilar, ejecutar y probar el sistema siguiendo el [manual local](manuales/instalacion-y-ejecucion.md).
- **RNF-02 — Persistencia:** la aplicación usa SQLite; la ruta puede configurarse con `STOXX_DATA_DIR`.
- **RNF-03 — Protección de acceso:** contraseñas almacenadas como hash, cookie HttpOnly y restricciones de autorización definidas por rol. Esto documenta decisiones del código, no una certificación de seguridad.
- **RNF-04 — Validación:** los cambios a `main` y las pull requests ejecutan la compilación y las pruebas automatizadas mediante GitHub Actions.
- **RNF-05 — Privacidad de desarrollo:** usar datos ficticios y no versionar secretos, credenciales ni bases de datos con datos personales.

## Límites y supuestos

La selección de QR identifica el método registrado y no confirma una transacción bancaria. La facturación fiscal, la app móvil, la operación multiempresa y las integraciones contables están fuera del MVP. La aceptación del proyecto depende de que el equipo ejecute y documente los casos pendientes.
