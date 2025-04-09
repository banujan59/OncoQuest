using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int targetSceneIdx = (currentScene.buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        StartCoroutine(LoadSceneRoutine(targetSceneIdx));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        while(operation.progress < 0.9f)
        {
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
