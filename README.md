# MLQuasarApi

Servicio MLQuasarApi  es un servicio RestAPI que retorna la posición de la de un punto y la decodificación del mensaje a través del cálculo de un punto mediante Trilateración. Se deja el link al concepto matemático del mismo [Trilateración de un punto](https://es.wikipedia.org/wiki/Trilateraci%C3%B3n).
**Consideraciones especiales del funcionamiento:**

 - Las posiciones de los 3 puntos están configuradas dentro del archivo \MLQuasar.Infrastucture\DBJson\dbQuasar.json
 - Los array de mensajes deben tener la misma longitud
 - El cálculo del punto no posee redondeo ni delta de ajuste.
 - Para validaciones de los cálculos se puede utilizar la herramienta online [Desmos](https://www.desmos.com/calculator/vdy4hafwyb?lang=es)
 ![Desmos Trilateration](https://github.com/frankristian/MLQuasarChallenge/blob/master/docs/img/img.PNG?raw=true)
 
    
# Cómo probar el servicio
El servicio cuenta con 2 endpoints  los cuales detallamos más abajo cómo funcionan.
 - /api/TopSecret
	 - 
	 - Este endpoint cuenta con  un método POST que recibe la distancia del punto a calcular hacia cada uno de los puntos configurados, y además el mensaje a decodificar mediante un array de string. Cada posición del array contiene una palabra o un string vacío.
	 - Ejemplo: 
	 - ![Post Ejemplo](https://github.com/frankristian/MLQuasarChallenge/blob/master/docs/img/Post1.PNG?raw=true)
 - /api/TopSecret_split
	 - 
	 - Este endpoint posee 3 métodos *(Post/Get/Delete)* 
			 - ***Post***: A este método lo llamamos con el nombre del punto al cual queremos setearle la distancia y mensaje (cabe aclarar que el nombre está configurado en el mismo Json detallado más arriba).
			 - 
			 - Ejemplo: api/topsecret_split/kenobi

![Post Parcial](https://github.com/frankristian/MLQuasarChallenge/blob/master/docs/img/Post2.PNG?raw=true)
			 - ***Get***: realiza la misma acción que el post del endpoint /api/TopSecret con la diferencia que consulta los datos guardados mediando las diferentes llamadas al post anterior. 
			- Ejemplo: /api/topsecret_split
			- ***Delete***: Verbo que se utiliza para resetear los datos almacenados.
			- Ejemplo: /api/topsecret_split
## Archivos de prueba

 - Dentro de la carpeta [/docs/PostmanCollection/](MLQuasarChallenge/docs/PostamCollection/) podemos encontrar el archivo Json con casos de test de aceptación para probar el servicio. Esto puede ser testeado mediante la herramienta **Postman**

## Aclaraciones funcionales

 - Los tamaños de los arrays de mensajes deben ser todos de la misma longitud.
 - Si no se han realizado al menos 3 llamadas *Post* al método /api/topsecret_split/{name} para actualizar los datos (distancias y mensajes) de los 3 puntos, puede que el servicio retorne un error de información insuficiente cuando se realice una llamada al método *Get*

# Descripción de la arquitectura

Se optó por utilizar arquitectura basada en la definición de Robert Martin [Clean Arquitecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html). Es arquitectura también se la conoce como Cebolla o Hexagonal. 

Las capas de la arquitectura planteada son las siguientes:
 1. **Api Controllers**: Recibe las peticiones HTTP y delega la responsabilidad de la mediante inyección de dependencias en los servicios otorgados por la capa de Aplicación.
 2. **Application**: Aquí se encuentran los servicios que resuelven la lógica de la aplicación y a su vez manipula entidades del Dominio para obtener y setear información.
 3. **Domain**: Maneja estado de las entidades del dominio. Además se agregan funcionalidades a través de extensiones a dichas entidades. Aquí también podemos encontrar las entidades de tipo Dto que representan los objetos del Request y Response respectivamente.
 4.  **Infrastructure**: Se utiliza el patrón Repository para retornar entidades de Dominio desde la fuente de datos (en nuestro caso, un archivo Json).

> Nota: Queda fuera del scope de este proyecto el manejo de excepciones custom. Se utilizan las Excepciones definidas en el framework de .Net.

## Diagrama de arquitectura
![Diagrama de la arqiutectura](https://github.com/frankristian/MLQuasarChallenge/blob/master/docs/img/ArquitectureDiagram.png?raw=true)

## Detalles adicionales

 - Se realizaron pruebas unitarias utilizando las librerías XUnit, Moq y FluentAssertion

# Deploy del servicio
El servicio se desplegó en y habilitó en Google Cloud Platform y se puede acceder en la siguiente url https://mlquasarapi.uc.r.appspot.com
