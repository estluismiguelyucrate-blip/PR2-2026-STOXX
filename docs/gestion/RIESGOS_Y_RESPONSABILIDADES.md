# Responsabilidades y riesgos del proyecto STOXX

**Fecha:** 20 de octubre de 2026  
**Estado:** propuesta de organización para confirmación del equipo.

## 1. Responsabilidades del equipo

| Integrante | Responsabilidad principal registrada | Responsabilidades para el cierre de madurez |
|---|---|---|
| Luis Miguel Yucra Terrazas | Desarrollador principal | Revisar el código y el despliegue; apoyar las pruebas de flujos, permisos y persistencia; documentar defectos y correcciones. |
| Heydam Matias Cespedes Hurtado | Análisis y base de datos | Revisar alcance y reglas del negocio; comprobar consistencia de stock, ventas y caja; apoyar la verificación de persistencia y riesgos técnicos. |
| Magdiel Siomn Grandidier Alegria | QA y documentación | Mantener la matriz de pruebas; coordinar evidencias reales; registrar resultados, incidencias y enlaces documentales. |

Las responsabilidades se basan en los archivos de roles que ya están en la carpeta `equipo/`. El equipo debe confirmar la distribución y acordar quién cubre tareas cuando un integrante no esté disponible.

## 2. Registro de riesgos

| ID | Riesgo | Probabilidad inicial | Impacto | Mitigación | Responsable de seguimiento |
|---|---|---|---|---|---|
| R-01 | El servicio de Render no carga durante la evaluación. | Media | Alto | Verificar la URL antes de presentar; revisar logs y estado del servicio; tener un entorno local como respaldo y registrar qué entorno se probó. | Luis Miguel |
| R-02 | Los datos se pierden al reiniciar o volver a desplegar. | Media | Alto | Verificar el montaje del volumen persistente y la ruta configurada; ejecutar la prueba TC-12. | Luis Miguel y Heydam |
| R-03 | Un rol puede realizar acciones no autorizadas. | Media | Alto | Probar permisos por rol con cuentas de prueba y corregir cualquier acceso indebido antes de la aprobación. | Luis Miguel |
| R-04 | Una venta descuenta mal el inventario o permite stock negativo. | Media | Alto | Ejecutar TC-06 y TC-07 con cantidades controladas; comprobar stock antes y después. | Heydam y Magdiel |
| R-05 | La documentación apunta a archivos ausentes o con nombres distintos. | Media | Medio | Abrir cada enlace desde una sesión externa y mantener rutas relativas exactas en README. | Magdiel |
| R-06 | Se presentan resultados sin evidencia verificable. | Media | Alto | No aprobar casos sin captura o registro; mantener la matriz actualizada y nombrar archivos por ID de prueba. | Magdiel |
| R-07 | Un integrante no puede completar su tarea a tiempo. | Media | Medio | Dividir tareas pequeñas, registrar bloqueos en reuniones y asignar apoyo o reemplazo explícito. | Equipo completo |
| R-08 | La configuración inicial de administrador queda expuesta o se permite crear administradores de forma indebida. | Media | Alto | Probar el primer inicio en entorno controlado; verificar que el alta inicial quede cerrada después de crear la cuenta y revisar restricciones de roles. | Luis Miguel |

Las probabilidades son estimaciones iniciales del equipo, no mediciones estadísticas. Deben actualizarse al revisar la situación real.

## 3. Reglas mínimas de seguridad para la demostración

- Usar cuentas y datos ficticios.
- No subir contraseñas, tokens, archivos de configuración con secretos ni bases de datos con datos reales.
- No mostrar contraseñas en capturas.
- Probar tanto operaciones permitidas como operaciones denegadas por cada rol.
- Comprobar que la creación inicial del administrador no se pueda reutilizar indebidamente después de la configuración.
- Confirmar que la base de datos persiste en el entorno de Render y no depende de un almacenamiento temporal.
- Registrar cualquier riesgo no resuelto y no afirmar que la seguridad está validada hasta completar las pruebas.

## 4. Registro de decisiones de reunión

Añadir una entrada por reunión:

- **Fecha:**
- **Asistentes:**
- **Tareas terminadas:**
- **Bloqueos e incidencias:**
- **Decisiones:**
- **Tareas siguientes / responsable / fecha:**
