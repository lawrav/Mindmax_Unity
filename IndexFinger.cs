using UnityEngine;
using UnityEngine.SceneManagement;

public class IndexFinger : MonoBehaviour
{

    public Collider[] targetColliders; // Colisionadores específicos para detectar interacciones

    private void OnTriggerEnter(Collider other)
    {
        if (IsTargetCollider(other))
        {
            Debug.Log("Trigger Entered: " + other.name);
        }

    }

    private bool IsTargetCollider(Collider other)
    {
        foreach (Collider target in targetColliders)
        {
            if (other == target)
            {
                return true;
            }
        }
        return false;
    }
}

