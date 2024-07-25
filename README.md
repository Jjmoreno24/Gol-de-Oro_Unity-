[![KRmj-Mz6-Qa-Um-P-1584-396.png](https://i.postimg.cc/HkXSSH13/KRmj-Mz6-Qa-Um-P-1584-396.png)](https://postimg.cc/Yv2f959m)

# Gol de Oro en Unity 

![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)  ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) ![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) 

## Descripción

**Gol de Oro** es un videojuego que sumerge a los jugadores en la intensidad y emoción de los tiros de penal en un entorno de fútbol virtual. Este juego fusiona habilidad, estrategia y deporte, ofreciendo una experiencia desafiante y gratificante tanto para aficionados al fútbol como para aquellos que buscan un juego competitivo y táctico.

El juego destaca por su mecánica innovadora que requiere de los jugadores buena puntería y estrategia para superar barreras y las intervenciones dinámicas del portero. A medida que avanzan, los niveles incrementan en dificultad, introduciendo desafíos que requieren reflexión rápida y reacciones precisas. 

## Desarrollo
### Herramientas y Tecnologías Utilizadas
- **UNITY HUB:** versión 3.8.0
- **Lenguaje:** C#

### Implementación de la Línea de Trayectoria

La línea de trayectoria en **Gol de Oro** se implementa utilizando una ecuación matemática que simula la física del movimiento de un proyectil. Esto permite a los jugadores visualizar la trayectoria del balón antes de realizar un tiro, añadiendo una capa de estrategia y precisión al juego.

#### Ecuación de la Trayectoria

La trayectoria del balón se calcula en función de la velocidad inicial y la gravedad. La posición del balón en cada punto de la trayectoria se determina utilizando las siguientes ecuaciones del movimiento de proyectiles:

- **Velocidad Inicial**: Se calcula dividiendo el vector de fuerza aplicado al balón por la masa del `Rigidbody` y multiplicándolo por el tiempo fijo de actualización (`Time.fixedDeltaTime`).
- **Duración del Vuelo**: Se determina utilizando la fórmula `(2 * velocidad inicial en y) / gravedad`.
- **Cálculo de la Trayectoria**: Se utiliza la fórmula de movimiento de proyectiles para calcular la posición del balón en cada segmento de tiempo.

```csharp
for (int i = 0; i < lineSegmentCount; i++)
{
    float stepTimePassed = stepTime * i;

    // Ecuaciones del movimiento de proyectiles
    Vector3 movementVector = new Vector3(
        velocity.x * stepTimePassed,
        velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
        velocity.z * stepTimePassed
    );

    Vector3 newPosition = -movementVector + startingPoint;

    linePoints.Add(newPosition);
}
```

### Implementación de la Barrera

Cuando la pelota golpea a los jugadores de la barrera, se aplica una penalización en puntos al jugador. Esta lógica se implementa utilizando detección de colisiones y un prefab de texto flotante para mostrar visualmente la penalización.

**Detección de Colisiones**: Se utiliza el método `OnCollisionEnter` para detectar cuando la pelota golpea a los jugadores de la barrera.
- **Penalización de Puntos**: Se descuentan puntos del puntaje del jugador llamando a `ScoreManager.Instance.AddScore` con un valor negativo.
- **Texto Flotante**: Se instancia un prefab de texto flotante en la posición de la colisión para mostrar visualmente la penalización. El texto se destruye automáticamente después de un breve periodo.

```csharp
private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) 
        {
            // Muestra el texto flotante
            ShowFloatingText();
            // Llama al método para descontar puntos
            ScoreManager.Instance.AddScore(-puntosPenalizacion);
        }
    }
```

## 🔭 Vista - Ejecución

<p align="center">
  <a href="https://www.youtube.com/watch?v=lZy1SHYOSY0">
    <img src="https://img.youtube.com/vi/lZy1SHYOSY0/0.jpg" alt="Video de PowerFit" width="600">
  </a>
</p>

<div align="center">
<h3 align="center">Let's connect 😋</h3>
</div>
<p align="center">
<a href="https://www.linkedin.com/in/jjosemoreno24" target="blank">
<img align="center" width="30px" alt="Hector's LinkedIn" src="https://www.vectorlogo.zone/logos/linkedin/linkedin-icon.svg"/></a> &nbsp; &nbsp;
<a href="https://twitter.com" target="blank">
<img align="center" width="30px" alt="Hector's Twitter" src="https://www.vectorlogo.zone/logos/twitter/twitter-official.svg"/></a> &nbsp; &nbsp;
<a href="https://www.twitch.tv" target="blank">
<img align="center" width="30px" alt="Hector's Twitch" src="https://www.vectorlogo.zone/logos/twitch/twitch-icon.svg"/></a> &nbsp; &nbsp;
<a href="https://www.youtube.com" target="blank">
<img align="center" width="30px" alt="Hector's Youtube" src="https://www.vectorlogo.zone/logos/youtube/youtube-icon.svg"/></a> &nbsp; &nbsp;
</p>
