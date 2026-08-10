# Tpo_DotNet_bb.Api — Documentación del proyecto

Resumen
-------
API REST desarrollada con .NET (ASP.NET Core) que expone recursos para clientes, productos y pedidos. Incluye autenticación JWT (token también enviado en cookie), acceso a base de datos MySQL/MariaDB mediante Entity Framework Core y documentación Swagger.

Proyectos incluidos
-------------------
- Tpo_DotNet_bb.Api: API principal (aspnetcore). Contiene controladores, DTOs, entidades y DbContext.

Requisitos
---------
- .NET 10 SDK (el proyecto apunta a .NET 10)
- MariaDB / MySQL accesible para la conexión de datos
- (Opcional) Docker para construir la imagen de la API

Estructura principal del repositorio
-----------------------------------
- Tpo_DotNet_bb.Api/
  - Api/Controllers/           -> Controladores WebAPI (endpoints)
  - Api/DTOs/                  -> Objetos de transferencia (entradas/validaciones)
  - Api/Entities/              -> Entidades EF Core y AppDbContext
  - appsettings.json           -> Configuración (Jwt, MySql)
  - Dockerfile                 -> Imagen Docker para la API
  - Program.cs                 -> Configuración de la app (CORS, DB, JWT, Swagger)

Configuración
-------------
La configuración principal se encuentra en Tpo_DotNet_bb.Api/appsettings.json. Claves relevantes:
- Jwt:Key  -> clave para firmar tokens JWT
- Jwt:Time -> tiempo de expiración en horas
- MySql:Connect -> cadena de conexión a la base de datos

Por seguridad en producción, no mantenga secretos en el repositorio. Use variables de entorno o Secret Manager. Las claves de configuración se pueden sobreescribir mediante variables de entorno (por ejemplo, prefixando con la ruta de configuración: `Jwt__Key`, `MySql__Connect`).

Cómo ejecutar (desarrollo)
--------------------------
1. Abrir la solución en Visual Studio o usar la CLI:
   - dotnet build
   - dotnet run --project Tpo_DotNet_bb.Api/Tpo_DotNet_bb.Api.csproj
2. Acceder a Swagger para explorar los endpoints: https://localhost:{PORT}/swagger (o la ruta que se indique en launchSettings.json)

Docker
------
El Dockerfile está en la raíz del proyecto Tpo_DotNet_bb.Api. Ejemplos:
- Construir: docker build -t tpo-dotnet-api .
- Ejecutar: docker run -p 8080:8080 tpo-dotnet-api

Nota: configure variables de entorno para la cadena de conexión y la clave JWT cuando ejecute el contenedor.

Seguridad y secretos
--------------------
- No mantenga credenciales en appsettings.json en el repositorio.
- Añada bin/ y obj/ a .gitignore y elimine artefactos generados del control de versiones.
- Consulte docs/SECURITY.md para instrucciones sobre variables de entorno, Secret Manager y cómo limpiar el historial de Git si las credenciales fueron comprometidas.
Seguridad y autenticación
-------------------------
- La API usa JWT (Bearer) y realiza validación de tokens. En el login de clientes el token se devuelve también en una cookie llamada `tpo_dotnet_bb` con HttpOnly y Secure.
- Algunos endpoints requieren autorización ([Authorize]) y obtienen el ID del cliente desde la reclamación `IDCLIENTE` en el token.

Controladores y endpoints principales
-----------------------------------
Listado resumido de controladores y rutas (métodos principales):

- api/clientes
  - GET /api/clientes                  -> listar clientes
  - GET /api/clientes/{id}             -> obtener cliente por id
  - POST /api/clientes/registrar       -> registrar nuevo cliente (dto ClienteRegistroDto)
  - POST /api/clientes/login           -> login (dto LoginDto) — devuelve token y cookie
  - PUT /api/clientes/{id}             -> actualizar cliente (ClienteUpdateDto)

- api/productos
  - GET /api/productos                 -> listar productos
  - GET /api/productos/{id}            -> obtener producto por id

- api/pedidos  (Requiere autorización)
  - GET /api/pedidos                   -> listar pedidos del cliente autenticado
  - GET /api/pedidos/{id}              -> obtener pedido (cliente autenticado)
  - POST /api/pedidos                  -> crear pedido (PedidoDto)
  - PUT /api/pedidos/{id}              -> actualizar pedido (PedidoDto)
  - DELETE /api/pedidos/{id}           -> eliminar pedido

