using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneIdx = currentScene.buildIndex;
        SceneManager.LoadScene(currentSceneIdx+1);
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
