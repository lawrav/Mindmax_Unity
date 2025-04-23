using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceGrabSceneTrigger : MonoBehaviour
{
    [Tooltip("Lista de objetos que deben colisionar con esta esfera")]
    public List<GameObject> targetObjects;

    [Tooltip("Nombre de la escena a cargar cuando todos los objetos han sido tocados")]
    public string targetSceneName;

    private HashSet<GameObject> touchedObjects = new();

    private void OnTriggerEnter(Collider other)
    {
        if (targetObjects.Contains(other.gameObject))
        {
            if (touchedObjects.Add(other.gameObject))
            {
                Debug.Log($"[SceneTrigger] Colisionaste con: {other.gameObject.name}. Total: {touchedObjects.Count}/{targetObjects.Count}");

                if (touchedObjects.Count >= targetObjects.Count)
                {
                    Debug.Log("[SceneTrigger] Todos los objetos requeridos fueron tocados. Cambiando de escena...");
                    SceneManager.LoadScene(targetSceneName);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[SceneTrigger] Tecla SPACE presionada. Cambiando de escena...");
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
