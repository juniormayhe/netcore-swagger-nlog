# PAC.WebApplication

Aplicación principal de la Plataforma de Atención al Cliente

# Dependencias

Para consumir webAPIs:

- System.Net.Http
- Microsoft.AspNet.WebApi.Client

# Creando una WebApplication para consumir WebAPI a través de referencia a proyectos

Agregue su WebApplication reutilizando otros proyectos de la solución

- PAC.abcAPI (referencia al servicio web deseado, donde abc es el nombre del WebAPI)
- PAC.Business.Contracts (requests y responses de los WebAPIs)
- PAC.Common (configuración de paginación PageSettings)
- PAC.Entities (entidades que representan objetos de la base de datos)

# Creando una WebApplication externa para consumir WebAPI a través de ajax 

Si una WebApplication externa necesita consumir WebAPIs de PAC a través de ajax

- crear las clases para request/response y entities necesarias o;
- manejar request y response con el tipo dynamic (JObject)