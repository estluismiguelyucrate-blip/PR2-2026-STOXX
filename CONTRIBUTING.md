# Guía para contribuir

## Antes de comenzar

1. Revisa el alcance vigente en `docs/proyecto/ALCANCE_OBJETIVOS_CRITERIOS.md` y la documentación técnica en `docs/`.
2. Busca una tarea o incidencia existente en GitHub. Si el equipo usa otro mecanismo para organizar el trabajo, registra allí la tarea antes de empezar.
3. Confirma con el equipo cualquier cambio que altere el alcance, la arquitectura, el modelo de datos o las responsabilidades.

## Cambios y commits

- Mantén cada cambio enfocado y coherente con la arquitectura documentada.
- Usa mensajes de commit descriptivos, en imperativo o estilo convencional, por ejemplo: `docs: documenta el modelo de datos`.
- No incluyas credenciales, datos reales, bases de datos locales ni artefactos temporales.
- Actualiza la documentación y las pruebas afectadas junto con el cambio de comportamiento.

## Revisión

1. Ejecuta `dotnet test tests/Stoxx.Web.Tests/Stoxx.Web.Tests.csproj --configuration Release` para cambios de código.
2. Comprueba los enlaces Markdown y las instrucciones que hayas modificado.
3. Abre un pull request hacia `main` con propósito, resumen, validación realizada y capturas cuando cambie la interfaz.
4. Espera la revisión de otro integrante y el resultado del flujo de GitHub Actions antes de integrar.

## Datos de prueba

Usa datos ficticios. Nunca incluyas contraseñas, tokens, datos personales o información financiera real en el repositorio, los issues, los commits o las capturas.
