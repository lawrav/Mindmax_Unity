using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("Tiempo total en segundos")]
    public float totalTime = 60f;

    [Header("Segundos finales para cambio de color")]
    public float warningThreshold = 10f;

    [Header("Colores")]
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;

    [Header("Referencia al TextMeshPro")] 
    public TextMeshProUGUI TimerText;

    [Header("Estado del temporizador")]
    public bool isRunning = false;

    private float currentTime;

    void Start()
    {
        currentTime = totalTime;
        isRunning = false; // Asegura que NO arranque automáticamente
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0);

        UpdateTimerDisplay();

        if (currentTime <= 0)
        {
            isRunning = false;
            OnTimerEnd();
        }
    }

    public void StartCountdown()
    {
        currentTime = totalTime; // Reinicia tiempo por si se quiere reusar
        isRunning = true;
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerText.color = currentTime <= warningThreshold ? warningColor : normalColor;
    }

    void OnTimerEnd()
    {
        Debug.Log("Tiempo terminado");
    }
}
