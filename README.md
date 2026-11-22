# STOXX

Aplicación web académica para registrar ventas, controlar inventario y consultar cierres de caja de pequeños comercios.

## Estado

- **Proyecto:** PR2-2026-STOXX.
- **Tipo:** MVP académico en desarrollo y validación.
- **Servidor de destino:** Google Cloud Run para ASP.NET Core con Neon PostgreSQL para conservar los datos fuera del contenedor.
- **URL pública del nuevo servidor:** pendiente de publicación. El código y las instrucciones están preparados, pero todavía falta configurar el proyecto de Google Cloud, la base PostgreSQL y sus secretos; no hay una dirección nueva que se pueda compartir aún.
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
- Persistencia local con SQLite y PostgreSQL administrado en el despliegue de Cloud Run.

## Tecnologías

- C# y ASP.NET Core 10 con componentes Blazor Server.
- .NET SDK 10 para desarrollo y pruebas.
- SQLite y PostgreSQL mediante Entity Framework Core.
- Docker para desplegar el servidor .NET en Google Cloud Run.

La ejecución local sigue usando SQLite. En Cloud Run, `DATABASE_URL` selecciona PostgreSQL y las claves de sesión también se guardan en esa base persistente.

## Ejecución local

1. Instala el [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
2. Clona el repositorio y abre una terminal en su raíz.
3. Ejecuta:

   ```powershell
   dotnet run --project src/Stoxx.Web/Stoxx.Web.csproj --urls http://localhost:5080
   ```

4. Abre [http://localhost:5080](http://localhost:5080). En una base vacía, completa el flujo de configuración inicial de la cuenta administradora.

Por defecto, la base local se crea en `src/Stoxx.Web/data/`. Configura `STOXX_DATA_DIR` para usar otra carpeta. No agregues contraseñas, tokens, cadenas de conexión ni datos reales al repositorio.

## Preparar Google Cloud Run

Cloud Run admite aplicaciones .NET en contenedores. El `Dockerfile` ya escucha en el puerto 8080. La instancia escala a cero cuando está inactiva, así que la primera petición después de un periodo sin uso puede tardar un poco; los registros siguen en PostgreSQL y no se pierden cuando se reemplaza el contenedor.

1. Crea un proyecto de Google Cloud, habilita facturación y crea una base PostgreSQL en Neon.
2. En Secret Manager, guarda la URL de conexión como `stoxx-database-url`. Concede acceso de lectura a la identidad de servicio que usará Cloud Run.
3. Si vas a conservar los registros de SQLite, exporta de forma segura el archivo original y ejecuta la utilidad de migración C# con `STOXX_SQLITE_PATH` y `DATABASE_URL` configuradas. La utilidad se detiene si la base de destino ya tiene datos.
4. Desde la raíz del repositorio, despliega:

   ```powershell
   gcloud run deploy stoxx --source . --region us-central1 --allow-unauthenticated --port 8080 --timeout 3600 --max 1 --memory 1Gi --set-env-vars STOXX_REQUIRE_POSTGRES=true --set-secrets DATABASE_URL=stoxx-database-url:latest
   ```

5. Abre la URL que entrega Google Cloud, verifica `/api/health`, crea o usa la cuenta administradora y confirma que los datos continúan después de un nuevo despliegue.

Cloud Run tiene una cuota gratuita mensual, pero requiere una cuenta de facturación y puede cobrar si se superan los límites o se mantiene una instancia caliente. Para reducir la espera al primer acceso se puede configurar una instancia mínima; esa instancia tiene costo incluso sin tráfico. Neon también aplica límites gratuitos de almacenamiento y cómputo. Revisa los consumos y configura alertas de presupuesto antes de uso real.

### Migrar los datos existentes

La utilidad `tools/Stoxx.DataMigration` copia usuarios y hashes de contraseñas, productos, proveedores, cajas, ventas, detalle de ventas y compras de SQLite a una base PostgreSQL vacía. Las claves de sesión se generan de nuevo; los usuarios podrán volver a iniciar sesión con sus contraseñas actuales.

```powershell
$env:STOXX_SQLITE_PATH = (Resolve-Path "src/Stoxx.Web/data/stoxx.db").Path
$env:DATABASE_URL = Read-Host "Pega la conexión de PostgreSQL"
dotnet run --project tools/Stoxx.DataMigration/Stoxx.DataMigration.csproj
Remove-Item Env:DATABASE_URL
Remove-Item Env:STOXX_SQLITE_PATH
```

Si la base está en el disco de Render, primero obtén una copia segura de ese archivo. Conserva Render y la copia de SQLite hasta validar conteos, inicio de sesión, ventas y reportes en Cloud Run. No subas la copia a Git.

### Pruebas

Ejecuta la suite de pruebas automatizadas desde la raíz:

```powershell
dotnet test tests/Stoxx.Web.Tests/Stoxx.Web.Tests.csproj --configuration Release
```

La matriz de [pruebas manuales](QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md) indica cómo registrar fecha, entorno, responsable, versión y evidencia. No marques como aprobada una prueba que no se haya ejecutado.

## Documentación

- [Índice de documentación](docs/README.md): guía de ubicación de alcance, requisitos, arquitectura, datos, planificación, diagramas, manuales y equipo.
- [Arquitectura](docs/arquitectura.md), [modelo de datos](docs/modelo-de-datos.md) y [manual de ejecución local](docs/manuales/instalacion-y-ejecucion.md).
- [Guía para exponer STOXX](docs/guia-exposicion.md): resumen del proyecto, recorrido por el código, ejecución local y guion de presentación.
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
