# Tpo_DotNet_bb.Api — Documentación actualizada

Resumen 
-------
Backend REST en ASP.NET Core (.NET 10) para gestión de clientes, productos y pedidos. Incluye autenticación JWT (token también enviado en cookie), acceso a MariaDB/MySQL mediante Entity Framework Core y documentación Swagger (habilitada solo en Development).

Cambios importantes realizados
------------------------------
- Eliminada la cadena de conexión hard-coded del código fuente. AppDbContext ahora se configura exclusivamente vía DI (DbContextOptions) en Program.cs.
- Eliminado DbContext duplicado y consolidados los namespaces a `Tpo_DotNet_bb.Api.*`.
- Añadido `.gitignore` para evitar commitear bin/ y obj/ y eliminados artefactos con secrets del árbol de trabajo.
- Añadido `docs/SECURITY.md` con instrucciones para variables de entorno, user-secrets y limpieza del historial de Git.
- Estandarizado el uso de BCrypt para hashing de contraseñas.

Requisitos
---------
- .NET 10 SDK
- MariaDB / MySQL (accesible desde el entorno donde se ejecuta la API)
- Docker (opcional)

Estructura principal (resumida)
--------------------------------
- Api/Controllers/      -> Controladores WebAPI
- Api/DTOs/             -> DTOs (namespace Tpo_DotNet_bb.Api.DTOs)
- Api/Entities/         -> Entidades EF Core y AppDbContext (namespace Tpo_DotNet_bb.Api.Entities)
- Program.cs            -> Configuración de la app (CORS, DB, JWT, Swagger)
- appsettings.json      -> Valores no sensibles (placeholders)
- docs/SECURITY.md      -> Guía de manejo de secretos

Configuración de conexión (importante)
-----------------------------------
La cadena de conexión se lee desde la configuración en la clave `MySql:Connect`. No dejes credenciales en appsettings.json en el repositorio.

Ejemplos para desarrollo (PowerShell):

    $env:MySql__Connect = "server=MI_HOST;port=3306;database=MI_DB;user=MI_USER;password=MI_PASS"
    $env:Jwt__Key = "clave_secreta_larga"
    dotnet run --project Tpo_DotNet_bb.Api/Tpo_DotNet_bb.Api.csproj

Con dotnet user-secrets (local):

    dotnet user-secrets init
    dotnet user-secrets set "MySql:Connect" "server=...;database=...;user=...;password=..."
    dotnet user-secrets set "Jwt:Key" "valor"

Verificación de conectividad antes de ejecutar
----------------------------------------------
- PowerShell: Test-NetConnection -ComputerName MI_HOST -Port 3306
- Linux: nc -vz MI_HOST 3306

Instrucciones rápidas para limpiar artefactos y commits con secretos
------------------------------------------------------------------
1) Añadir .gitignore (ya añadido) y eliminar artefactos del índice:

    git rm --cached -r Tpo_DotNet_bb.Api/bin Tpo_DotNet_bb.Api/obj
    git commit -m "Remove build artifacts containing secrets and add .gitignore"

2) Si las credenciales fueron comiteadas en el historial: rotar las credenciales y usar BFG/git filter-repo (ver docs/SECURITY.md). Esto reescribe historial y requiere coordinar con colaboradores.

Seguridad y buenas prácticas
----------------------------
- No subir secretos al repositorio.
- Rotar claves si fueron comprometidas.
- Proteger Swagger en producción (actualmente solo se expone en Development).
- Establecer issuer/audience para JWT en entornos productivos.

Notas de implementación relevantes
---------------------------------
- DbContext: AppDbContext se configura en Program.cs con builder.Configuration["MySql:Connect"].
- Namespaces: consolidados a Tpo_DotNet_bb.Api.Controllers, .DTOs, .Entities y .Services.
- Hashing: BCrypt se usa para registrar y verificar contraseñas.
- Swagger: solo activo en Development.

Endpoints principales
---------------------
- /api/clientes  (registrar, login, CRUD)
- /api/productos (listado, detalle)
- /api/pedidos   (CRUD, requiere autorización JWT)
- /api/estado_pedidos, /api/logs_procesos, /api/subcategoria, /api/reporte01

Ejecutar y comprobar
---------------------
1) Definir variables de entorno (MySql__Connect, Jwt__Key).
2) dotnet build
3) dotnet run --project Tpo_DotNet_bb.Api/Tpo_DotNet_bb.Api.csproj
4) Acceder a Swagger en Development: https://localhost:{PORT}/swagger

Más información
----------------
Consulta docs/SECURITY.md para pasos detallados sobre manejo de secretos y limpieza del historial de Git.

Contacto
--------
Para dudas o problemas, abrir un issue en el repositorio.
