using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    private bool isTransitioning = false;
    private const string SCENE1_NAME = "Scene1";
    private const string SCENE2_NAME = "Scene2";

    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning) return;

        // Check if the colliding object is an IndexFinger
        if (other.GetComponent<IndexFinger>() != null)
        {
            Debug.Log("Index finger detected, preparing to change scene");
            StartCoroutine(ChangeSceneWithDelay());
        }
    }

    private IEnumerator ChangeSceneWithDelay()
    {
        isTransitioning = true;
        
        yield return new WaitForSeconds(1);

        try
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string targetScene = (currentScene == SCENE1_NAME) ? SCENE2_NAME : SCENE1_NAME;

            // Check if the target scene exists in build settings
            bool sceneExists = false;
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                if (sceneName == targetScene)
                {
                    sceneExists = true;
                    break;
                }
            }

            if (sceneExists)
            {
                Debug.Log($"Loading scene: {targetScene} (from {currentScene})");
                SceneManager.LoadScene(targetScene);
            }
            else
            {
                Debug.LogError($"Scene '{targetScene}' not found in build settings!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading scene: " + e.Message);
        }
        finally
        {
            isTransitioning = false;
        }
    }

    void Start()
    {
        Debug.Log($"Current scene: {SceneManager.GetActiveScene().name}");
        Debug.Log($"Total scenes in build: {SceneManager.sceneCountInBuildSettings}");
    }
}