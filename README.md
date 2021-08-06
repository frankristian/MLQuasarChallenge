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

## Aclaración funcional
Si no se han realizado al menos 3 llamadas *Post* al método /api/topsecret_split/{name} para actualizar los datos (distancias y mensajes) de los 3 puntos, puede que el servicio retorne un error de información insuficiente cuando se realice una llamada al método *Get*
