# Contribuir a STOXX

El repositorio es un proyecto académico en equipo. Antes de empezar, revisa el [README](README.md), el [alcance](docs/proyecto/ALCANCE_OBJETIVOS_CRITERIOS.md) y la documentación de [arquitectura](docs/arquitectura.md).

## Flujo de trabajo

1. Comprueba Issues y pull requests abiertos para evitar trabajo duplicado. Si el equipo usa otra herramienta de seguimiento, registra allí el objetivo y su responsable.
2. Crea una rama breve y descriptiva desde `main`, por ejemplo `feat/alerta-stock` o `docs/guia-local`.
3. Mantén cada cambio enfocado y respeta la arquitectura y los requisitos documentados.
4. Ejecuta las comprobaciones adecuadas al cambio. Para cambios de código, ejecuta `dotnet test tests/Stoxx.Web.Tests/Stoxx.Web.Tests.csproj --configuration Release`.
5. Actualiza la documentación y las evidencias cuando cambien los pasos, el comportamiento o los requisitos.
6. Abre un pull request a `main` con propósito, resumen, verificación realizada y capturas anonimizadas cuando haya cambios de interfaz.

## Commits y revisión

Usa mensajes descriptivos que expliquen el cambio, por ejemplo `docs: aclarar ejecucion local` o `fix: validar stock disponible`. En la revisión, confirma que los enlaces relativos funcionen, que no se hayan incluido archivos locales o datos sensibles y que el flujo de CI termine correctamente.

No incluyas secretos, credenciales, bases de datos con datos reales, archivos temporales ni resultados de pruebas inventados. Si una prueba no se pudo ejecutar, indícalo en el pull request.
