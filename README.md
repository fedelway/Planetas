# Planetas

Resolución ejercicio Planetas de meli.

# Uso

Simplemente se debe ejecutar "Planetas.exe" y mostrará por consola los 
resultados de la simulación.

En la carpeta donde ejecuta el programa debe existir el archivo 
"appSettings.json". Aquí irá la configuración de la simulación.

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

Para modelar el Clima utilicé un simple enum. Podría haber utilizado una clase Clima subclaseando por tipo de clima, pero al no tener ningún comportamiento diferencial entre los mismos, no me pareció una buena solución. Los tipos de climas son los que menciona el enunciado, más el tipo de clima "NORMAL" que aparece cuando no se cumple ninguna de las condiciones de los otros climas.

