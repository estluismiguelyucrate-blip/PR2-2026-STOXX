# PR2-2026-STOXX

## Información del proyecto

- **Nombre del repositorio:** PR2-2026-STOXX.
- **Nombre usado en los materiales:** Sistema Web de Ventas e Inventario (hoja de Gantt); la presentación lo denomina “Ventas e inventario”.
- **Problema:** En pequeñas tiendas y otros comercios, las ventas y el inventario suelen registrarse por separado o manualmente. Al no descontarse automáticamente el stock al vender, pueden pasar inadvertidos los productos agotados, acumularse mercancías de baja rotación y tomar tiempo los cierres de caja.
- **Propósito:** Automatizar procesos básicos de ventas, inventario y caja para apoyar la gestión de pequeños comercios.
- **Objetivos documentados:** Registrar ventas con pago en efectivo o QR y actualizar el stock; alertar sobre existencias bajas; apoyar la reposición y gestión de proveedores; generar reportes diarios; y ofrecer una interfaz sencilla.
- **Usuarios identificados:** Propietarios o administradores, vendedores o cajeros y personal encargado del inventario. El documento también identifica a los clientes como personas afectadas por el problema.
- **Descripción general:** Los materiales describen un sistema para ventas, caja, inventario, compras y proveedores, alertas de stock y reportes. La presentación plantea una aplicación web y menciona acceso desde navegador.

## Integrantes

Los archivos de integrantes están en [`equipo/`](equipo/).

| Integrante | Responsabilidad indicada |
|---|---|
| Luis Miguel Yucra Terrazas | Desarrollador principal; el documento del proyecto también le asigna Scrum Master. |
| Heydam Matías Céspedes Hurtado | Análisis de sistemas y base de datos. |
| Magdiel Simón Grandidier Alegría | Pruebas (QA) y documentación. |

**Dato por confirmar:** `equipo/QA.txt` escribe el nombre como “Magdiel Siomn Grandidier Alegria”, mientras el documento del proyecto y la presentación usan “Magdiel Simón Grandidier Alegría”.

## Estado actual

El repositorio contiene el documento del proyecto, la presentación y materiales de planificación. Los archivos entregados no incluyen implementación ni pruebas del sistema. El Gantt contempla tareas de desarrollo posteriores a las tareas iniciales de documentación y organización.

**Decisión pendiente del equipo:** los documentos discrepan sobre la tecnología y el tipo de aplicación. El PDF menciona Java para un módulo, C++ y una aplicación de escritorio en una sección, y C#/ASP.NET Core y una aplicación web en otra; la presentación describe una aplicación web con C#. Este README no elige una alternativa. El equipo debe confirmar cuál es la definición vigente antes de iniciar la implementación.

## Documentos

- [Documento del proyecto (PDF)](docs/proyecto/Proyecto_Programa_Unido_con_Gantt.pdf)
- [Presentación](docs/presentacion/Presentacion.pptx)
- [Gantt editable (Excel)](docs/gantt/Diagrama_Gantt_tabla.xlsx)
- [Diagrama de Gantt (imagen)](docs/gantt/Diagrama_Gantt.png)

## Estructura

- `docs/proyecto/`: documento del proyecto.
- `docs/presentacion/`: presentación.
- `docs/gantt/`: planificación editable y su imagen.
- `docs/reuniones/`: documentos de reuniones.
- `equipo/`: integrantes y responsabilidades.
- `src/`: código fuente cuando comience el desarrollo.
- `tests/`: pruebas.
- `resources/`: imágenes, base de datos y otros recursos.
- `QA/`: casos de prueba y evidencias.
