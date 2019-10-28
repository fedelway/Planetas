# Planetas

Resolución ejercicio Planetas de meli.

# Uso

Simplemente nos paramos sobre la carpeta "Planetas" (donde se encuentra "Planetas.csproj") y ejecutamos:
```
dotnet run
```
El programa se compilará y ejecutará dando como resultado el reporte del clima.

En la carpeta donde ejecuta el programa debe existir el archivo "appSettings.json". Aquí irá la configuración de la simulación.

Ejemplo:
```
{
  "doubleComparisonPrecision": 5,
  "daysToSimulate": 3650,
  "uploadToMongo": true
}
```

* doubleComparisonPrecision: Marca con qué precisión se harán las 
comparaciones de double.
* daysToSimulate: La cantidad de días que tendrá la simulación.
* uploadToMongo: Si una vez finalizada la ejecución se actualizará la 
base de datos con los nuevos datos.

# Decisiones tomadas

## Elección del lenguaje

El problema, al ser un problema de simulación puramente matemático, podía ser resuelto con cualquier lenguaje de programación. Por las características del mismo, un lenguaje orientado a objetos era muy apropiado al existir entidades muy claramente definidas que se pueden modelar mediante objetos: Planetas, Sistema Solar, Sistemas de coordenadas, etc.

En particular, elegí .Net Core como tecnología para resolverlo debido a la comodidad de ya tener todo el entorno instalado y configurado en la máquina donde realice el desarrollo.

## Modelo de la solución

Lo primero que tuve que pensar al resolver el problema era cómo realizar los cálculos para determinar la posición de los planetas. Lo más fácil me pareció plantear un sistema de coordenadas cartesianas, donde cada planeta tiene coordenadas X,Y. Luego, utilizando la posición podría calcular los distintos tipos de clima que plantea el enunciado. Sin embargo, como los planetas tienen órbitas circulares y una distancia fija respecto del eje de coordenadas (el Sol), lo más apropiado era utilizar un sistema de coordenadas polares. De esta forma, la rotación se logra simplemente incrementando el ángulo de las coordenadas polares.

De esta forma, me quedó una clase Planeta de la siguiente manera (en pseudocódigo):
```
class Planeta{
  coordenadas polares, 
  velocidad de rotación,
  la dirección rotación.
  
  public Rotar();
}
```

Por otra parte, las condiciones climáticas se dan a partir de la relación entre las posiciones de los tres planetas. Para resolver este tema utilice una entidad SistemaSolar que posee una lista con tres planetas. Los objetos de tipo sistema solar pueden simular un día y calcular el clima que se da en ese día particular.

```
class SistemaSolar{
  Lista<Planeta> planetas
  
  public Clima calcularClima();
}
```

Para modelar el Clima utilicé una clase que posee un enum con el tipo de clima y la intensidad de la lluvia. Utilicé el patrón Factory Method para construir Climas válidos. Esto es porque la intensidad de la lluvia solo aparece para el clima "lluvioso", por lo tanto, los otros climas deberían no tener intensidad. Para evitar complicaciones, consideré que los otros climas tienen una intensidad de lluvia = 0. De esta forma, con el Factory Method, sería imposible generar un objeto con tipo de clima sequía y una intensidad de lluvia distinta de 0.

```
class Clima{
  TipoClima tipo;
  intensidad de lluvia;
}
```

Por último generé una clase ReporteClima que realiza una simulación dado un sistema solar y la cantidad de dias a simular, y genera el reporte con el clima que se da en cada día y las estadísticas de cuantos días hubo de cada clima.

## Problemas encontrados

Uno de los primeros problemas con los que me encontré fue encontrar un librería que trabaje con coordenadas cartesianas y polares tal cual necesitaba. 

Lo que más se aproximaba dentro de las librerías estándar de .Net era la clase Vector2. Comencé a resolver el problema modelando las coordenadas como instancias de Vector2 pero llegue a la limitación de que necesitaba convertir de coordenadas polares y cartesianas, y viceversa. 

Las librerías de terceros de álgebra lineal y matemáticas resultaban muy complicadas de utilizar. Las más apropiadas eran librerías de gráficos por computadoras, pero no se justificaba importar todo un motor gráfico para utilizar solo un puñado de clases y métodos.

Por esto mismo es que decidí refactorizar y crear mis propias clases "CartesianCoordinates" y "PolarCoordinates". No fue algo muy complicado, y luego tuvo su resultado positivo.

Otra razón que me motivó a realizar el cambio, fue que la precisión de "float" no se ajustaba a las necesidades del problema y se necesitaba utilizar "double" (algo imposible con Vector2). Aún así, utilizando "double" seguí experimentando problemas la precisión y tuve que truncar los dígitos decimales. De otra manera, tras las conversiones entre coordenadas cartesianas y polares, los errores de redondeo generaban que no se valide la igualdad.

Lo mismo ocurrió con valores muy cercanos a cero. Si un ángulo muy cercano a cero se comparaba con otro tambien muy cercano a cero pero negativo, la igualdad no verificaba.

# Resolución Bonus

Para el bonus consideré utilizar una base de datos NoSql. Al no tener esquema, suele ser mucho más fácil persistir datos del modelo de clases. Además, considerando que no es necesaria una alta consistencia y siendo únicamente una API de consulta, una base de datos NoSql se adaptaba bien a mis necesidades.

En particular elegí MongoDb porque lo he utilizado un par de veces y sé que tiene drivers para todos los lenguajes populares. Además, sé que existe una solución Cloud de mongo, Atlas, que es muy fácil de utilizar y configurar.

En cuanto a la API rest, la implementé utilizando NodeJS con Express. La pude haber hecho en .Net pero no había ningún motivo para hacerlo, porque la API esta totalmente separada de la lógica de generación de la simulación y los resultados. Además, existen muchas soluciones Cloud para hostear API's en Node.

Para hostear la api utilice Google Firebase Functions. Adaptar la aplicación original de Node no requerió de mucho esfuerzo adicional porque Firebase tiene soporte directo para Express.
