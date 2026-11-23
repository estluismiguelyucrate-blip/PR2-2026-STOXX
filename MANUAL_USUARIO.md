# Manual de usuario de STOXX

## 1. Presentación

STOXX es una aplicación web para apoyar las tareas de ventas e inventario de una tienda. Permite registrar ventas, controlar la caja, mantener un catálogo de productos, registrar compras y consultar reportes.

La aplicación solicita iniciar sesión. Si todavía no se ha creado ninguna cuenta, muestra la opción para configurar la primera cuenta administradora.

## 2. Objetivo del sistema

El objetivo de STOXX es reunir en un mismo lugar el registro de ventas, las existencias de productos, las compras recibidas, el estado de la caja y reportes diarios. Los importes se muestran en bolivianos (Bs).

## 3. Acceso y roles

### Primera cuenta

Cuando el sistema indique **Configura el acceso**, completa el nombre, el usuario y la contraseña y selecciona **Crear administrador**. Esta configuración inicial solo se ofrece mientras no exista una cuenta. Después, inicia sesión con el usuario y la contraseña creados.

Si ya hay cuentas, escribe tu usuario y contraseña y selecciona **Entrar**. Para salir de la cuenta, selecciona **Salir** en la parte superior de la aplicación.

### Opciones según el rol

La navegación muestra las opciones disponibles para el rol de la cuenta:

| Rol | Opciones principales |
| --- | --- |
| Administrador | Resumen, Registrar venta, Productos, Compras y proveedores, Reportes y Usuarios. |
| Cajero | Resumen, Registrar venta y Reportes. |
| Inventario | Resumen, Productos y Compras y proveedores. |

Si no ves una opción en el menú, puede que no esté disponible para tu rol. Solicita al administrador que revise tu cuenta si necesitas acceso.

## 4. Pantallas y cómo utilizarlas

### Resumen del día

Esta pantalla presenta la actividad del día:

- **Ventas de hoy:** importe y cantidad de ventas registradas durante el día.
- **Ganancia bruta estimada:** ventas menos el costo registrado de los productos vendidos.
- **Productos activos:** cantidad de productos activos del catálogo.
- **Stock bajo:** productos que están en el nivel mínimo o por debajo de él.
- **Caja:** muestra si está abierta o cerrada.
- **Revisar stock:** muestra alertas de inventario cuando las hay.
- **Ventas recientes:** lista ventas recientes, hora, persona que atendió, forma de pago y total.

Selecciona **Actualizar** para volver a cargar el resumen.

Desde el panel **Caja**, los usuarios con rol Administrador o Cajero pueden:

1. Con la caja cerrada, escribir el **Monto inicial (Bs)** y seleccionar **Abrir caja**.
2. Con la caja abierta, escribir el **Dinero contado (Bs)** y seleccionar **Cerrar caja**. El sistema muestra la diferencia entre el efectivo contado y el esperado.

### Registrar venta

1. Abre **Registrar venta** en el menú.
2. Busca un producto por nombre o código en el campo de búsqueda.
3. Selecciona **Agregar** junto a cada producto que quieras vender. La lista muestra su precio y stock disponible.
4. En **Detalle de venta**, usa **+** o **−** para cambiar la cantidad. Al quitar la última unidad, el producto sale del detalle.
5. Revisa el total y selecciona la forma de pago: **Efectivo** o **QR**.
6. Selecciona **Confirmar venta**.

Para confirmar una venta, la caja debe estar abierta y el detalle debe contener al menos un producto. La cantidad no puede superar el stock disponible. Al confirmarse, el sistema descuenta las unidades vendidas del inventario y muestra un aviso con el número y el total de la venta.

### Productos e inventario

En **Productos**, la tabla permite buscar productos por código, nombre o categoría y revisar precio, stock y estado.

Para registrar un producto, completa **Código**, **Nombre**, **Categoría** (opcional), **Costo (Bs)**, **Precio venta (Bs)**, **Stock** y **Stock mínimo**; después selecciona **Guardar producto**.

