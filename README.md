# STOXX

Aplicación web académica para registrar ventas, controlar inventario y consultar cierres de caja de pequeños comercios.

## Estado

- **Proyecto:** PR2-2026-STOXX.
- **Tipo:** MVP académico en desarrollo y validación.
- **Aplicación publicada:** [stocks-con-3x.onrender.com](https://stocks-con-3x.onrender.com/). La disponibilidad y la configuración de la cuenta administradora deben verificarse antes de cada demostración.
- **Estado de calidad:** hay pruebas automatizadas y recorridos locales documentados; los casos manuales pendientes se mantienen como pendientes en la [matriz de QA](QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md).

## Propósito y alcance

STOXX reúne el catálogo, ventas, inventario, compras, caja y reportes diarios para reducir errores al registrar operaciones de un pequeño comercio. Está dirigido a administradores, cajeros y responsables de inventario. El MVP no valida pagos QR con un banco, no emite facturas fiscales y no integra sistemas contables externos.

Las [metas, usuarios, límites y criterios de aceptación](docs/proyecto/ALCANCE_OBJETIVOS_CRITERIOS.md) y los [requisitos verificables](docs/requisitos.md) describen el alcance acordado para revisión del equipo.

## Equipo

- **Luis Miguel Yucra Terrazas:** desarrollo principal.
- **Magdiel Siomn Grandidier Alegria:** QA y documentación.
- **Heydam Matias Cespedes Hurtado:** análisis y base de datos.

El detalle de responsabilidades está en [documentación del equipo](docs/equipo/integrantes-y-roles.md).

## Funcionalidades principales

- Configuración de la primera cuenta administradora, inicio de sesión y roles.
- Catálogo de productos, precios, existencias y stock mínimo.
- Registro de ventas en efectivo o QR con control de stock.
- Apertura y cierre de caja, incluyendo efectivo esperado y diferencia.
- Registro de proveedores y compras recibidas que aumentan el inventario.
- Resumen diario, alertas de stock bajo y totales por forma de pago.
- Persistencia en SQLite.

## Tecnologías y requisitos

- C# y ASP.NET Core 10 con componentes Blazor Server.
- .NET SDK 10 para desarrollo y pruebas.
- SQLite mediante Entity Framework Core.
- Docker para la publicación configurada en Render.

No se necesita instalar SQLite por separado. En el primer inicio, la aplicación crea el directorio de datos y la base de datos.

## Ejecución local

1. Instala el [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
2. Clona el repositorio y abre una terminal en su raíz.
3. Ejecuta:

   ```powershell
   dotnet run --project src/Stoxx.Web/Stoxx.Web.csproj --urls http://localhost:5080
   ```

4. Abre [http://localhost:5080](http://localhost:5080). En una base vacía, completa el flujo de configuración inicial de la cuenta administradora.

Por defecto, los archivos locales se crean en `src/Stoxx.Web/data/`. Para usar otra ubicación, configura `STOXX_DATA_DIR` antes de iniciar la aplicación. No agregues contraseñas, tokens ni datos reales al repositorio. Usa información ficticia para desarrollo y demostraciones.

### Pruebas

Ejecuta la suite de pruebas automatizadas desde la raíz:

```powershell
dotnet test tests/Stoxx.Web.Tests/Stoxx.Web.Tests.csproj --configuration Release
```

La matriz de [pruebas manuales](QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md) indica cómo registrar fecha, entorno, responsable, versión y evidencia. No marques como aprobada una prueba que no se haya ejecutado.

## Documentación

- [Índice de documentación](docs/README.md): guía de ubicación de alcance, requisitos, arquitectura, datos, planificación, diagramas, manuales y equipo.
- [Arquitectura](docs/arquitectura.md), [modelo de datos](docs/modelo-de-datos.md) y [manual de ejecución local](docs/manuales/instalacion-y-ejecucion.md).
- [Criterios de cierre y madurez](docs/proyecto/PLAN_CIERRE_MADUREZ.md).
- [Planificación y cronograma de cierre](docs/planificacion/CRONOGRAMA_MADUREZ.md) y [seguimiento del Gantt](docs/gantt/seguimiento.md).
- [Responsabilidades y riesgos](docs/gestion/RIESGOS_Y_RESPONSABILIDADES.md).
- [Matriz de pruebas](QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md) y [cómo guardar evidencias](QA/evidencias/README.md).
- [Documento del proyecto](docs/proyecto/Proyecto_Programa_Unido_con_Gantt_actualizado.pdf), [presentación](docs/presentacion/Presentacion-actualizada-v2.pptx) y [Gantt editable](docs/gantt/Diagrama_Gantt_tabla.xlsx). Se conservaron las fuentes originales; consulta la nota sobre discrepancias técnicas en el [plan de cierre](docs/proyecto/PLAN_CIERRE_MADUREZ.md).

## Estructura del repositorio

```text
.
├── .github/                 # CI, plantillas y propiedad de código
├── docs/                    # Documentación del producto, arquitectura y equipo
├── equipo/                  # Registro fuente de integrantes y responsabilidades
├── QA/                      # Casos de prueba y evidencias reales
├── src/Stoxx.Web/           # Aplicación ASP.NET Core
├── tests/Stoxx.Web.Tests/   # Pruebas automatizadas
├── Dockerfile               # Imagen para despliegue
└── render.yaml              # Configuración de Render
```

## Contribución

Lee [CONTRIBUTING.md](CONTRIBUTING.md) antes de proponer cambios. Los cambios se revisan mediante pull request; el flujo de integración continua compila y ejecuta la suite de pruebas para cambios dirigidos a `main`.

## Seguridad y licencia

Consulta [SECURITY.md](SECURITY.md) para reportar vulnerabilidades. La licencia del proyecto todavía debe ser seleccionada y aprobada por el equipo; revisa [LICENSE](LICENSE) antes de reutilizar o distribuir el código.
