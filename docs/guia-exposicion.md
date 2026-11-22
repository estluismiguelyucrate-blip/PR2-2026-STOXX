# Guía para exponer STOXX

Esta guía sirve para explicar el proyecto, recorrer el código y hacer una demostración breve. La aplicación principal es un sistema web de ventas e inventario para un comercio pequeño.

## Presentación breve

> STOXX es una aplicación web académica para registrar ventas, controlar productos e inventario, administrar compras y proveedores, y consultar reportes. Está desarrollada principalmente con C# en .NET y Blazor Server. La interfaz se escribe con componentes Razor, que combinan HTML con C#. En desarrollo se usa SQLite; el proyecto también está preparado para conectarse a PostgreSQL en un despliegue en la nube.

## Qué puede hacer el sistema

- Iniciar sesión y crear la primera cuenta administradora cuando la base está vacía.
- Consultar el resumen y los datos de caja.
- Registrar ventas y actualizar existencias.
- Crear y editar productos, precios y stock mínimo.
- Registrar proveedores y compras.
- Consultar reportes por fecha.
- Administrar usuarios según sus permisos: administrador, cajero e inventario.

Para exponerlo, usa datos inventados y evita mostrar contraseñas o información real de clientes.

## Lenguajes y tecnologías

- **C# es el lenguaje principal.** Contiene el servidor, las reglas de negocio, la autenticación, los endpoints, el acceso a datos y la herramienta C# de migración.
- **Razor/HTML presenta la interfaz.** Los archivos `.razor` contienen etiquetas HTML junto con expresiones y eventos de C#.
- **CSS da formato visual** en `src/Stoxx.Web/wwwroot/app.css`.
- **Hay un archivo JavaScript pequeño** en `src/Stoxx.Web/wwwroot/app.js`: proporciona a Blazor una función `fetch` para llamar los endpoints HTTP. La lógica comercial no está implementada en JavaScript.

Por eso, la forma precisa de describirlo es **“aplicación principalmente en C# con interfaz Blazor/Razor y HTML”**. No conviene afirmar que el proyecto entero contiene literalmente solo C# y HTML, porque también incluye estilos CSS y esa pequeña función JavaScript de comunicación.

El repositorio privado `pruebas` es distinto: contiene únicamente una maqueta estática en HTML/SVG para revisar el diseño; no contiene el código de la aplicación.

## Recorrido por el código

| Archivo o carpeta | Qué explicar |
| --- | --- |
| `src/Stoxx.Web/Program.cs` | Arranque de ASP.NET Core, configuración de SQLite/PostgreSQL, autenticación y rutas `/api`. |
| `src/Stoxx.Web/Components/Pages/Home.razor` | Estructura HTML/Razor de las pantallas de inicio, ventas, productos, compras, reportes y usuarios. |
| `src/Stoxx.Web/Components/Pages/Home.razor.cs` | Código C# asociado a la pantalla: carga datos, envía solicitudes y valida acciones de la interfaz. |
| `src/Stoxx.Web/Data/AppDbContext.cs` | Contexto de Entity Framework Core y tablas que se guardan en la base de datos. |
| `src/Stoxx.Web/wwwroot/app.css` | Colores, distribución y adaptación a pantallas pequeñas. |
| `src/Stoxx.Web/wwwroot/app.js` | Puente pequeño para que la interfaz invoque los endpoints HTTP. |
| `tools/Stoxx.DataMigration/Program.cs` | Utilidad C# para copiar registros de SQLite a PostgreSQL cuando se configure la migración. |
| `tests/Stoxx.Web.Tests/` | Proyecto de pruebas automatizadas. Las pruebas deben ejecutarse antes de afirmar que están aprobadas. |
| `Dockerfile` y `.github/workflows/dotnet-ci.yml` | Empaquetado de la aplicación y compilación automática en GitHub Actions. |