- api/estado_pedidos
  - GET /api/estado_pedidos            -> listar estados
  - GET /api/estado_pedidos/{id}       -> obtener estado por id

- api/logs_procesos
  - GET /api/logs_procesos             -> listar logs
  - GET /api/logs_procesos/{id}        -> obtener log por id

- api/reporte01
  - GET /api/reporte01                 -> reporte / vista Vw_Productos

- api/subcategoria
  - GET /api/subcategoria              -> listar subcategorías
  - GET /api/subcategoria/{id}         -> obtener subcategoría por id

Modelos / DTOs importantes
--------------------------
- ClienteRegistroDto, ClienteUpdateDto, LoginDto
- PedidoDto
- Entidades EF: Clientes, Productos, Pedidos, Estado_Pedidos, Logs_Procesos, Categoria, Subcategoria, Vw_Productos, Vw_Pedidos

Base de datos
-------------
- AppDbContext está configurado para MariaDB/MySQL y contiene DbSet para las tablas y vistas usadas.
- El proyecto incluye un connection string de ejemplo en appsettings.json; por seguridad cámbielo por variables de entorno en entornos reales.

Detalles de implementación
-------------------------
- CORS: Política "AllowFront" configurada para permitir el front desplegado en https://tpo-nodejs-bf.vercel.app (ajustar según necesidad).
- JWT: el middleware lee el token también desde la cookie `tpo_dotnet_bb` (OnMessageReceived).
- Serialización JSON: se deshabilita la política de nombrado por defecto para mantener propiedad con mayúsculas (PropertyNamingPolicy = null).

Ejemplos de requests
--------------------
A continuación ejemplos con curl para operaciones típicas. Ajuste la URL y el puerto según su entorno.

1) Registro de cliente

curl -X POST https://localhost:5001/api/clientes/registrar \
  -H "Content-Type: application/json" \
  -d '{
    "Email": "usuario@example.com",
    "Apellido": "Perez",
    "Nombre": "Juan",
    "Direccion": "Calle Falsa 123",
    "Password": "P@ssw0rd!"
  }'

Respuesta esperada (200):
{
  "mensaje": "Cliente registrado correctamente"
}

2) Login (obtiene token y cookie)

curl -i -X POST https://localhost:5001/api/clientes/login \
  -H "Content-Type: application/json" \
  -d '{ "Email": "usuario@example.com", "Password": "P@ssw0rd!" }'

- En la respuesta se incluye el token en el body y la cookie `tpo_dotnet_bb` en el header Set-Cookie.

Respuesta esperada (200) body:
{
  "mensaje": "Login correcto",
  "token": "<JWT_TOKEN>"
}

3) Usar token Bearer en Authorization header (o cookie) para llamar endpoint protegido

curl -X GET https://localhost:5001/api/pedidos \
  -H "Authorization: Bearer <JWT_TOKEN>"

ó usando cookie (si el cliente y el servidor comparten dominio y políticas CORS permiten cookie):

curl -X GET https://localhost:5001/api/pedidos \
  -H "Cookie: tpo_dotnet_bb=<JWT_TOKEN>"

4) Crear un pedido (ejemplo)

curl -X POST https://localhost:5001/api/pedidos \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <JWT_TOKEN>" \
  -d '{
    "FECHA_COMPRA": "2026-08-10T12:00:00",
    "IDPRODUCTO": 1,
    "IDESTADO": 1,
    "CANTIDAD": 2,
    "PRECIO": 150.50,
    "IMPORTE": 301.00
  }'

Respuesta esperada (200):
{
  "mensaje": "Pedido creado correctamente"
}

Notas sobre CORS y cookies
-------------------------
- La API permite cookies para el front `https://tpo-nodejs-bf.vercel.app` tal como está configurada. Si su front local o dominio cambia, actualice la política CORS en Program.cs.
- Para que las cookies funcionen en requests cross-site, asegúrese de usar Secure, SameSite=None y habilitar credenciales en el cliente (fetch/axios con credentials: 'include').

Buenas prácticas recomendadas
----------------------------
- No exponer secrets en el repositorio (mover valores sensibles a variables de entorno o gestor de secretos).
- Activar HTTPS y revisar configuración de cookies y SameSite según el dominio y entorno.
- Validar y sanitizar entradas adicionales si se exponen a usuarios externos.

Contribuir
----------
1. Hacer fork del repositorio
2. Crear una rama feature/bugfix
3. Abrir un Pull Request describiendo cambios

Contacto
--------
Para dudas sobre el código, abrir un issue en el repositorio.
