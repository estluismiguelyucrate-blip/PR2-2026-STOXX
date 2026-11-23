# Manual de instalación y ejecución local

## Requisitos

- Windows, macOS o Linux.
- .NET SDK 10 instalado.
- Acceso a Internet durante la primera restauración de paquetes NuGet.

SQLite se incorpora mediante el paquete del proyecto; no hace falta instalar un servidor de base de datos.

Los días del resumen y de los reportes se calculan con la hora de Bolivia (UTC−4), sin depender de la zona horaria configurada en el servidor.

## Iniciar la aplicación

Desde la raíz del repositorio:

```powershell
dotnet restore src/Stoxx.Web/Stoxx.Web.csproj
dotnet run --project src/Stoxx.Web/Stoxx.Web.csproj --urls http://localhost:5080
```

Abre `http://localhost:5080`. En una base nueva, la pantalla permite configurar el primer administrador. Completa este paso solo en un entorno que controles, con una contraseña de prueba segura; no guardes la contraseña en el repositorio ni en capturas.

## Datos locales

Por defecto, la aplicación crea `src/Stoxx.Web/data/stoxx.db` y `src/Stoxx.Web/data/keys/`. Para cambiar la ubicación, establece `STOXX_DATA_DIR` en la sesión antes de iniciar el proceso. Trata la carpeta como información privada y no la subas a Git.

## Ejecutar las pruebas

```powershell
dotnet test tests/Stoxx.Web.Tests/Stoxx.Web.Tests.csproj --configuration Release
```

Las pruebas manuales de operaciones se describen en la [matriz de QA](../../QA/casos-prueba/MATRIZ_PRUEBAS_MVP.md). Registra el resultado real y guarda evidencia anonimizada de acuerdo con las [reglas de evidencias](../../QA/evidencias/README.md).

## Despliegue

La raíz incluye `Dockerfile` y `render.yaml` para Render. El servicio requiere que la configuración de disco de Render conserve la ruta `STOXX_DATA_DIR=/var/data`; verifica la disponibilidad y persistencia de producción por separado. No realices la configuración inicial con credenciales reales en un entorno compartido para una demostración.
