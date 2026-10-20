# PR2-2026-STOXX

Aplicación web académica para registrar ventas, controlar inventario y consultar cierres de caja de un pequeño comercio.

## Aplicación publicada

**Abrir la aplicación web:** [https://stocks-con-3x.onrender.com/](https://stocks-con-3x.onrender.com/)

## Estado de madurez del proyecto

Se incorporó una carpeta de documentación para organizar los criterios de aprobación. Los documentos describen qué debe comprobarse y dónde guardar las evidencias. **La existencia de estos documentos no significa que las pruebas técnicas ya estén aprobadas**: el equipo debe ejecutar cada caso y registrar el resultado real.

- [Objetivos, alcance y criterios de aceptación](docs/proyecto/ALCANCE_OBJETIVOS_CRITERIOS.md)
- [Cronograma de cierre de brechas](docs/planificacion/CRONOGRAMA_MADUREZ.md)
- [Responsabilidades y matriz de riesgos](docs/gestion/RIESGOS_Y_RESPONSABILIDADES.md)
- [Matriz de pruebas del MVP](QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md)
- [Cómo guardar las evidencias](QA/evidencias/README.md)

## Problema y propósito

El proyecto busca ayudar a pequeños comercios a registrar ventas y existencias en un mismo sistema. Cuando las ventas y el inventario se anotan por separado, pueden aparecer diferencias de stock, dificultades para planificar compras y errores al cuadrar la caja.

El sistema reúne ventas, inventario, proveedores, compras y reportes para facilitar esos registros. Está pensado para propietarios o administradores, vendedores o cajeros y responsables de inventario.

## Qué incluye esta versión

- Inicio de sesión y cuentas con roles de administrador, cajero e inventario.
- Configuración inicial para crear la primera cuenta administradora.
- Catálogo de productos, precios, existencias y umbrales de stock mínimo.
- Registro de ventas en efectivo o QR con descuento de existencias y control de stock insuficiente.
- Apertura y cierre de caja, con cálculo del efectivo esperado y diferencia frente al conteo.
- Registro de proveedores y compras recibidas que actualizan el inventario.
- Resumen diario, alertas de stock bajo, totales por forma de pago, costo de mercancía, ganancia bruta y productos vendidos.
- Base de datos SQLite. El catálogo empieza vacío para que el equipo introduzca datos de demostración.

## Tecnologías

La aplicación utiliza C# con ASP.NET Core y Blazor Server para la interfaz y la lógica. SQLite guarda los datos.

## Requisitos y ejecución local

- .NET SDK 10.
- Windows, macOS o Linux con soporte para ASP.NET Core.
- Conexión a Internet la primera vez para restaurar el paquete de SQLite.

Desde la raíz del repositorio, ejecuta:

```powershell
dotnet run --project src/Stoxx.Web/Stoxx.Web.csproj
```

Abre [http://localhost:5080](http://localhost:5080). En el primer inicio, sigue el flujo de creación de la cuenta administradora. Después agrega productos y proveedores de prueba. No uses datos personales ni financieros reales.

## Publicación en Internet

El repositorio incluye `Dockerfile` y `render.yaml` para el despliegue en Render con almacenamiento persistente. La URL publicada es [https://stocks-con-3x.onrender.com/](https://stocks-con-3x.onrender.com/). El equipo debe verificar que cargue y que las operaciones de prueba se conserven después de reiniciar el servicio.

## Flujo de demostración de extremo a extremo

1. Inicia la aplicación y configura la cuenta administradora inicial.
2. Registra productos con costo, precio, cantidad inicial y stock mínimo.
3. Registra un proveedor y una compra recibida.
4. Abre la caja con un monto inicial.
5. Registra una venta y verifica que se descuente el inventario.
6. Comprueba que un usuario sin permisos no pueda entrar en funciones restringidas.
7. Consulta alertas y reporte diario; cierra la caja con el efectivo contado.
8. Reinicia la aplicación y verifica la persistencia de productos, ventas y movimientos.
9. Guarda capturas y anota el resultado real de cada prueba en la matriz de QA.

## Documentos originales del equipo

- [Documento del proyecto (PDF)](docs/proyecto/Proyecto_Programa_Unido_con_Gantt.pdf)
- [Presentación (PPTX)](docs/presentacion/Presentacion.pptx)
- [Gantt editable (XLSX)](docs/gantt/Diagrama_Gantt_tabla.xlsx)
- [Diagrama de Gantt (PNG)](docs/gantt/Diagrama_Gantt.png)
- [Responsabilidades existentes del equipo](equipo/)

## Estructura principal

- `src/Stoxx.Web/`: aplicación ASP.NET Core, interfaz y persistencia.
- `docs/proyecto/`: documento base, objetivos y alcance.
- `docs/planificacion/`: planificación y tareas pendientes.
- `docs/gestion/`: responsables y riesgos.
- `docs/presentacion/`: presentación del equipo.
- `docs/gantt/`: cronograma original.
- `QA/casos-prueba/`: casos de prueba.
- `QA/evidencias/`: capturas y resultados de ejecución.
- `equipo/`: integrantes y responsabilidades.

## Condición para declarar el proyecto listo

No marcar el proyecto como aprobado hasta que el equipo haya ejecutado las pruebas críticas, registrado resultados y adjuntado evidencias reales. Las fechas y responsables del cronograma deben revisarse y confirmarse por el equipo.
