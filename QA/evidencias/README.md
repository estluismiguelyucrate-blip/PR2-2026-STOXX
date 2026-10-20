# Evidencias de calidad

Guardar aquí las capturas y los registros que demuestren la ejecución real de los casos definidos en [la matriz de pruebas](../casos-prueba/MATRIZ_PRUEBAS_MVP.md).

## Convención de nombres

Usar los nombres TC-01 a TC-13 definidos en la matriz, por ejemplo:
- `TC-01-URL.png`
- `TC-06-venta-stock.png`
- `TC-11-roles.png`

## Reglas

1. Las capturas deben corresponder a una ejecución real y mostrar el resultado relevante.
2. Indicar en la matriz la fecha, responsable, entorno y resultado de cada prueba.
3. No crear capturas ficticias ni marcar pruebas como aprobadas sin ejecutarlas.
4. Ocultar contraseñas, cookies, tokens, información personal y datos de clientes.
5. Para cálculos de caja e inventario, incluir los datos de prueba necesarios para comprobar el resultado sin exponer información real.
6. Si la aplicación publicada no está disponible, guardar una captura del error con fecha y registrar la prueba como bloqueada; no sustituirla por una captura de otro entorno.

## Evidencias incorporadas el 20-10-2026

- `E-01-resumen.png` a `E-04-configuracion-inicial.png`: copias anonimizadas de capturas proporcionadas por el equipo; muestran pantallas de escritorio, no validan por sí solas una operación.
- [E-05 — ejecución local](qa-runtime-2026-10-20.md): registro de recorrido HTTP con base SQLite temporal y datos ficticios. No corresponde a Render.
- [E-06 — acceso público a Render](E-06-render-acceso-2026-10-20.md): comprobación de solo lectura; la URL cargó y solicitó configurar el primer administrador. No se creó una cuenta en producción.

La matriz TC-01–TC-13 conserva el seguimiento formal de esas pruebas; los resultados de E-05 son una comprobación local adicional y no deben marcar automáticamente como aprobados los casos ejecutados con otro entorno/datos.
