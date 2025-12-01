# To-Do List - Aplicación Web ASP.NET

✅ **Proyecto migrado exitosamente de WindowsForms a ASP.NET Web Forms**

## 📋 Descripción

Aplicación web para gestión de tareas (To-Do List) desarrollada en ASP.NET Web Forms con .NET Framework 4.7.2 y MySQL.

### Características

- ✨ **Operaciones CRUD completas**: Agregar, Modificar, Eliminar tareas
- 🔍 **Filtros de consulta**: Ver todas las tareas, solo completadas, o solo pendientes
- 🎨 **Diseño moderno**: Interfaz responsive con Bootstrap 5
- 🗄️ **Base de datos MySQL**: Persistencia de datos en MySQL

## 🚀 Cómo ejecutar el proyecto

### Requisitos previos

1. **Visual Studio 2022** (Community, Professional o Enterprise)
2. **MySQL Server** instalado y corriendo
3. **Base de datos configurada** (ver sección de configuración de BD)

### Paso 1: Configurar Base de Datos

Ejecuta el script SQL incluido para crear la base de datos y la tabla:

```bash
mysql -u root -p < database_setup.sql
```

O abre `database_setup.sql` en MySQL Workbench y ejecútalo.

### Paso 2: Configurar conexión (opcional)

Si tus credenciales de MySQL son diferentes, edita `Web.config`:

```xml
<connectionStrings>
  <add name="MySqlConnection" 
       connectionString="Server=localhost;Database=todolist;User Id=root;Password=TU_PASSWORD;" 
       providerName="MySql.Data.MySqlClient"/>
</connectionStrings>
```

### Paso 3: Abrir en Visual Studio 2022

1. Abre Visual Studio 2022
2. Selecciona **Archivo** → **Abrir** → **Proyecto/Solución**
3. Navega a la carpeta del proyecto y abre `ToDoList.sln`
4. Espera a que Visual Studio restaure los paquetes NuGet

### Paso 4: Compilar y Ejecutar

#### Opción A: Con IIS Express (Recomendado)

1. En Visual Studio, presiona **F5** o haz clic en el botón **▶ IIS Express**
2. La aplicación se abrirá automáticamente en tu navegador predeterminado

#### Opción B: Compilación manual

1. En Visual Studio, ve a **Compilar** → **Recompilar solución**
2. Si hay errores, verifica que:
   - Todos los paquetes NuGet estén instalados
   - MySQL está corriendo
   - La base de datos existe

## 📁 Estructura del Proyecto

```
ToDoList/
├── Default.aspx              # Página principal (interfaz)
├── Default.aspx.cs           # Code-behind (lógica)
├── Default.aspx.designer.cs  # Designer (controles)
├── CRUD.cs                   # Operaciones de base de datos
├── Conexion.cs               # Clase de conexión MySQL
├── Web.config                # Configuración de la aplicación
├── ToDoList.csproj           # Archivo de proyecto
└── packages.config           # Dependencias NuGet
```

## 🗄️ Esquema de Base de Datos

**Tabla: `tareas`**

| Campo              | Tipo          | Descripción                     |
|--------------------|---------------|---------------------------------|
| Id                 | INT (PK)      | Identificador único             |
| Titulo             | VARCHAR(100)  | Título de la tarea              |
| Descripcion        | VARCHAR(500)  | Descripción detallada           |
| FechaLimite        | DATE          | Fecha límite de la tarea        |
| Completada         | BOOLEAN       | Estado (pendiente/completada)   |
| FechaCreacion      | TIMESTAMP     | Fecha de creación automática    |
| FechaModificacion  | TIMESTAMP     | Fecha de última modificación    |

## 🎯 Funcionalidades

### 1. Agregar Tarea
- Llena el formulario con título, descripción, fecha límite y estado
- Haz clic en "Agregar Tarea"
- La tarea aparecerá en la lista automáticamente

### 2. Modificar Tarea
- Haz clic en "Seleccionar" en la tabla
- Los datos se cargarán en el formulario
- Modifica los campos que desees
- Haz clic en "Modificar Tarea"

### 3. Eliminar Tarea
- Haz clic en "Eliminar" en la fila de la tarea que deseas eliminar
- Confirma la eliminación

### 4. Filtrar Tareas
- **Mostrar Todas**: Muestra todas las tareas sin filtro
- **Tareas Completadas**: Solo muestra tareas completadas
- **Tareas Pendientes**: Solo muestra tareas pendientes

## 🔧 Tecnologías Utilizadas

- **Backend**: ASP.NET Web Forms (.NET Framework 4.7.2)
- **Lenguaje**: C#
- **Base de datos**: MySQL 9.5
- **Frontend**: HTML5, CSS3, Bootstrap 5
- **Iconos**: Bootstrap Icons
- **ORM**: ADO.NET con MySql.Data

## ⚠️ Notas Importantes

> **WindowsForms**: Este proyecto **NO contiene ningún rastro** de WindowsForms. Ha sido completamente convertido a ASP.NET Web Application.

> **Visual Studio 2022**: El proyecto está configurado para compilarse en Visual Studio 2022 con .NET Framework 4.7.2.

> **MySQL**: Asegúrate de que MySQL esté corriendo en `localhost:3306` con las credenciales configuradas en `Web.config`.

## 📝 Cambios Realizados en la Migración

### Archivos Eliminados (WindowsForms)
- ❌ Form1.cs
- ❌ Form1.Designer.cs
- ❌ Form1.resx
- ❌ Program.cs
- ❌ App.config

### Archivos Nuevos (Web Application)
- ✅ Default.aspx
- ✅ Default.aspx.cs
- ✅ Default.aspx.designer.cs
- ✅ Web.config

### Archivos Modificados
- 🔄 Conexion.cs (ahora lee desde Web.config)
- 🔄 ToDoList.csproj (convertido a proyecto web)
- ✅ CRUD.cs (mantenido sin cambios, compatible con web)

## 👤 Autor

**Carlos Saul Arenas Maciel**

## 📅 Fecha

Noviembre 2025

---

¿Necesitas ayuda? Revisa que:
1. ✅ MySQL esté corriendo
2. ✅ La base de datos `todolist` exista
3. ✅ Las credenciales en `Web.config` sean correctas
4. ✅ Los paquetes NuGet estén restaurados
