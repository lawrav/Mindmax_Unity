using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float countdownDuration = 60f; // Default 60 seconds countdown

    private float startTime;
    private bool isRunning = false;

    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
        Debug.Log("Timer iniciado.");
    }

    public void StopTimer()
    {
        if (isRunning)
        {
            isRunning = false;
            Debug.Log($"Timer detenido. Tiempo total: {GetElapsedTime():F2} segundos");
        }
    }

    public float GetElapsedTime()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            //Debug.Log($"Tiempo transcurrido: {elapsedTime:F2} segundos");
            return elapsedTime;
        }
        return 0f;
    }

    public bool IsTimeUp()
    {
        if (!isRunning) return false;
        return GetElapsedTime() >= countdownDuration;
    }

    // Call this method from ClickCubeInteraction script
    public void OnCubeTriggered()
    {
        if (isRunning)
        {
            GetElapsedTime();
        }
    }

    // Call this method from QuitButton script
    public void OnQuitButtonPressed()
    {
        StopTimer();
    }
}
