using UnityEngine;
using TMPro;
using System.IO;
using System.Collections;

public class RotatingCubeGame : MonoBehaviour
{
    [Header("Collision Settings")]
    public Collider[] targetColliders;

    [Header("Rotation Settings")]
    public float rotationSpeed = 50f;
    public bool isRotating = true;

    [Header("Color Settings")]
    public Color initialColor = new Color(0.5f, 0f, 0.5f); // Purple
    public Color triggeredColor = Color.white;

    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public TextMeshPro cubeText;
    public TextMeshProUGUI finalScoreText;

    [Header("Timer Reference")]
    public Timer timer;

    [Header("Audio Clips")]
    public AudioClip coinSound;
    public AudioClip errorSound;
    public AudioClip tickTackSound;

    private AudioSource coinSource;
    private AudioSource errorSource;
    private AudioSource tickTackSource;

    private static int totalScore = 0;
    private static int lastNumber = -1;

    private string filePath;
    private string cubeName;
    private bool isTriggered = false;
    private bool isGameOver = false;

    private Renderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = initialColor;
        UpdateScoreDisplay();

        filePath = Path.Combine(Application.dataPath, "cube_interactions.csv");
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Cube,Points,Time,TriggerType,TotalScore\n");
        }

        cubeName = gameObject.name;

        // Setup AudioSources
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 3)
        {
            tickTackSource = sources[0];
            coinSource = sources[1];
            errorSource = sources[2];
        }

        // Play background ticking if timer is running
        if (timer != null && !timer.IsTimeUp() && tickTackSound != null && tickTackSource != null)
        {
            tickTackSource.clip = tickTackSound;
            tickTackSource.loop = true;
            tickTackSource.Play();
        }
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetMouseButtonDown(0) && !isTriggered && !isGameOver)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                TriggerCube("Click");
            }
        }

        if (timer != null && timer.IsTimeUp() && !isGameOver)
        {
            ShowFinalScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTargetCollider(other) && !isTriggered && !isGameOver)
        {
            TriggerCube("Collision");
        }
    }

    private void TriggerCube(string triggerType)
    {
        isTriggered = true;
        isRotating = false;
        cubeRenderer.material.color = triggeredColor;

        int randomNumber = Random.Range(1, 11);

        if (randomNumber == lastNumber)
        {
            totalScore = randomNumber;
            Debug.Log($"Number {randomNumber} repeated! Resetting score to {totalScore}");

            if (errorSource != null && errorSound != null)
            {
                errorSource.PlayOneShot(errorSound);
            }
        }
        else
        {
            totalScore += randomNumber;
            Debug.Log($"New number {randomNumber} added! Total score is now {totalScore}");

            if (coinSource != null && coinSound != null)
            {
                coinSource.PlayOneShot(coinSound);
            }
        }

        lastNumber = randomNumber;
        UpdateScoreDisplay();

        if (cubeText != null)
        {
            cubeText.text = randomNumber.ToString();
        }

        float currentTime = timer != null ? timer.GetElapsedTime() : 0f;
        string line = $"{cubeName},{randomNumber},{currentTime:F3},{triggerType},{totalScore}\n";
        File.AppendAllText(filePath, line);

        StartCoroutine(ResetCubeAfterDelay(10f));
    }

    private IEnumerator ResetCubeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (timer != null && !timer.IsTimeUp())
        {
            isTriggered = false;
            isRotating = true;
            cubeRenderer.material.color = initialColor;
            if (cubeText != null)
            {
                cubeText.text = "";
            }
        }
        else
        {
            isGameOver = true;
        }
    }

    private void ShowFinalScore()
    {
        isGameOver = true;

        // Detiene el sonido de fondo
        if (tickTackSource != null)
        {
            tickTackSource.Stop();
        }

        if (finalScoreText != null)
        {
            finalScoreText.gameObject.SetActive(true);
            finalScoreText.text = $"Final Score: {totalScore}";
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

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }
    }
}
