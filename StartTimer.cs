using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public Timer timer;
    public TextMeshProUGUI timerText;
    public Button startButton;
    public float countdownDuration = 60f;
    private bool timerStarted = false;
    private float remainingTime;
    private Color originalTextColor;

    void Start()
    {
        if (timerText != null)
        {
            timerText.text = "Timer: 60.0";
            originalTextColor = timerText.color;
        }

        if (startButton != null)
        {
            startButton.onClick.AddListener(StartTimerOnClick);
        }
    }

    void Update()
    {
        if (timerStarted && timer != null && timerText != null)
        {
            remainingTime = countdownDuration - timer.GetElapsedTime();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                timerText.text = "Time's up!";
                timerStarted = false;
            }
            else
            {
                timerText.text = $"Timer: {remainingTime:F1}";
                
                // Change text color to red when 10 seconds or less remain
                timerText.color = remainingTime <= 10f ? Color.red : originalTextColor;
            }
        }
    }

    private void StartTimerOnClick()
    {
        if (!timerStarted && timer != null)
        {
            timer.StartTimer();
            timerStarted = true;
            remainingTime = countdownDuration;
            Debug.Log("Timer started from button click");
        }
    }
}