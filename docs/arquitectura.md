# Arquitectura actual

Este documento describe los componentes presentes en `src/Stoxx.Web`. Las funciones no implementadas o no verificadas no se presentan como terminadas.

## Componentes

- **Interfaz:** Blazor Server y recursos estáticos en `src/Stoxx.Web/Components` y `wwwroot`.
- **Aplicación y API:** ASP.NET Core configura autenticación por cookie, autorización, rutas HTTP y manejo de errores en `Program.cs`.
- **Persistencia local:** Entity Framework Core con SQLite y `AppDbContext`.
- **Persistencia en Google Cloud:** Entity Framework Core con PostgreSQL; `DATABASE_URL` selecciona el proveedor en el contenedor.
- **Pruebas:** proyecto xUnit con `Microsoft.AspNetCore.Mvc.Testing` en `tests/Stoxx.Web.Tests`.
- **Integración continua:** `.github/workflows/dotnet-ci.yml` restaura, compila y ejecuta pruebas en GitHub Actions.
- **Despliegue actual:** `render.yaml` conserva el servicio Render y su disco persistente en `/var/data`.
- **Destino preparado:** Cloud Run construye el contenedor desde `Dockerfile`, escucha en el puerto 8080 y lee `DATABASE_URL` desde Secret Manager. El sistema de archivos del contenedor no aloja datos persistentes.

## Recorrido de una solicitud

1. El navegador carga la interfaz y envía solicitudes a los endpoints `/api/*`.
2. ASP.NET Core valida autenticación y autorización en las rutas protegidas.
3. Los manejadores de rutas validan la operación y usan `AppDbContext` para consultar o guardar datos.
4. Entity Framework Core persiste los cambios en SQLite durante el desarrollo o en PostgreSQL en Cloud Run.

## Decisiones y límites documentados

- `STOXX_DATA_DIR` define dónde guardar la base `stoxx.db` y sus claves al usar SQLite. En local, por defecto, se usa `data/` dentro del directorio de contenido.
- En Render, el volumen de `/var/data` debe mantenerse asociado al servicio para conservar base y claves entre reinicios.
- En Cloud Run, se exige `STOXX_REQUIRE_POSTGRES=true`; así el servidor falla al arrancar si falta `DATABASE_URL`, en vez de crear una base SQLite efímera. PostgreSQL guarda también las claves de protección de datos.
- `tools/Stoxx.DataMigration` importa en C# los datos de SQLite a una base PostgreSQL vacía y reajusta las secuencias de IDs.
- La autenticación usa cookies de sesión y hashes de contraseña. Los nombres de roles son `Admin`, `Cajero` e `Inventario`.
- El modelo actual usa `EnsureCreated`; no se observan migraciones de Entity Framework en el repositorio.
- El registro de ventas por QR no se integra con un banco ni confirma pagos externos.

Para el esquema persistido, consulta el [modelo de datos](modelo-de-datos.md) y su [diagrama](diagramas/modelo-de-datos.md). Para iniciar el sistema, sigue el [manual](manuales/instalacion-y-ejecucion.md).