Para cambiar un producto, selecciona **Editar** en su fila, modifica los campos del formulario y selecciona **Guardar producto**. **Cancelar** descarta la edición del formulario.

**Desactivar** cambia el estado del producto para que deje de estar activo. La interfaz no presenta una opción para reactivarlo.

### Compras y proveedores

En **Compras y proveedores** hay tres áreas:

1. **Agregar proveedor:** completa el nombre y, si corresponde, el contacto, teléfono, correo y notas. Selecciona **Guardar proveedor**.
2. **Registrar compra recibida:** selecciona un proveedor y un producto, escribe la cantidad y el costo unitario en Bs y selecciona **Guardar compra y actualizar stock**. La compra registrada aumenta el stock del producto.
3. **Compras recientes:** consulta fecha, proveedor, producto, unidades y costo total.

En **Proveedores registrados**, **Desactivar** está disponible para los proveedores activos. La interfaz no muestra opciones para editar o reactivar proveedores.

### Reportes

En **Reportes**, selecciona una fecha en el campo **Fecha** para consultar ese día. Se muestran las ventas totales y su cantidad, los totales por **Efectivo** y **QR**, la ganancia bruta y el costo de los productos vendidos. También aparecen el detalle de ventas y los productos más vendidos.

### Usuarios

Esta opción solo está disponible para Administradores. Completa el nombre, el usuario y la contraseña; luego elige un rol (**Cajero**, **Inventario** o **Administrador**) y selecciona **Crear cuenta**. Las cuentas registradas aparecen en la tabla.

La pantalla revisada no muestra opciones para editar o eliminar cuentas de usuario.

## 5. Botones y opciones

| Botón u opción | Para qué sirve |
| --- | --- |
| **Entrar** | Inicia sesión con la cuenta indicada. |
| **Crear administrador** | Registra la primera cuenta cuando el sistema aún no tiene usuarios. |
| **Salir** | Cierra la sesión. |
| **Resumen** | Abre los indicadores del día y el panel de caja. |
| **Actualizar** | Recarga los datos del resumen. |
| **Abrir caja** / **Cerrar caja** | Inicia o cierra la caja con los importes indicados. |
| **Registrar venta** | Abre el catálogo y el detalle para preparar una venta. |
| **Agregar**, **+**, **−** | Añade productos o modifica su cantidad en una venta. |
| **Confirmar venta** | Registra la venta y descuenta el stock correspondiente. |
| **Guardar producto**, **Editar**, **Cancelar**, **Desactivar** | Registra, modifica, cancela la edición o desactiva un producto. |
| **Guardar proveedor**, **Desactivar** | Registra o desactiva un proveedor. |
| **Guardar compra y actualizar stock** | Registra la compra recibida y aumenta el stock. |
| **Crear cuenta** | Registra una cuenta con el rol seleccionado. |

Los avisos que aparecen en la parte superior informan si una operación se completó o si ocurrió un error. Lee el mensaje antes de volver a enviar un formulario.

## 6. Recomendaciones para usuarios

- Revisa el producto, el precio, la cantidad y el total antes de confirmar una venta.
- Comprueba que la caja esté abierta antes de registrar ventas.
- Mantén actualizado el costo, el precio y el stock de cada producto.
- Registra las compras recibidas para que el inventario refleje las unidades ingresadas.
- Ingresa con tu propia cuenta, no compartas tu contraseña y selecciona **Salir** al terminar.
- Si una opción no aparece o recibes un aviso de error, contacta al administrador del sistema.

## 7. Conclusión

STOXX reúne las operaciones principales de venta, caja e inventario y permite consultar su actividad diaria. Las opciones disponibles dependen del rol asignado. Este manual describe las funciones visibles en la aplicación revisada; no presupone funciones adicionales, como exportar reportes o recuperar cuentas desactivadas, que no aparecen en sus pantallas.
