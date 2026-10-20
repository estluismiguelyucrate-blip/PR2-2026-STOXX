# E-06 — Acceso público a Render

**Fecha:** 20-10-2026. **Entorno:** servicio público Render. **URL:** https://stocks-con-3x.onrender.com/.

## Resultado observado

Después del arranque en frío del servicio, la página cargó y presentó la pantalla **“Configura el acceso”** para crear la primera cuenta administradora. La consulta de solo lectura a `/api/auth/status` respondió `setupRequired: true`, `authenticated: false` y `user: null`.

No se envió el formulario ni se creó una cuenta en producción. Por tanto, queda pendiente que el administrador designado realice y documente el alta inicial. Mientras `setupRequired` sea verdadero, la configuración inicial sigue disponible públicamente para quien visite la página; el equipo debe completar el alta antes de compartir el sitio como sistema operativo y comprobar después que la ruta de configuración quede cerrada.

Este resultado acredita que la URL pública responde desde el navegador, pero no acredita autenticación, uso funcional ni persistencia de datos en Render. La revisión fue visual y mediante la respuesta de estado; no se probaron operaciones ni se accedió a datos de producción.
