# 🐱 CatGifApi - Backend (.NET + PostgreSQL)

CatGifApi es una API REST construida con ASP.NET Core que permite buscar un GIF relacionado con una consulta de texto (query), usando la API pública de Giphy, y guardar el historial de búsqueda en una base de datos PostgreSQL.

---

## 🚀 Tecnologías

- .NET SDK 8.0+
- Entity Framework Core
- PostgreSQL
- Giphy API
- Swagger para documentación de la API

---

## ⚙️ Requisitos Previos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)
- PostgreSQL (local o remoto)
- Visual Studio, VS Code o cualquier editor compatible

---

## 📦 Instalación

1. **Clonar el repositorio**:

   ```bash
   git clone https://github.com/TheOliver413/CatGifApi
   cd CatGifApi

2. **Configurar la cadena de conexión en appsettings.json:**:
   ```bash
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=catgifdb;Username=postgres;Password=tu_password"
    }
3. **Aplicar migraciones y crear la base de datos:**:
   ```bash
   dotnet ef database update

4. **Ejecutar el proyecto:**:
   ```bash
   dotnet run

## El backend estará disponible en: http://localhost:5145 

## 📖 Documentación Swagger
Una vez iniciado el servidor, puedes acceder a la documentación interactiva Swagger:
    
    🔗 http://localhost:5145/swagger

## 1. **🐱 Obtener un dato aleatorio sobre gatos**

GET /api/cat/fact/random

- Llama a la API https://catfact.ninja/fact y devuelve un dato aleatorio sobre gatos.

**Ejemplo de respuesta:**
```bash
    {
        "fact": "A cat sees about 6 times better than a human at night, and needs 1/6 the amount of of light that a human does - it has a layer of extra reflecting cells which absorb light."
    }
```

## 2. **🔍 Buscar GIF**

GET /api/gif?query=palabra1 palabra2 palabra3

- Parámetro: query → Un string con 3 palabras.

- Acción:

    - Usa las primeras 3 palabras del query para buscar un GIF en Giphy.

    - Guarda el resultado en la base de datos.

**Ejemplo:**

    GET /api/gif?query=happy cat jumping

**Respuesta:**
```bash
    {
        "originalQuery": "happy cat jumping",
        "trimmedQuery": "happy cat jumping",
        "gif": "https://media3.giphy.com/media/XW5yE0xTpVDqxdEskD/giphy.gif?cid=581e12b99l7lfqvebfdve5j0rjq0s3wc4clv86a2xvq6p7v4&ep=v1_gifs_search&rid=giphy.gif&ct=g"
    }
```
## 3. **🗃️ Historial de Búsquedas**

GET /api/history/all

- Devuelve todas las búsquedas almacenadas en la base de datos.

- Acción:

    - Usa las primeras 3 palabras del query para buscar un GIF en Giphy.

    - Guarda el resultado en la base de datos.

**Ejemplo de respuesta:**
```bash
    [
        {
            "id": 3,
            "date": "2025-06-05T05:25:49.593374Z",
            "catFact": "The smallest pedigreed cat is a Singapura, which can weigh just 4 lbs (1.8 kg), or about five large cans of cat food. The largest pedigreed cats are Maine Coon cats, which can weigh 25 lbs (11.3 kg), or nearly twice as much as an average cat weighs.",
            "queryWords": "The smallest pedigreed",
            "gifUrl": "https://media3.giphy.com/media/13A7YlLvYVDnmU/giphy.gif?cid=581e12b9p8u1ryvey4cgl4o2b82z38jqxv2lttaa8ac609tf&ep=v1_gifs_search&rid=giphy.gif&ct=g"
        }
    ]
```

## 4. **🔄 Recarga de gift**

POST /api/gif/refresh

- Refrescar únicamente el GIF relacionado a un dato curioso sobre gatos previamente mostrado, sin cambiar el texto del dato (fact). 

**📥 Cuerpo de la solicitud (application/json):**
```bash
    {
        "catFact": "Cats sleep 70% of their lives."
    }
```

**📤 Respuesta exitosa**
```bash
    {
        "gifUrl": "https://media0.giphy.com/media/aMmiKP7Z4TquI/giphy.gif?cid=581e12b9jcvi5002ucdakaig5kpb1gwpcyt71cljvaq7g5dd&ep=v1_gifs_search&rid=giphy.gif&ct=g"
    }
```


## 📝 Estructura de Base de Datos
- Tabla: SearchHistories

Campo	Tipo
Id	int (PK)
Date	timestamp with time zone
QueryWords	text
GifUrl	text

| Campo  | Tipo |
| ------------- | ------------- |
| Id  | int (PK)  |
| Date  | timestamp with time zone  |
| QueryWords  | text  |
| GifUrl  | text  |

## 🔑 API Keys
Este proyecto utiliza la API pública de Giphy:
```bash
    voaNIOg1u7ONPbckzWK71C48YqCOkhVP
```

## 🛠️ Comandos Útiles
Agregar migración:
```bash
dotnet ef migrations add InitialCreate
```

Aplicar migración:
```bash
dotnet ef database update
```

## 🧑‍💻 Autor
- Oliver Borda
- theoliver413
- oliverborda04@outlook.com