# 💻 Clase04 - DSW1

Proyecto académico desarrollado para el curso de **Desarrollo de Software Web (DSW1)** utilizando tecnologías .NET y arquitectura MVC.  
Este repositorio contiene la implementación de funcionalidades backend y frontend orientadas al desarrollo de aplicaciones web modernas.

---

## 🚀 Objetivos del proyecto

- Aplicar el patrón MVC
- Implementar buenas prácticas de desarrollo web
- Gestionar datos mediante base de datos relacional
- Trabajar con control de versiones usando Git y GitHub
- Desarrollar funcionalidades CRUD completas

---

## 🛠️ Tecnologías utilizadas

### Backend
- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server

### Frontend
- HTML5
- CSS3
- Bootstrap
- JavaScript
- Razor Views

### Herramientas
- Visual Studio 2022
- Git & GitHub

---

## 📂 Estructura del proyecto

```bash
Clase04-DSW1/
│
├── Controllers/      # Controladores MVC
├── Models/           # Modelos de datos
├── Views/            # Vistas Razor
├── Data/             # Contexto de base de datos
├── wwwroot/          # Recursos estáticos
├── appsettings.json  # Configuración del proyecto
└── Program.cs        # Punto de entrada
```

---

## ⚙️ Funcionalidades

- ✅ Registro de información
- ✅ Edición de datos
- ✅ Eliminación de registros
- ✅ Listado dinámico
- ✅ Integración con base de datos
- ✅ Navegación entre vistas
- ✅ Diseño responsive

---

## 🧩 Arquitectura utilizada

El proyecto implementa el patrón:

### MVC (Model - View - Controller)

- **Models** → Representación de datos
- **Views** → Interfaz de usuario
- **Controllers** → Lógica de negocio y control de flujo

---

## 🗄️ Base de datos

La aplicación utiliza **SQL Server** junto con **Entity Framework Core** para el acceso y manipulación de datos.

---

## ▶️ Cómo ejecutar el proyecto

### 1️⃣ Clonar el repositorio

```bash
git clone https://github.com/EDU11QR/Clase04-DSW1.git
```

### 2️⃣ Abrir en Visual Studio

Abrir la solución con:

```bash
Visual Studio 2022
```

### 3️⃣ Configurar conexión

Editar el archivo:

```bash
appsettings.json
```

Agregar tu cadena de conexión:

```json
"ConnectionStrings": {
  "cn": "TU_CONEXION_SQL_SERVER"
}
```

---

### 4️⃣ Ejecutar migraciones

```bash
Update-Database
```

---

### 5️⃣ Ejecutar el proyecto

```bash
Ctrl + F5
```
---

## 📚 Aprendizajes aplicados

- Arquitectura MVC
- Entity Framework Core
- Conexión a SQL Server
- CRUD con ASP.NET Core
- Control de versiones con Git
- Desarrollo web responsive

---

## 👨‍💻 Autor

### DevEdu
