# E-08 — Ejecución local de pruebas automatizadas

- **Fecha:** 22 de noviembre de 2026.
- **Commit probado:** `080198d`.
- **Entorno:** Windows, SDK .NET 10.0.401.
- **Comando:** `dotnet test tests/Stoxx.Web.Tests/Stoxx.Web.Tests.csproj --configuration Release -p:OutputPath=tmp/test-output/`.
- **Resultado:** 5 aprobadas, 0 fallidas, 0 omitidas. Código de salida 0.
- **Publicación local:** `dotnet publish` en Release terminó correctamente.

Esta evidencia corresponde únicamente a la suite automatizada existente. No acredita las 12 pruebas manuales pendientes ni el despliegue público en Cloud Run y PostgreSQL.
