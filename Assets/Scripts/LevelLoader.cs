using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private FadeScreen _fadeScreen = null;

    void Start()
    {
        GameObject fadePanel = GameObject.FindWithTag("FadeScreenPanel");
        if(fadePanel != null)
        {
            _fadeScreen =  fadePanel.GetComponent<FadeScreen>();
        }
    }

    public void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int targetSceneIdx = (currentScene.buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        StartCoroutine(LoadSceneRoutine(targetSceneIdx));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        float fadeDuration = 0.0f;
        if(_fadeScreen != null)
        {
            _fadeScreen.FadeOut();
            fadeDuration = _fadeScreen.fadeDuration;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while(timer <= fadeDuration || operation.progress < 0.9f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
