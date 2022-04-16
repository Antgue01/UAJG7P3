# UAJG7P3
# Curso 2021 / 2022 - Universidad Complutense de Madrid
# Facultad de Informática - Videojuegos


# Guión de evaluación
Objetivo : Comprobar si el diseño del nivel está balanceado
-------------------------------------
A continuación se muestran las preguntas que se pretenden responder a través del sistema de telemetría y los eventos de los que vamos a utilizar para ello:
- ¿Existen zonas en el mapa a las que los jugadores no van?
    - Distribución de tiempo en cada habitación:
        Vamos a utilizar un evento periódico que contenga información sobre la posición del jugador en casillas.
    - Frecuencia de uso de cada uno de los objetos que generan caos en el nivel:
        Vamos a utilizar un evento instantaneo que se va a lanzar cada vez que se use un objeto. En dicho evento se va a almacenar un ID que lo identifique.
- ¿Existen zonas especialmente difíciles en el nivel?
    Vamos a analizar el número de muertes que ocurren en cada una de las zonas del nivel. Para ello, se va a utilizar un evento instantaneo que se dispare en el momento en el que un enemigo mate al jugador, donde se va a almacenar el id propio de cada uno de los enemigos. 
- ¿El jugador ha tenido dificultades para pasarse el nivel?
    - Tiempo medio que tarda el jugador en pasarse el nivel
        Para analizar esto vamos a hacer uso de los eventos de inicio y final de sesion.
    - Número de vidas utilizadas por nivel
        Para analizar esto vamos a hacer uso del evento de muerte descrito anteriormente .


# PostProcesado de los eventos
Una vez tenemos hemos realizado las pruebas, el procesado de los datos lo vamos a hacer de las siguientes formas:

Tiempo medio por habitación
------------------------------------------------------------------------
Para calcular el tiempo medio por habitación, combinaremos los eventos de posición del jugador con secciones del mapa asociadas a determinadas habitaciones (por ejemplo x>10 && x < 20 && y>20 && y <30). Luego, juntaremos esto con el timestamp del evento para calcular cuando se entra y cuando se sale de las zonas importantes, y con esto su tiempo medio. 

Frecuencia de uso de cada uno de los objetos que generan caos en el nivel
------------------------------------------------------------------------
Para sacar la frecuencia de uso de cada objeto, dividiremos la cantidad de veces que el jugador utilizó dicho objeto entre el número de vidas que necesitó para pasarse el nivel.

Número de muertes provocadas por zona
------------------------------------------------------------------------
Para calcular el número de muertes provocadas por zona, cada vez que el jugador muera nos guardaremos su posición, y al igual que en el analisis del tiempo medio por habitación, dividiremos el mapa en secciones. Para posteriormente contar cuántas muertes han ocurrido en cada una.

Tiempo medio que tardan en pasarse el nivel	
------------------------------------------------------------------------
Para calcular el tiempo medio que tardan en pasarse el nivel, tomaremos el tiempo que tarda cada jugador en pasárselo mediante los timestamps presentes en los inicios y finales de sesión, y haremos la media con los tiempos obtenidos entre todos los jugadores.

Número de vidas utilizadas por nivel
------------------------------------------------------------------------
Para calcular el número de vidas utilizadas por nivel, juntaremos los eventos de muerte, inicio y final de sesión de cada uno de los jugadores. Con el objetivo de sacar además la desviación típica de estos datos.

