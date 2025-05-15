## UNITY SCRIPTS & APK MINDMAX GAME
En este repositorio podrán encontrar los scripts utilizados para el desarrollo del juego de VR MINDMAX desarrollado en EMFUTECH 2025 en MIRAI Research Institute. 

##🔄 Rotación del cubo
El cubo gira constantemente sobre su eje Y a una velocidad configurable (rotationSpeed).
La rotación se detiene cuando el cubo colisiona con uno de los targetColliders.

🎨 Cambio de color
El color inicial del cubo es púrpura (initialColor).
Cuando se activa por colisión, cambia a color blanco (triggeredColor).

🔢 Generación de número aleatorio y puntaje
Al activarse por colisión:
Se genera un número aleatorio entre 1 y 10.
Este número se muestra en el cubo (si cubeText está asignado).
Se suma al total del puntaje, que se actualiza en pantalla (scoreText).

📁 Registro en archivo CSV
Se crea un archivo llamado cube_interactions.csv en la carpeta del proyecto.
Cada vez que el cubo es activado, se agrega una línea con:
El nombre del cubo
El número aleatorio generado
El tiempo del evento

✅ Verificación de colisión
Usa el método IsTargetCollider() para verificar si el Collider que activa la colisión es uno de los targetColliders definidos.

##⏱️ Temporizador
Usa un script de temporizador externo (Timer) para registrar el tiempo exacto de la colisión.
El tiempo se guarda con dos decimales.

## 🧠 ¿Para qué sirve?
-- Este script podría servir perfectamente para:
-- Mini juegos de puntuación (como tu juego de cubos).
-- Entrenamientos cognitivos o experimentos con VR/EEG, donde se necesita medir reacciones.
--Prototipos interactivos que requieren logging de acciones del usuario.
