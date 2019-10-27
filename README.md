# Planetas

Resolución ejercicio Planetas de meli.

# Uso

Simplemente se debe ejecutar "Planetas.exe" y mostrará por consola los 
resultados de la simulación.

En la carpeta donde ejecuta el programa debe existir el archivo 
"appSettings.json". Aquí irá la configuración de la simulación.
Ejemplo:
´´´
{
  "doubleComparisonPrecision": 5,
  "daysToSimulate": 3650,
  "uploadToMongo": true
}
´´´

* doubleComparisonPrecision: Marca con qué precisión se harán las 
comparaciones de double.
* daysToSimulate: La cantidad de días que tendrá la simulación.
* uploadToMongo: Si una vez finalizada la ejecución se actualizará la 
base de datos con los nuevos datos.
