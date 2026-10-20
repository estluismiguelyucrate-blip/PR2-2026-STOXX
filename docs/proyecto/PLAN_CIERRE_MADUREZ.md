# Lista de cierre para el cuestionario de madurez

Usar esta lista en la reunión del equipo. Marcar una tarea como completa solo cuando el enlace, la revisión o la prueba se haya comprobado.

## Observación sobre la consistencia de las fuentes

El PDF del proyecto contiene versiones técnicas incompatibles: las páginas iniciales describen una aplicación de escritorio en C++/Java y páginas posteriores describen una aplicación web en C#; el repositorio actual contiene una aplicación ASP.NET Core con SQLite. Se conservaron ambos documentos fuente y se prepararon una [copia del PDF con una nota técnica complementaria](Proyecto_Programa_Unido_con_Gantt_actualizado.pdf) y una [presentación actualizada](../presentacion/Presentacion-actualizada-v2.pptx) con la arquitectura y estado observados. El equipo aún debe revisar y aprobar su uso. La observación del Gantt original está resumida en [seguimiento](../gantt/seguimiento.md).

## 1. Documentación

- [ ] Abrir desde GitHub el PDF del proyecto.
- [ ] Abrir desde GitHub la presentación PPTX.
- [ ] Abrir desde GitHub el Gantt editable y su imagen.
- [ ] Confirmar que el README enlaza todos los documentos.
- [ ] Revisar que los documentos tengan el mismo nombre del proyecto y alcance actualizado.

Enlaces:
- [Documento del proyecto](Proyecto_Programa_Unido_con_Gantt.pdf)
- [Presentación](../presentacion/Presentacion.pptx)
- [Gantt editable](../gantt/Diagrama_Gantt_tabla.xlsx)
- [Gantt en imagen](../gantt/Diagrama_Gantt.png)

## 2. Definición del proyecto

- [ ] El equipo aprueba el objetivo general y los objetivos específicos.
- [ ] El equipo confirma qué entra y qué queda fuera del MVP.
- [ ] Los criterios CA-01 a CA-10 están entendidos y se pueden comprobar.
- [ ] Las limitaciones de pago QR, facturación y despliegue están explicadas.

Documento: [Objetivos, alcance y criterios](ALCANCE_OBJETIVOS_CRITERIOS.md).

## 3. Pruebas y calidad

- [ ] Ejecutar las 13 pruebas de la [matriz de QA](../../QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md).
- [ ] Registrar fecha, entorno, responsable, resultado y commit probado.
- [ ] Guardar capturas reales en [evidencias](../../QA/evidencias/README.md).
- [ ] Registrar defectos fallidos/bloqueados y su seguimiento.
- [ ] Repetir las pruebas que fallen después de aplicar una corrección.

## 4. Planificación

- [ ] El equipo confirma las fechas propuestas del [cronograma](../planificacion/CRONOGRAMA_MADUREZ.md).
- [ ] Se confirma un responsable principal y apoyo para cada tarea.
- [ ] Se actualiza el estado real de cada actividad.
- [ ] Se deja un acta breve de la reunión de revisión.

## 5. Equipo y riesgos

- [ ] Los integrantes confirman sus responsabilidades.
- [ ] Se revisan los riesgos y se asigna seguimiento.
- [ ] Los riesgos críticos tienen mitigación o plan de contingencia.

Documento: [Responsabilidades y riesgos](../gestion/RIESGOS_Y_RESPONSABILIDADES.md).

## 6. Validación técnica y seguridad

- [x] La URL de Render carga y se guarda evidencia fechada: [E-06](../../QA/evidencias/E-06-render-acceso-2026-10-20.md). La pantalla de alta inicial sigue pendiente de completar por el administrador designado.
- [ ] La creación inicial del administrador se prueba en entorno controlado.
- [ ] Se verifica que el inicio de sesión rechace credenciales incorrectas.
- [ ] Se comprueban los permisos de administrador, cajero e inventario.
- [ ] Una venta válida descuenta las unidades correctas.
- [ ] Una venta sin stock suficiente se rechaza sin cambiar el inventario.
- [ ] La compra recibida aumenta el stock correctamente.
- [ ] Apertura, cierre de caja y reporte diario coinciden con los cálculos de prueba.
- [ ] Los datos permanecen después de reiniciar el servicio.

## Decisión final

- [ ] **Listo para presentar:** documentos accesibles, criterios revisados, pruebas críticas ejecutadas y evidencias adjuntas.
- [ ] **No listo todavía:** quedan pruebas críticas pendientes, fallidas o bloqueadas.

**No declarar aprobado el proyecto solo por tener los documentos creados.** La aprobación requiere revisar resultados reales, cerrar defectos críticos y acordar la decisión entre los integrantes.