Una forma sencilla de explicar el recorrido es: **la pantalla Razor llama al servidor, el servidor valida permisos y datos, y Entity Framework guarda o consulta la base de datos**.

## Cómo abrir y ejecutar el código

1. Instala el SDK de .NET 10 y abre la carpeta del repositorio en Visual Studio.
2. Si Visual Studio no detecta una solución, abre `src/Stoxx.Web/Stoxx.Web.csproj` directamente.
3. También puedes abrir una terminal en la raíz del proyecto y ejecutar:

   ```powershell
   dotnet restore src/Stoxx.Web/Stoxx.Web.csproj
   dotnet run --project src/Stoxx.Web/Stoxx.Web.csproj
   ```

4. Abre `http://localhost:5080` en el navegador. Ese puerto está definido en `src/Stoxx.Web/Properties/launchSettings.json`.
5. Si la base local no tiene usuarios, la pantalla pedirá crear la primera cuenta administradora. Para detener el servidor, vuelve a la terminal y pulsa `Ctrl+C`.

En Visual Studio, muestra el **Explorador de soluciones** y abre primero `Program.cs`, después `Home.razor` y `Home.razor.cs`, y por último `AppDbContext.cs`. Así se ve el flujo desde el servidor hasta la interfaz y la persistencia.

## Cómo presentar la vista previa y el código

- El repositorio privado [`pruebas`](https://github.com/estluismiguelyucrate-blip/pruebas) muestra la maqueta visual en su README. Sirve para revisar el diseño, no para demostrar el funcionamiento del servidor.
- Para mostrar el código real, abre el repositorio principal y navega a `src/Stoxx.Web/`.
- Durante la exposición, alterna entre el navegador con la aplicación local y el editor con los archivos C# y Razor.
- Si necesitas proyectar, aumenta el tamaño del texto del editor y usa solo datos ficticios.

## Servidor y persistencia

La configuración local usa SQLite. El código también permite usar PostgreSQL mediante `DATABASE_URL` y exige esa variable en Cloud Run cuando `STOXX_REQUIRE_POSTGRES=true`. El contenedor ya está preparado para Cloud Run y la documentación del repositorio explica el despliegue con una base PostgreSQL externa.

La publicación real todavía requiere un proyecto de Google Cloud, configurar su facturación, crear la base PostgreSQL y guardar su conexión como secreto. Si se van a conservar datos existentes, primero hay que obtener una copia segura de SQLite y ejecutar la herramienta C# de migración contra una base PostgreSQL vacía. No se deben subir archivos `.db`, contraseñas ni cadenas de conexión al repositorio.

## Guion de exposición de 2 minutos

1. **Problema:** “Un comercio pequeño necesita registrar ventas y saber qué productos quedan en stock.”
2. **Solución:** “STOXX reúne ventas, inventario, compras y reportes en una aplicación web.”
3. **Tecnología:** “El servidor y la lógica están principalmente en C# con ASP.NET Core y Blazor Server; las vistas usan Razor/HTML.”
4. **Demostración:** “Voy a mostrar el inicio de sesión, el resumen, el registro de una venta y la actualización del inventario con datos ficticios.”
5. **Código:** “En `Program.cs` se configura el servidor; `Home.razor` define la interfaz; `Home.razor.cs` contiene las acciones de la pantalla; y `AppDbContext.cs` conecta la aplicación con los datos.”
6. **Cierre:** “La versión local usa SQLite y el proyecto tiene una ruta preparada para PostgreSQL y Cloud Run. La publicación y migración de datos requieren configurar las cuentas y secretos correspondientes.”

## ¿Es difícil?

Para una exposición básica, la dificultad es **moderada**: basta con explicar el flujo de pantalla, servidor y base de datos, y mostrar dos o tres funciones. Aprender el código completo lleva más tiempo porque hay autenticación, permisos, ventas y persistencia. La vista previa `pruebas` es solo visual; no la presentes como si fuera la aplicación funcionando.
